using System.Numerics;
using Jitter2;
using Jitter2.Collision.Shapes;
using Jitter2.LinearMath;
using Raylib_cs;
using static Raylib_cs.Raylib;

class CarGameObject : PhysicsGameObject
{
    private CarConfig config;
    private Model carModel;
    private Model wheelModel;

    private float destSteering;
    private float destAccelerate;
    private float steering;
    private float accelerate;

    // The maximum steering angle in degrees
    // for both front wheels
    public float SteerAngle { get; set; }

    // The maximum torque which is applied to the
    // car when accelerating.
    public float DriveTorque { get; set; }

    // Lower/Higher the acceleration of the car.
    public float AccelerationRate { get; set; }

    // Lower/Higher the steering rate of the car.
    public float SteerRate { get; set; }

    // don't damp perfect, allow some bounciness.
    private const float dampingFrac = 0.7f;
    private const float springFrac = 0.9f;

    public CarGameObject(Vector3 position, CarConfig config)
        : base(position)
    {
        this.config = config;

        body.IsStatic = false;

        Load();
        LoadCollision();
    }

    private unsafe void Load()
    {
        carModel = LoadModelFromMesh(
            Util.D3DToMesh(
                Game.GameDataManager.GetArchive("rcs").GetEntry(config.model + ".d3d").GetModel()!
            )
        );

        carModel.Materials[0] = Game.GameDataManager.GetMaterial(
            GameData.StaticMaterialLocation.GENERIC
        );

        wheelModel = LoadModelFromMesh(
            Util.D3DToMesh(Game.GameDataManager.GetArchive("rcs").GetEntry("wiel.d3d").GetModel()!)
        );

        wheelModel.Materials[0] = Game.GameDataManager.GetMaterial(
            GameData.StaticMaterialLocation.GENERIC
        );
    }

    private void LoadCollision()
    {
        World world = Game.GameWorldManager.GetPhysicsWorld();

        AccelerationRate = config.accelerationRate;
        SteerAngle = 40.0f * DEG2RAD;
        DriveTorque = config.driveTorque;
        SteerRate = config.steerRate;

        body.AddShape(
            new TransformedShape(
                new SphereShape(config.collisionShape.Y / 2f),
                new JVector(
                    config.collisionShape.X / 2f - config.collisionShape.Y / 2f,
                    config.centerOfMassHeight + 0.5f,
                    config.collisionShape.Z / 2f - config.collisionShape.Y / 2f
                )
            )
        );

        body.AddShape(
            new TransformedShape(
                new SphereShape(config.collisionShape.Y / 2f),
                new JVector(
                    -config.collisionShape.X / 2f + config.collisionShape.Y / 2f,
                    config.centerOfMassHeight + 0.5f,
                    config.collisionShape.Z / 2f - config.collisionShape.Y / 2f
                )
            )
        );

        body.AddShape(
            new TransformedShape(
                new SphereShape(config.collisionShape.Y / 2f),
                new JVector(
                    config.collisionShape.X / 2f - config.collisionShape.Y / 2f,
                    config.centerOfMassHeight + 0.5f,
                    -config.collisionShape.Z / 2f + config.collisionShape.Y / 2f
                )
            )
        );

        body.AddShape(
            new TransformedShape(
                new SphereShape(config.collisionShape.Y / 2f),
                new JVector(
                    -config.collisionShape.X / 2f + config.collisionShape.Y / 2f,
                    config.centerOfMassHeight + 0.5f,
                    -config.collisionShape.Z / 2f + config.collisionShape.Y / 2f
                )
            )
        );

        body.AddShape(
            new TransformedShape(
                new SphereShape(config.collisionShape.Y / 2f),
                new JVector(0, config.centerOfMassHeight + 0.5f, 0)
            )
        );

        float mass = config.mass / 10f;
        JVector sides = new(config.collisionShape.X, 1f, config.collisionShape.Z);

        float Ixx = (1.0f / 12.0f) * mass * (sides.Y * sides.Y + sides.Z * sides.Z);
        float Iyy = (1.0f / 12.0f) * mass * (sides.X * sides.X + sides.Z * sides.Z);
        float Izz = (1.0f / 12.0f) * mass * (sides.X * sides.X + sides.Y * sides.Y);

        JMatrix inertia = new(Ixx, 0, 0, 0, Iyy, 0, 0, 0, Izz);
        JVector r = new(0, 0, 0);
        inertia += mass * r.LengthSquared() * JMatrix.Identity - mass * JVector.Outer(r, r);

        body.SetMassInertia(inertia, mass);

        for (int i = 0; i < 4; i++)
        {
            Wheels[i] = new Wheel(
                world,
                body,
                new JVector(
                    config.wheelPositions[i].X,
                    config.wheelPositions[i].Y,
                    config.wheelPositions[i].Z
                ),
                config.wheelRadius,
                config.wheelConfig
            );
        }

        AdjustWheelValues();
    }

    // Should be called after editing wheel values.
    public void AdjustWheelValues()
    {
        World world = Game.GameWorldManager.GetPhysicsWorld();
        float mass = body.Mass / 4.0f;
        float wheelMass = body.Mass * 0.03f;

        foreach (Wheel w in Wheels)
        {
            w.Inertia = 0.5f * (w.Radius * w.Radius) * wheelMass;
            w.Spring = mass * world.Gravity.Length() / (w.WheelTravel * springFrac);
            w.Damping = 2.0f * (float)Math.Sqrt(w.Spring * body.Mass) * 0.25f * dampingFrac;
        }
    }

    public Wheel[] Wheels { get; } = new Wheel[4];

