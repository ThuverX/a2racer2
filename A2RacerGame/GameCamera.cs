using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

class GameCamera
{
    private RenderTexture2D renderTexture;
    private Camera3D camera =
        new()
        {
            Position = new Vector3(0, 0, 0),
            Up = new Vector3(0, 1, 0),
            FovY = 85.0f,
            Projection = CameraProjection.Perspective
        };

    public GameCamera()
    {
        renderTexture = LoadRenderTexture(GetRenderWidth(), GetRenderHeight());
    }

    public void SetPosition(Vector3 position)
    {
        camera.Position = position;
    }

    public Vector3 GetPosition()
    {
        return camera.Position;
    }

    public Camera3D GetCamera()
    {
        return camera;
    }

    public RenderTexture2D GetRenderTexture()
    {
        return renderTexture;
    }

    private float distance = 4f;
    private float height = 2f;
    private float targetHeight = 0f;
    private float targetForward = 2f;

    private bool debug = false;

    public void Update()
    {
        if (debug)
        {
            UpdateCamera(ref camera, CameraMode.Free);
            return;
        }

        CarGameObject obj = Game.GameWorldManager.GetCar();

        if (obj == null)
            return;

        Vector3 pos = obj.GetPosition();
        Vector3 forward = obj.GetForward();

        camera.Position = pos - forward * distance + new Vector3(0.0f, height, 0.0f);
        camera.Target = pos + forward * targetForward + new Vector3(0.0f, targetHeight, 0.0f);
    }
}
