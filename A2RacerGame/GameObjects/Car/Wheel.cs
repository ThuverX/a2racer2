using System.Numerics;
using Jitter2;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Jitter2.LinearMath;

class Wheel
{
    private readonly World world;

    private readonly RigidBody car;

    private float displacement,
        upSpeed,
        lastDisplacement;
    private bool onFloor;
    private float driveTorque;

    private float angVel;

    /// used to estimate the friction
    private float angVelForGrip;

    private float torque;

    private readonly World.RaycastFilterPre rayCast;

    public float SteerAngle { get; set; }

    public float WheelRotation { get; private set; }

    public float Damping { get; set; }

    public float Spring { get; set; }

    public float Inertia { get; set; }

    public float Radius { get; set; }

    public float SideFriction { get; set; }

    public float ForwardFriction { get; set; }

    public float WheelTravel { get; set; }

    public bool Locked { get; set; }

    public float MaximumAngularVelocity { get; set; }

    public int NumberOfRays { get; set; }

    public JVector Position { get; set; }

    public float AngularVelocity => angVel;

    public readonly JVector Up = JVector.UnitY;

    public Wheel(
        World world,
        RigidBody car,
        JVector bodyPosition,
        float radius,
        CarConfig.CarWheelConfig config
    )
    {
        this.world = world;
        this.car = car;
        Position = bodyPosition;

        rayCast = RayCastCallback;

        // set some default values.
        SideFriction = config.SideFriction;
        ForwardFriction = config.ForwardFriction;
        Radius = radius;
        Inertia = 1f;
        WheelTravel = config.WheelTravel;
        MaximumAngularVelocity = config.MaximumAngularVelocity;
        NumberOfRays = 1;
    }

    /// <summary>
    /// Gets the position of the wheel in world space.
    /// </summary>
    /// <returns>The position of the wheel in world space.</returns>
    public JVector GetWheelCenter()
    {
        return Position + JVector.Transform(Up, car.Orientation) * displacement;
    }

    /// <summary>
    /// Adds drivetorque.
    /// </summary>
    /// <param name="torque">The amount of torque applied to this wheel.</param>
    public void AddTorque(float torque)
    {
        driveTorque += torque;
    }

    public void PostStep(float timeStep)
    {
        if (timeStep <= 0.0f)
            return;

        float origAngVel = angVel;
        upSpeed = (displacement - lastDisplacement) / timeStep;

        if (Locked)
        {
            angVel = 0;
            torque = 0;
        }
        else
        {
            angVel += torque * timeStep / Inertia;
            torque = 0;

            if (!onFloor)
                driveTorque *= 0.1f;

            // prevent friction from reversing dir - todo do this better
            // by limiting the torque
            if (
                (origAngVel > angVelForGrip && angVel < angVelForGrip)
                || (origAngVel < angVelForGrip && angVel > angVelForGrip)
            )
                angVel = angVelForGrip;

            angVel += driveTorque * timeStep / Inertia;
            driveTorque = 0;

            float maxAngVel = MaximumAngularVelocity;
            angVel = Math.Clamp(angVel, -maxAngVel, maxAngVel);

            WheelRotation += timeStep * angVel;
        }
    }

