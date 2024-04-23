using System.Numerics;
using Jitter2.Dynamics;
using Jitter2.LinearMath;
using Raylib_cs;
using static Raylib_cs.Raylib;

class PhysicsGameObject : GameObject
{
    protected RigidBody body = Game.GameWorldManager.GetPhysicsWorld().CreateRigidBody();

    public PhysicsGameObject(Vector3 position)
        : base(position)
    {
        body.Position = new JVector(position.X, position.Y, position.Z);
    }

    public override void SetPosition(Vector3 position)
    {
        body.Position = new JVector(position.X, position.Y, position.Z);
    }

    public override Vector3 GetPosition()
    {
        JVector pos = body.Position;
        return new Vector3(pos.X, pos.Y, pos.Z);
    }

    public override void SetRotation(Quaternion rotation)
    {
        body.Orientation = JMatrix.CreateFromQuaternion(
            new(rotation.X, rotation.Y, rotation.Z, rotation.W)
        );
    }

    public override Quaternion GetRotation()
    {
        JMatrix orientation = body.Orientation;
        JQuaternion quat = JQuaternion.CreateFromMatrix(orientation);

        return new Quaternion(quat.X, quat.Y, quat.Z, quat.W);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Render()
    {
        base.Render();
    }
}
