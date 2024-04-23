using System.Numerics;
using Jitter2.Collision;
using Jitter2.Collision.Shapes;
using Jitter2.LinearMath;
using Raylib_cs;
using static Raylib_cs.Raylib;

class WorldChunkGameObject : PhysicsGameObject
{
    private ChunkData chunkData;
    private Model roadModel;
    private Model landModel;
    private Model extraModel;
    private Model railModel;
    private bool isVisible = true;

    public WorldChunkGameObject(ChunkData chunkData)
        : base(Vector3.Zero)
    {
        this.chunkData = chunkData;

        body.IsStatic = true;

        GenerateModel();
        GenerateCollisionShape();
    }

    private unsafe void GenerateModel()
    {
        roadModel = LoadModelFromMesh(chunkData.GetRoadMeshData().ToMesh());
        roadModel.Materials[0] = Game.GameDataManager.GetMaterial(
            GameData.StaticMaterialLocation.GENERIC
        );

        landModel = LoadModelFromMesh(chunkData.GetLandMeshData().ToMesh());
        landModel.Materials[0] = Game.GameDataManager.GetMaterial(
            GameData.StaticMaterialLocation.GENERIC
        );

        extraModel = LoadModelFromMesh(chunkData.GetExtraMeshData().ToMesh());
        extraModel.Materials[0] = Game.GameDataManager.GetMaterial(
            GameData.StaticMaterialLocation.GENERIC
        );

        railModel = LoadModelFromMesh(chunkData.GetRailMeshData().ToMesh());
        railModel.Materials[0] = Game.GameDataManager.GetMaterial(
            GameData.StaticMaterialLocation.GENERIC
        );
    }

    private unsafe void GenerateCollisionMeshForTriangles(List<JTriangle> jTriangles)
    {
        TriangleMesh jtm = new(jTriangles);

        for (int i = 0; i < jtm.Indices.Length; i++)
        {
            TriangleShape ts = new(jtm, i);
            body.AddShape(ts);
        }
    }

    private unsafe void GenerateCollisionShape()
    {
        GenerateCollisionMeshForTriangles(chunkData.GetRoadMeshData().GetCollisionTriangles());
        GenerateCollisionMeshForTriangles(chunkData.GetLandMeshData().GetCollisionTriangles());
        GenerateCollisionMeshForTriangles(chunkData.GetExtraMeshData().GetCollisionTriangles());
        GenerateCollisionMeshForTriangles(chunkData.GetRailMeshData().GetCollisionTriangles());
    }

    public override void Update()
    {
        isVisible = Util.IsWrappedBetween(
            GetChunkID(),
            Util.Wrap(
                Game.GameWorldManager.GetChunkID() - GameWorld.MAX_CHUNKS,
                0,
                Game.GameDataManager.GetEtappe().GetChunkCount()
            ),
            Util.Wrap(
                Game.GameWorldManager.GetChunkID() + GameWorld.MAX_CHUNKS,
                0,
                Game.GameDataManager.GetEtappe().GetChunkCount()
            )
        );
    }

    public override unsafe void Render()
    {
        if (!isVisible)
            return;

        SetMaterialTexture(
            ref roadModel.Materials[0],
            MaterialMapIndex.Diffuse,
            Game.GameDataManager.GetStaticTexture(GameData.StaticTexureLocation.ROAD_TEXTURE)
        );

        DrawModel(roadModel, Vector3.Zero, 1.0f, Color.White);

        SetMaterialTexture(
            ref landModel.Materials[0],
            MaterialMapIndex.Diffuse,
            Game.GameDataManager.GetStaticTexture(GameData.StaticTexureLocation.LAND_TEXTURE)
        );

        DrawModel(landModel, Vector3.Zero, 1.0f, Color.White);

        SetMaterialTexture(
            ref extraModel.Materials[0],
            MaterialMapIndex.Diffuse,
            Game.GameDataManager.GetStaticTexture(GameData.StaticTexureLocation.EXTRA_TEXTURE)
        );

        DrawModel(extraModel, Vector3.Zero, 1.0f, Color.White);

        SetMaterialTexture(
            ref railModel.Materials[0],
            MaterialMapIndex.Diffuse,
            Game.GameDataManager.GetStaticTexture(GameData.StaticTexureLocation.RAIL_TEXTURE)
        );

        DrawModel(railModel, Vector3.Zero, 1.0f, Color.White);
    }

    public int GetChunkID()
    {
        return chunkData.GetChunkIndex();
    }

    public override Vector3 GetPosition()
    {
        BoundingBox box = chunkData.GetBoundingBox();
        return (box.Min + box.Max) / 2;
    }

    public BoundingBox GetBoundingBox()
    {
        return chunkData.GetBoundingBox();
    }
}
