using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

class DebugLineObject : GameObject
{
    private Vector3 endPosition;
    private Color color;

    public DebugLineObject(Vector3 position, Vector3 endPosition, Color color)
        : base(position)
    {
        this.endPosition = endPosition;
        this.color = color;
    }

    public override void Render()
    {
        DrawLine3D(GetPosition(), endPosition, color);
    }
}
