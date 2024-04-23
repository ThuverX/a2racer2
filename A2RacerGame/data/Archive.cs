using Raylib_cs;

class Archive
{
    public record ArchiveEntry
    {
        public enum Type
        {
            TEXTURE,
            MODEL,
        }

        public required Type type;
        public required string filename;
        public required byte[] data;

        public D3dFile? GetModel()
        {
            if (type != Type.MODEL)
                throw new Exception($"[Archive.cs] Entry \"{filename}\" is not a model");

            D3dFile file = new D3dFile(filename);

            using MemoryStream stream = new(data);
            using BinaryReader reader = new(stream);

            file.Read(reader);

            return file;
        }
    }

    private string filename;
    private string indPath;
    private string rcsPath;
    private IndFile? indFile = null;
    private RcsFile? rcsFile = null;

    public Archive(string filename)
    {
        this.filename = filename;

        indPath = Path.Combine(Game.GameFolder, filename + ".ind");
        rcsPath = Path.Combine(Game.GameFolder, filename + ".img");

        Load();
    }

    private void Load()
    {
        using FileStream indFileStream = new(indPath, FileMode.Open, FileAccess.Read);
        using BinaryReader indReader = new(indFileStream);

        indFile = new(indPath);
        indFile.Read(indReader);

        using FileStream rcsFileStream = new(rcsPath, FileMode.Open, FileAccess.Read);
        using BinaryReader rcsReader = new(rcsFileStream);

        rcsFile = new RcsFile(rcsPath);
        rcsFile.Read(rcsReader);

        Console.WriteLine(
            $"[Archive.cs] Loaded archive: \"{filename}\" with {indFile.GetEntries().Count} entries"
        );
    }

    private IndFile.IndEntry? GetIndEntry(string name)
    {
        return indFile?.GetEntries().Find(e => e.GetName().ToLower().Equals(name.ToLower()));
    }

    public bool HasEntry(string name)
    {
        return GetIndEntry(name) != null;
    }

    public ArchiveEntry GetEntry(string filename)
    {
        if (!HasEntry(filename))
            throw new Exception(
                $"[Archive.cs] Entry \"{filename}\" not found in archive \"{this.filename}\""
            );

        IndFile.IndEntry entry = GetIndEntry(filename)!;

        using FileStream rcsFileStream = new(rcsPath, FileMode.Open, FileAccess.Read);
        using BinaryReader rcsReader = new(rcsFileStream);

        RcsFile.RcsEntry rcsEntry = rcsFile!.GetEntry(entry);

        rcsFileStream.Close();
        rcsReader.Close();

        string ext = Path.GetExtension(filename).ToLower();
        ArchiveEntry.Type type = ext switch
        {
            ".tga" => ArchiveEntry.Type.TEXTURE,
            ".d3d" => ArchiveEntry.Type.MODEL,
            _
                => throw new Exception(
                    $"[Archive.cs] Unknown file extension \"{ext}\" for entry \"{filename}\""
                ),
        };

        Console.WriteLine(
            $"[Archive.cs] Loaded entry: \"{filename}\" with {rcsEntry.GetData().Length} bytes"
        );

        return new ArchiveEntry
        {
            type = type,
            filename = filename,
            data = rcsEntry.GetData()
        };
    }
}