    public void PreStep(float timeStep)
    {
        JVector force = JVector.Zero;
        lastDisplacement = displacement;
        displacement = 0.0f;

        float vel = car.Velocity.Length();

        JVector worldPos = car.Position + JVector.Transform(Position, car.Orientation);
        JVector worldAxis = JVector.Transform(Up, car.Orientation);

        JVector forward = -car.Orientation.GetColumn(2);
        JVector wheelFwd = JVector.Transform(
            forward,
            JMatrix.CreateRotationMatrix(worldAxis, SteerAngle)
        );

        JVector wheelLeft = JVector.Cross(worldAxis, wheelFwd);
        wheelLeft.Normalize();

        JVector wheelUp = JVector.Cross(wheelFwd, wheelLeft);

        float rayLen = 2.0f * Radius + WheelTravel;

        JVector wheelRayEnd = worldPos - Radius * worldAxis;
        JVector wheelRayOrigin = wheelRayEnd + rayLen * worldAxis;
        JVector wheelRayDelta = wheelRayEnd - wheelRayOrigin;

        float deltaFwd = 2.0f * Radius / (NumberOfRays + 1);
        float deltaFwdStart = deltaFwd;

        onFloor = false;

        JVector groundNormal = JVector.Zero;
        JVector groundPos = JVector.Zero;
        float deepestFrac = float.MaxValue;
        RigidBody worldBody = null!;

        for (int i = 0; i < NumberOfRays; i++)
        {
            float distFwd = deltaFwdStart + i * deltaFwd - Radius;
            float zOffset = Radius * (1.0f - (float)Math.Cos(Math.PI / 2.0f * (distFwd / Radius)));

            JVector newOrigin = wheelRayOrigin + distFwd * wheelFwd + zOffset * wheelUp;

            RigidBody body;

            bool result = world.Raycast(
                newOrigin,
                wheelRayDelta,
                rayCast,
                null,
                out Shape? shape,
                out JVector normal,
                out float frac
            );

            if (result && frac <= 1.0f)
            {
                body = shape!.RigidBody!;

                if (frac < deepestFrac)
                {
                    deepestFrac = frac;
                    groundPos = newOrigin + frac * wheelRayDelta;
                    worldBody = body;
                    groundNormal = normal;
                }

                onFloor = true;
            }
        }

        if (!onFloor)
            return;

        if (groundNormal.LengthSquared() > 0.0f)
            groundNormal.Normalize();

        displacement = rayLen * (1.0f - deepestFrac);
        displacement = Math.Clamp(displacement, 0.0f, WheelTravel);

        float displacementForceMag = displacement * Spring;

        // reduce force when suspension is par to ground
        displacementForceMag *= JVector.Dot(groundNormal, worldAxis);

        // apply damping
        float dampingForceMag = upSpeed * Damping;

        float totalForceMag = displacementForceMag + dampingForceMag;

        if (totalForceMag < 0.0f)
            totalForceMag = 0.0f;

        JVector extraForce = totalForceMag * worldAxis;

        force += extraForce;

        // side-slip friction and drive force. Work out wheel- and floor-relative coordinate frame
        JVector groundUp = groundNormal;
        JVector groundLeft = JVector.Cross(groundNormal, wheelFwd);
        if (groundLeft.LengthSquared() > 0.0f)
            groundLeft.Normalize();

        JVector groundFwd = JVector.Cross(groundLeft, groundUp);

        JVector wheelPointVel =
            car.Velocity
            + JVector.Cross(car.AngularVelocity, JVector.Transform(Position, car.Orientation));

        // rimVel=(wxr)*v
        JVector rimVel = angVel * JVector.Cross(wheelLeft, groundPos - worldPos);
        wheelPointVel += rimVel;

        if (worldBody == null)
            throw new Exception("car: world body is null.");

        JVector worldVel =
            worldBody.Velocity
            + JVector.Cross(worldBody.AngularVelocity, groundPos - worldBody.Position);

        wheelPointVel -= worldVel;

        // sideways forces
        float noslipVel = 0.2f;
        float slipVel = 0.4f;
        float slipFactor = 0.7f;

        float smallVel = 3.0f;
        float friction = SideFriction;

        float sideVel = JVector.Dot(wheelPointVel, groundLeft);

        if (sideVel > slipVel || sideVel < -slipVel)
        {
            friction *= slipFactor;
        }
        else if (sideVel > noslipVel || sideVel < -noslipVel)
        {
            friction *=
                1.0f
                - (1.0f - slipFactor) * (Math.Abs(sideVel) - noslipVel) / (slipVel - noslipVel);
        }

        if (sideVel < 0.0f)
            friction *= -1.0f;

        if (Math.Abs(sideVel) < smallVel)
            friction *= Math.Abs(sideVel) / smallVel;

        float sideForce = -friction * totalForceMag;

        extraForce = sideForce * groundLeft;
        force += extraForce;

        // fwd/back forces
        friction = ForwardFriction;
        float fwdVel = JVector.Dot(wheelPointVel, groundFwd);

        if (fwdVel > slipVel || fwdVel < -slipVel)
        {
            friction *= slipFactor;
        }
        else if (fwdVel > noslipVel || fwdVel < -noslipVel)
        {
            friction *=
                1.0f - (1.0f - slipFactor) * (Math.Abs(fwdVel) - noslipVel) / (slipVel - noslipVel);
        }

        if (fwdVel < 0.0f)
            friction *= -1.0f;

        if (Math.Abs(fwdVel) < smallVel)
            friction *= Math.Abs(fwdVel) / smallVel;

        float fwdForce = -friction * totalForceMag;

        extraForce = fwdForce * groundFwd;
        force += extraForce;

        // fwd force also spins the wheel
        JVector wheelCentreVel =
            car.Velocity
            + JVector.Cross(car.AngularVelocity, JVector.Transform(Position, car.Orientation));

        angVelForGrip = JVector.Dot(wheelCentreVel, groundFwd) / Radius;
        torque += -fwdForce * Radius;

        // add force to car
        car.AddForce(force, groundPos);

        // add force to the world
        if (!worldBody.IsStatic)
        {
            const float maxOtherBodyAcc = 500.0f;
            float maxOtherBodyForce = maxOtherBodyAcc * worldBody.Mass;

            if (force.LengthSquared() > (maxOtherBodyForce * maxOtherBodyForce))
                force *= maxOtherBodyForce / force.Length();

            worldBody.SetActivationState(true);

            worldBody.AddForce(force * -1, groundPos);
        }
    }

    private bool RayCastCallback(Shape shape)
    {
        return shape.RigidBody != car;
    }

    public Matrix4x4 GetMatrix()
    {
        JMatrix ori = car.Orientation;
        JVector pos = car.Position + JVector.Transform(Position, ori);

        JMatrix rot = JMatrix.CreateRotationY(SteerAngle) * JMatrix.CreateRotationX(-WheelRotation);

        ori *= rot;

        return new Matrix4x4(
            ori.M11,
            ori.M12,
            ori.M13,
            pos.X,
            ori.M21,
            ori.M22,
            ori.M23,
            pos.Y,
            ori.M31,
            ori.M32,
            ori.M33,
            pos.Z,
            0,
            0,
            0,
            1.0f
        );
    }
}
