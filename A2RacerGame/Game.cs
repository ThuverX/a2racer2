using Raylib_cs;
using static Raylib_cs.Raylib;

class Game
{
    public static string GameFolder = "X:/a2racer/data/";

    public static GameCamera GameCamera = new();
    public static GameData GameDataManager = new();
    public static GameWorld GameWorldManager = new();
    public static GameSun Sun = new(new(0, 100, 0), new(-1572, 0, 2355));

    public static GameUI GameUIManager = new();

    public static float TimeSinceLastUpdate = 0.0f;
    public const float UpdateFrameRate = 60.0f;

    public static void Start()
    {
        InitWindow(1920, 1080, "A2RacerGame");
        DisableCursor();
        SetTargetFPS(1000);

        Load();

        while (!WindowShouldClose())
        {
            if (TimeSinceLastUpdate >= 1.0f / UpdateFrameRate)
            {
                Update();
                TimeSinceLastUpdate = 0;
            }

            Render();

            TimeSinceLastUpdate += GetFrameTime();
        }
    }

    public static void Load()
    {
        GameDataManager.Load();
        GameWorldManager.Load();
    }

    public static void Update()
    {
        Sun.Update();
        GameCamera.Update();
        GameWorldManager.Update();
        GameUIManager.Update();
    }

    public static void Render()
    {
        // Sun.Render();

        BeginDrawing();

        GameCamera.Update();

        BeginTextureMode(GameCamera.GetRenderTexture());

        ClearBackground(Color.Gray);
        BeginMode3D(GameCamera.GetCamera());

        GameWorldManager.Render();
        EndMode3D();

        EndTextureMode();

        RenderMain();

        GameUIManager.Render();

        // DebugRender();

        EndDrawing();
    }

    private static void RenderMain()
    {
        if (IsRenderTextureReady(GameCamera.GetRenderTexture()))
            DrawTextureRec(
                GameCamera.GetRenderTexture().Texture,
                new(
                    0,
                    0,
                    GameCamera.GetRenderTexture().Texture.Width,
                    -GameCamera.GetRenderTexture().Texture.Height
                ),
                new(0, 0),
                Color.White
            );
    }

    public static void DebugRender()
    {
        if (IsRenderTextureReady(Sun.GetRenderTexture()))
            DrawTextureRec(
                Sun.GetRenderTexture().Texture,
                new(
                    0,
                    0,
                    Sun.GetRenderTexture().Texture.Width,
                    -Sun.GetRenderTexture().Texture.Height
                ),
                new(0, 0),
                Color.White
            );
    }

    public static void Close() { }
}
