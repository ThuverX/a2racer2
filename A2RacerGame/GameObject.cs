// Also world chunks, just anything that is in the world

using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

class GameObject
{
    protected Vector3 position = new(0, 0, 0);
    protected Quaternion rotation = Quaternion.Identity;

    public GameObject(Vector3 position)
    {
        this.position = position;
    }

    public virtual void Update() { }

    public virtual void Render()
    {
        DrawPoint3D(GetPosition(), Color.Red);
    }

    public virtual void SetPosition(Vector3 position)
    {
        this.position = position;
    }

    public virtual void SetRotation(Quaternion rotation)
    {
        this.rotation = rotation;
    }

    public virtual Vector3 GetPosition()
    {
        return position;
    }

    public virtual Quaternion GetRotation()
    {
        return rotation;
    }
}
