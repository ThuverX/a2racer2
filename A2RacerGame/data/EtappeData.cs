using Raylib_cs;

class EtappeData
{
    public Dictionary<int, EtappeInfo> StaticEtappeInfo =
        new()
        {
            {
                0,
                new EtappeInfo(
                    EtappeType.CITY,
                    "etappe0",
                    "e00_weg",
                    "e00_gras",
                    "e00_gra2",
                    "e00_vang",
                    "scrl_lo",
                    "scrl_hi",
                    "AMSTERDAM",
                    0,
                    10000
                )
            },
            {
                1,
                new EtappeInfo(
                    EtappeType.HIGHWAY,
                    "etappe1",
                    "e01_weg",
                    "e01_gras",
                    "e01_gra2",
                    "e01_vang",
                    "scrl_lo",
                    "scrl_hi",
                    "AMSTERDAM - DEN HAAG",
                    5,
                    10000
                )
            },
            {
                2,
                new EtappeInfo(
                    EtappeType.CITY,
                    "etappe2",
                    "e02_weg",
                    "e02_gras",
                    "e02_gra2",
                    "e02_vang",
                    "scrl_lo",
                    "scrl_hi",
                    "DEN HAAG",
                    4,
                    10000
                )
            },
            {
                3,
                new EtappeInfo(
                    EtappeType.HIGHWAY,
                    "etappe3",
                    "e03_weg",
                    "e03_gras",
                    "e03_gra2",
                    "e03_vang",
                    "scrl_lo",
                    "scrl_hi",
                    "DEN HAAG - ROTTERDAM",
                    0,
                    10000
                )
            },
            {
                4,
                new EtappeInfo(
                    EtappeType.CITY,
                    "etappe4",
                    "e04_weg",
                    "e04_gras",
                    "e04_gra2",
                    "e04_vang",
                    "scrl_lo",
                    "scrl_hi",
                    "ROTTERDAM",
                    1,
                    10000
                )
            },
            {
                5,
                new EtappeInfo(
                    EtappeType.HIGHWAY,
                    "etappe5",
                    "e05_weg",
                    "e05_gras",
                    "e05_gra2",
                    "e05_vang",
                    "scrl_lo",
                    "scrl_hi",
                    "ROTTERDAM - AMSTERDAM",
                    3,
                    10000
                )
            },
            {
                6,
                new EtappeInfo(
                    EtappeType.CITY,
                    "etappe6",
                    "e00_weg",
                    "e00_gras",
                    "e00_gra2",
                    "e00_vang",
                    "scrl_lo",
                    "scrl_hi",
                    "AMSTERDAM",
                    4,
                    10000
                )
            },
            {
                7,
                new EtappeInfo(
                    EtappeType.HIGHWAY,
                    "etappe7",
                    "e03_weg",
                    "e03_gras",
                    "e03_gra2",
                    "e03_vang",
                    "scrl_lo",
                    "scrl_hi",
                    "MUNCHEN - BERLIN",
                    3,
                    10000
                )
            },
            {
                8,
                new EtappeInfo(
                    EtappeType.CITY,
                    "etappe8",
                    "e02_weg",
                    "e02_gras",
                    "e02_gra2",
                    "e02_vang",
                    "scrl_lo",
                    "scrl_hi",
                    "BERLIN",
                    0,
                    10000
                )
            }
        };

    public enum EtappeType
    {
        HIGHWAY,
        CITY
    }

    public record EtappeInfo(
        EtappeType Type,
        string Filename,
        string Texture1,
        string Texture2,
        string Texture3,
        string Texture4,
        string SkyLow,
        string SkyHigh,
        string Name,
        int MusicIndex,
        int Timer
    );

    private List<ChunkData> chunks = new();

    private string filename;
    private int index;
    private string rmpPath;
    private string ompPath;
    private string pmpPath;

    private OmpFile? ompFile;
    private RmpFile? rmpFile;
    private PmpFile? pmpFile;

    public EtappeData(string filename, int index)
    {
        this.filename = filename;
        this.index = index;

        ompPath = Path.Combine(Game.GameFolder, filename + ".omp");
        rmpPath = Path.Combine(Game.GameFolder, filename + ".rmp");
        pmpPath = Path.Combine(Game.GameFolder, filename + ".pmp");

        Load();
    }

    private void Load()
    {
        using FileStream ompFileStream = new(ompPath, FileMode.Open, FileAccess.Read);
        using BinaryReader ompReader = new(ompFileStream);

        using FileStream rmpFileStream = new(rmpPath, FileMode.Open, FileAccess.Read);
        using BinaryReader rmpReader = new(rmpFileStream);

        using FileStream pmpFileStream = new(pmpPath, FileMode.Open, FileAccess.Read);
        using BinaryReader pmpReader = new(pmpFileStream);

        ompFile = new OmpFile(ompPath);
        ompFile.Read(ompReader);

        rmpFile = new RmpFile(rmpPath);
        rmpFile.Read(rmpReader);

        pmpFile = new PmpFile(pmpPath);
        pmpFile.Read(pmpReader);

        Console.WriteLine(
            $"[EtappeData] Loaded OMP file: {ompPath} with {ompFile.GetObjects().Count} objects"
        );

        Console.WriteLine(
            $"[EtappeData] Loaded PMP file: {pmpPath} with {pmpFile.GetChunkCount()} chunks"
        );

        Console.WriteLine(
            $"[EtappeData] Loaded RMP file: {rmpPath} with {rmpFile.GetDrawChunkCount()} chunks"
        );

        LoadTextures();
        LoadChunks();
    }

    public EtappeInfo GetEtappeInfo()
    {
        return StaticEtappeInfo[index];
    }

    private void LoadTextures()
    {
        EtappeInfo info = GetEtappeInfo();

        Game.GameDataManager.SetStaticTexture(
            GameData.StaticTexureLocation.ROAD_TEXTURE,
            Game.GameDataManager.GetOrLoadTexture("rcs", info.Texture1)
        );
        Game.GameDataManager.SetStaticTexture(
            GameData.StaticTexureLocation.LAND_TEXTURE,
            Game.GameDataManager.GetOrLoadTexture("rcs", info.Texture2)
        );
        Game.GameDataManager.SetStaticTexture(
            GameData.StaticTexureLocation.EXTRA_TEXTURE,
            Game.GameDataManager.GetOrLoadTexture("rcs", info.Texture3)
        );
        Game.GameDataManager.SetStaticTexture(
            GameData.StaticTexureLocation.RAIL_TEXTURE,
            Game.GameDataManager.GetOrLoadTexture("rcs", info.Texture4)
        );
    }

    private void LoadChunks()
    {
        if (rmpFile!.GetDrawChunkCount() != pmpFile!.GetChunkCount())
            throw new Exception("RMP and PMP chunk count mismatch");

        for (int i = 0; i < rmpFile!.GetDrawChunkCount(); i++)
        {
            chunks.Add(new ChunkData(i, rmpFile!.GetChunks()[i], pmpFile!.GetChunks()[i]));
        }
    }

    public int GetChunkCount()
    {
        return rmpFile!.GetDrawChunkCount();
    }

    public List<ChunkData> GetChunks()
    {
        return chunks;
    }

    public List<OmpFile.StaticObject> GetObjects()
    {
        return ompFile!.GetObjects();
    }
}
