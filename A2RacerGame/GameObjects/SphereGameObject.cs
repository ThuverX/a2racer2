using System.Numerics;
using Jitter2.Collision.Shapes;
using Raylib_cs;
using static Raylib_cs.Raylib;

class SphereGameObject : PhysicsGameObject
{
    private float radius;
    private Model model;

    public SphereGameObject(Vector3 position, float radius)
        : base(position)
    {
        this.radius = radius;

        body.IsStatic = false;
        body.AddShape(new SphereShape(radius));

        model = LoadModelFromMesh(GenMeshSphere(radius, 16, 16));
    }

    public void SetRadius(float radius)
    {
        this.radius = radius;
    }

    public float GetRadius()
    {
        return radius;
    }

    public override void Render()
    {
        model.Transform = Matrix4x4.CreateFromQuaternion(GetRotation());
        DrawModel(model, GetPosition(), 1.0f, Color.White);
    }
}
