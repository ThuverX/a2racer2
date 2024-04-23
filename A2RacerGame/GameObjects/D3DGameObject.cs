using System.Numerics;
using Jitter2.Collision;
using Jitter2.Collision.Shapes;
using Jitter2.LinearMath;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Raymath;

class D3DGameObject : PhysicsGameObject
{
    private D3dFile? d3dFile;
    private OmpFile.StaticObject staticObject;
    private Model model;
    private Texture2D texture;
    private bool isVisible = true;

    private BoundingBox bbox;

    public D3DGameObject(OmpFile.StaticObject staticObject)
        : base(Vector3.One)
    {
        this.staticObject = staticObject;

        body.IsStatic = staticObject.GetObjectType() != 1;

        Load();
    }

    private void LoadD3DFile()
    {
        string objectName = staticObject.GetAnimname().Replace("_ANIM", "").Trim();
        d3dFile = Game.GameDataManager.GetArchive("rcs").GetEntry(objectName + ".d3d").GetModel()!;

        SetPosition(
            new Vector3(
                staticObject.GetX() / GameWorld.WORLD_SIZE,
                staticObject.GetY() / GameWorld.WORLD_SIZE,
                staticObject.GetZ() / GameWorld.WORLD_SIZE
            )
        );

        Vector3 rotation = new Vector3(
            staticObject.GetRotX(),
            -staticObject.GetRotY(),
            staticObject.GetRotZ()
        );

        SetRotation(
            Quaternion.CreateFromYawPitchRoll(
                rotation.Y * DEG2RAD,
                rotation.X * DEG2RAD,
                rotation.Z * DEG2RAD
            )
        );
    }

    private void LoadTexture()
    {
        string objectName = staticObject.GetAnimname().Replace("_ANIM", "").Trim();
        string textureName = TextureLookup.GetTextureForModel(objectName);
        if (Game.GameDataManager.GetArchive("rcs").HasEntry(textureName + ".tga"))
        {
            texture = Game.GameDataManager.GetOrLoadTexture("rcs", textureName);
        }
        else
        {
            Console.WriteLine(
                $"[D3DGameObject] Texture not found for object {objectName} in archive rcs"
            );
            texture = Game.GameDataManager.GetStaticTexture(
                GameData.StaticTexureLocation.NO_TEXTURE
            );
        }
    }

    private void GenCollisionMesh()
    {
        // float width = bbox.Max.X - bbox.Min.X;
        // float height = bbox.Max.Y - bbox.Min.Y;
        // float depth = bbox.Max.Z - bbox.Min.Z;

        // BoxShape shape = new(Math.Abs(width), Math.Abs(height), Math.Abs(depth));

        // if (shape.Mass > 0)
        //     body.AddShape(shape);
    }

    private unsafe void Load()
    {
        LoadD3DFile();

        model = LoadModelFromMesh(Util.D3DToMesh(d3dFile!));
        bbox = GetModelBoundingBox(model);

        GenCollisionMesh();
        LoadTexture();

        model.Materials[0] = Game.GameDataManager.GetMaterial(
            GameData.StaticMaterialLocation.GENERIC
        );
    }

    public override void Update()
    {
        isVisible = Util.IsWrappedBetween(
            staticObject.GetChunk(),
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

        body.SetActivationState(isVisible);
    }

    public override unsafe void Render()
    {
        if (!isVisible)
            return;

        SetMaterialTexture(ref model.Materials[0], MaterialMapIndex.Diffuse, texture);

        model.Transform = Matrix4x4.CreateFromQuaternion(GetRotation());
        DrawModel(model, GetPosition(), 1f, Color.White);
    }
}