    // Set input values for the car.
    // Accelerate:
    // A value between -1 and 1 (other values get clamped). Adjust
    // the maximum speed of the car by setting DriveTorque. The maximum acceleration is adjusted
    // by settingAccelerationRate
    // Steer:
    // A value between -1 and 1 (other values get clamped). Adjust
    // the maximum steer angle by setting SteerAngle. The speed of steering
    // change is adjusted by SteerRate.
    public void SetInput(float accelerate, float steer)
    {
        destAccelerate = accelerate;
        destSteering = steer * 0.4f;

        body.SetActivationState(true);
    }

    public void Step(float timestep)
    {
        foreach (Wheel w in Wheels)
            w.PreStep(timestep);

        float deltaAccelerate = timestep * AccelerationRate;

        float deltaSteering = timestep * SteerRate;

        float dAccelerate = destAccelerate - accelerate;
        dAccelerate = Math.Clamp(dAccelerate, -deltaAccelerate, deltaAccelerate);

        float dSteering = destSteering - steering;
        dSteering = Math.Clamp(dSteering, -deltaSteering, deltaSteering);

        accelerate += dAccelerate;
        steering += dSteering;

        float maxTorque = DriveTorque * 0.5f;

        foreach (Wheel w in Wheels)
        {
            w.AddTorque(maxTorque * accelerate);

            if (destAccelerate == 0.0f && w.AngularVelocity < 0.8f)
            {
                // if the car is slow enough and destAccelerate is zero
                // apply torque in the opposite direction of the angular velocity
                // to make the car come to a complete halt.
                w.AddTorque(-w.AngularVelocity);
            }
        }

        float alpha = SteerAngle * steering;

        Wheels[0].SteerAngle = alpha;
        Wheels[1].SteerAngle = alpha;

        foreach (Wheel w in Wheels)
        {
            w.PostStep(timestep);
        }
    }

    public override void Update()
    {
        Step(1f / Game.UpdateFrameRate);
    }

    public float GetSpeed()
    {
        return body.Velocity.Length() * 3.6f;
    }

    public Vector3 GetForward()
    {
        Quaternion q = GetRotation();
        Vector3 forward = Vector3.Transform(Vector3.UnitZ, q);

        return -Vector3.Normalize(forward);
    }

    public override unsafe void Render()
    {
        SetMaterialTexture(
            ref carModel.Materials[0],
            MaterialMapIndex.Diffuse,
            Game.GameDataManager.GetOrLoadTexture("rcs", config.texture)
        );

        carModel.Transform = Util.GetRayLibTransformMatrix(body);
        carModel.Transform *= Matrix4x4.CreateRotationY(MathF.PI);
        DrawModel(carModel, Vector3.Zero, 1f, Color.White);

        SetMaterialTexture(
            ref wheelModel.Materials[0],
            MaterialMapIndex.Diffuse,
            Game.GameDataManager.GetOrLoadTexture("rcs", "cars01")
        );

        foreach (Wheel w in Wheels)
        {
            wheelModel.Transform = w.GetMatrix();
            DrawModel(wheelModel, Vector3.Zero, 1f, Color.White);
        }
        {
            return;
            // draw all collision sphere

            Color color = new(255, 0, 0, 100);

            DrawSphere(
                new Vector3(0, config.centerOfMassHeight + 0.5f, 0) + GetPosition(),
                config.collisionShape.Y / 2f,
                color
            );

            DrawSphere(
                new Vector3(
                    config.collisionShape.X / 2f - config.collisionShape.Y / 2f,
                    config.centerOfMassHeight + 0.5f,
                    config.collisionShape.Z / 2f - config.collisionShape.Y / 2f
                ) + GetPosition(),
                config.collisionShape.Y / 2f,
                color
            );

            DrawSphere(
                new Vector3(
                    -config.collisionShape.X / 2f + config.collisionShape.Y / 2f,
                    config.centerOfMassHeight + 0.5f,
                    config.collisionShape.Z / 2f - config.collisionShape.Y / 2f
                ) + GetPosition(),
                config.collisionShape.Y / 2f,
                color
            );

            DrawSphere(
                new Vector3(
                    config.collisionShape.X / 2f - config.collisionShape.Y / 2f,
                    config.centerOfMassHeight + 0.5f,
                    -config.collisionShape.Z / 2f + config.collisionShape.Y / 2f
                ) + GetPosition(),
                config.collisionShape.Y / 2f,
                color
            );

            DrawSphere(
                new Vector3(
                    -config.collisionShape.X / 2f + config.collisionShape.Y / 2f,
                    config.centerOfMassHeight + 0.5f,
                    -config.collisionShape.Z / 2f + config.collisionShape.Y / 2f
                ) + GetPosition(),
                config.collisionShape.Y / 2f,
                color
            );
        }

        return;
        { // debug
            Quaternion q = GetRotation();
            Vector3 forward = Vector3.Transform(Vector3.UnitZ, q);

            Vector3 center =
                new Vector3(
                    body.Shapes[0].GeometricCenter.X,
                    body.Shapes[0].GeometricCenter.Y,
                    body.Shapes[0].GeometricCenter.Z
                )
                + GetPosition()
                + new Vector3(0, 0.6f, 0);

            DrawSphere(center, 0.2f, Color.Red);

            forward = Vector3.Normalize(forward);

            Vector3 endpoint = center + forward * 2.0f * (-accelerate);

            DrawCapsule(center, endpoint, 0.07f, 16, 16, Color.Green);

            DrawCube(
                new Vector3(
                    body.Shapes[0].GeometricCenter.X,
                    body.Shapes[0].GeometricCenter.Y,
                    body.Shapes[0].GeometricCenter.Z
                ) + GetPosition(),
                config.collisionShape.X,
                config.collisionShape.Y,
                config.collisionShape.Z,
                new Color(255, 0, 0, 100)
            );
        }
    }
}
