using System.Numerics;
using Jitter2;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Jitter2.LinearMath;
using Raylib_cs;
using static Raylib_cs.Raylib;

class GameWorld
{
    public const float WORLD_SIZE = 10.0f;
    private List<GameObject> gameObjects = new();
    private World physicsWorld = new();
    private CarGameObject playerCar;

    public const int MAX_CHUNKS = 60;
    public const int MAX_COLLISION_CHUNKS = 10;
    private int chunkID = 0;

    public void AddToWorld(GameObject obj)
    {
        gameObjects.Add(obj);
    }

    public int GetChunkID()
    {
        return chunkID;
    }

    public World GetPhysicsWorld()
    {
        return physicsWorld;
    }

    public void Load()
    {
        foreach (var chunk in Game.GameDataManager.GetEtappe().GetChunks())
        {
            AddToWorld(new WorldChunkGameObject(chunk));
        }

        foreach (var obj in Game.GameDataManager.GetEtappe().GetObjects())
        {
            AddToWorld(new D3DGameObject(obj));
        }

        Vector3 start = new(-1572, 10, 2355);

        Game.GameCamera.SetPosition(start);

        playerCar = new CarGameObject(start, CarConfig.Cars.bmw);

        AddToWorld(playerCar);

        // Subfloor seems to stabilize the physics world
        RigidBody worldBody = physicsWorld.CreateRigidBody();
        worldBody.Position = new JVector(0, -10, 0);
        worldBody.IsStatic = true;
        worldBody.AddShape(new BoxShape(10000, 1f, 10000));
    }

    public void Update()
    {
        physicsWorld.Step(1f / Game.UpdateFrameRate, true);

        int closestChunk = 0;
        float closestDistance = float.MaxValue;

        foreach (GameObject obj in gameObjects)
        {
            obj.Update();

            if (obj is WorldChunkGameObject)
            {
                WorldChunkGameObject chunk = (WorldChunkGameObject)obj;
                Vector3 pos = Game.GameCamera.GetPosition();

                float distance = Vector3.Distance(pos, chunk.GetPosition());
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestChunk = chunk.GetChunkID();
                }
            }
        }

        chunkID = closestChunk;

        float steer = 0.0f;
        float acc = 0.0f;

        if (IsKeyDown(KeyboardKey.Y))
        {
            AddToWorld(new SphereGameObject(playerCar.GetPosition() + new Vector3(0, 2, 0), 0.5f));
        }

        if (IsKeyDown(KeyboardKey.I))
        {
            acc = 1.0f;
        }

        if (IsKeyDown(KeyboardKey.K))
        {
            acc = -1.0f;
        }

        if (IsKeyDown(KeyboardKey.J))
        {
            steer = 1.0f;
        }

        if (IsKeyDown(KeyboardKey.L))
        {
            steer = -1.0f;
        }

        if (playerCar != null)
            playerCar.SetInput(acc, steer);
    }

    public CarGameObject GetCar()
    {
        return playerCar;
    }

    public void Render()
    {
        foreach (GameObject obj in gameObjects)
        {
            obj.Render();
        }
    }
}
