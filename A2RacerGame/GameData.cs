using ImageMagick;
using Raylib_cs;
using static Raylib_cs.Raylib;

class GameData
{
    public enum StaticTexureLocation
    {
        NO_TEXTURE,
        ROAD_TEXTURE,
        LAND_TEXTURE,
        EXTRA_TEXTURE,
        RAIL_TEXTURE
    }

    public enum StaticMaterialLocation
    {
        GENERIC
    }

    private Dictionary<StaticMaterialLocation, Material> materials = new();

    private Dictionary<string, Texture2D> textures = new();
    private Dictionary<string, Archive> archives = new();
    private Dictionary<StaticTexureLocation, Texture2D> staticTextures = new();
    private EtappeData? etappe;

    public void Load()
    {
        archives["rcs"] = new Archive("rcs");

        LoadEtappe("etappe0", 0);
        staticTextures[StaticTexureLocation.NO_TEXTURE] = LoadTextureFromImage(
            GenImageChecked(64, 64, 8, 8, Color.DarkPurple, Color.Black)
        );

        Shader world_generic = LoadShader(
            Path.Combine(Game.GameFolder, "shaders", "world_generic.vert"),
            Path.Combine(Game.GameFolder, "shaders", "world_generic.frag")
        );

        Material genericMaterial = LoadMaterialDefault();
        genericMaterial.Shader = world_generic;

        SetMaterialTexture(
            ref genericMaterial,
            MaterialMapIndex.Specular,
            Game.GameCamera.GetRenderTexture().Depth
        );

        SetMaterialTexture(
            ref genericMaterial,
            MaterialMapIndex.Normal,
            Game.Sun.GetRenderTexture().Depth
        );

        materials[StaticMaterialLocation.GENERIC] = genericMaterial;
    }

    public Archive GetArchive(string name)
    {
        return archives[name];
    }

    private static byte[] TgaToPng(byte[] tgaData)
    {
        using MagickImage image = new(tgaData, MagickFormat.Tga);

        image.Format = MagickFormat.Png;
        return image.ToByteArray();
    }

    public Texture2D GetOrLoadTexture(string archiveName, string name)
    {
        Archive archive = GetArchive(archiveName);
        if (!archive.HasEntry(name + ".tga"))
            throw new Exception($"Entry \"{name}\" not found in archive \"{archiveName}\"");

        if (HasTexture(name))
            return textures[name];

        Archive.ArchiveEntry tgaEntry = archive.GetEntry(name + ".tga")!;

        Image img = LoadImageFromMemory(".png", TgaToPng(tgaEntry.data));
        Texture2D texture = LoadTextureFromImage(img);
        UnloadImage(img);

        textures[name] = texture;
        return texture;
    }

    public bool HasTexture(string name)
    {
        return textures.ContainsKey(name);
    }

    public Texture2D GetTexture(string name)
    {
        return textures[name];
    }

    public void SetStaticTexture(StaticTexureLocation location, Texture2D texture)
    {
        staticTextures[location] = texture;
    }

    public Texture2D GetStaticTexture(StaticTexureLocation location)
    {
        return staticTextures[location];
    }

    public Material GetMaterial(StaticMaterialLocation location)
    {
        return materials[location];
    }

    public void LoadEtappe(string name, int index)
    {
        etappe = new EtappeData(name, index);
    }

    public EtappeData GetEtappe()
    {
        return etappe!;
    }
}
