using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

class PlaneGameObject : PhysicsGameObject
{
    private Vector2 size = new(1, 1);

    public PlaneGameObject(Vector3 position, Vector2 size)
        : base(position)
    {
        SetSize(size);
    }

    public void SetSize(Vector2 size)
    {
        this.size = size;
    }

    public Vector2 GetSize()
    {
        return size;
    }

    public override void Render()
    {
        DrawPlane(position, size, Color.White);
    }
}
