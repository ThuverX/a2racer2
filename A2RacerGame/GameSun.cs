using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

class GameSun
{
    private RenderTexture2D renderTexture;
    private Camera3D camera =
        new()
        {
            Position = new Vector3(0, 0, 0),
            Up = new Vector3(0.0f, 1.0f, 0.0f),
            FovY = 60f,
            Projection = CameraProjection.Orthographic
        };

    public Shader lightDepthShader = LoadShader(
        Path.Combine(Game.GameFolder, "shaders", "light_depth_shader.vert"),
        Path.Combine(Game.GameFolder, "shaders", "blit.frag")
    );

    private int modelLoc = -1;
    private int lightSpaceMatrixLoc = -1;

    public GameSun(Vector3 position, Vector3 target)
    {
        camera.Target = target;
        camera.Position = position;
        renderTexture = LoadRenderTexture(1024, 1024);

        modelLoc = GetShaderLocation(lightDepthShader, "model");
        lightSpaceMatrixLoc = GetShaderLocation(lightDepthShader, "lightSpaceMatrix");

        unsafe
        {
            lightDepthShader.Locs[(int)ShaderLocationIndex.MatrixModel] = modelLoc;
        }
    }

    public RenderTexture2D GetRenderTexture()
    {
        return renderTexture;
    }

    public void SetPosition(Vector3 position)
    {
        camera.Position = position;
    }

    public Vector3 GetPosition()
    {
        return camera.Position;
    }

    public void SetTarget(Vector3 target)
    {
        camera.Target = target;
    }

    public Vector3 GetTarget()
    {
        return camera.Target;
    }

    public void Update()
    {
        Vector3 floor = Game.GameCamera.GetPosition() * new Vector3(1, 0, 1);
        SetPosition(floor + new Vector3(200, 100, 0));
        SetTarget(floor);
    }

    public Matrix4x4 GetViewMatrix()
    {
        return GetCameraViewMatrix(ref camera);
    }

    public Matrix4x4 GetProjectionMatrix()
    {
        return GetCameraProjectionMatrix(ref camera, 60f);
    }

    public Matrix4x4 GetLightSpaceMatrix()
    {
        return GetViewMatrix() * GetProjectionMatrix();
    }

    public void Render()
    {
        renderTexture.Texture = renderTexture.Depth;

        SetShaderValueMatrix(
            lightDepthShader,
            lightSpaceMatrixLoc,
            Matrix4x4.CreateRotationX(0.9f)
        );

        Material mat = Game.GameDataManager.GetMaterial(GameData.StaticMaterialLocation.GENERIC);
        Shader og = mat.Shader;
        mat.Shader = lightDepthShader;

        BeginTextureMode(renderTexture);
        BeginMode3D(camera);
        ClearBackground(Color.Black);

        Game.GameWorldManager.Render();

        EndMode3D();
        EndTextureMode();

        mat.Shader = og;
    }
}
