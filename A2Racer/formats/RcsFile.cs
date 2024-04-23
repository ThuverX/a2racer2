public class RcsFile
{
    public class RcsEntry
    {
        private string name;
        private int offset;
        private byte[] data;

        public RcsEntry(string name, int offset, byte[] data)
        {
            this.name = name;
            this.offset = offset;
            this.data = data;
        }

        public string GetName()
        {
            return name;
        }

        public int GetOffset()
        {
            return offset;
        }

        public byte[] GetData()
        {
            return data;
        }
    }

    private readonly string filename;
    private byte[] data = Array.Empty<byte>();

    public RcsFile(string filename)
    {
        this.filename = filename;
    }

    public string GetFilename()
    {
        return filename;
    }

    public void Read(BinaryReader reader)
    {
        data = reader.ReadBytes((int)reader.BaseStream.Length);
    }

    public RcsEntry GetEntry(IndFile.IndEntry entry)
    {
        using MemoryStream stream = new(data);
        using BinaryReader reader = new(stream);

        reader.BaseStream.Position = entry.GetOffset();
        int length = entry.GetLength();

        if (length == -1)
        {
            entry.SetLength((int)reader.BaseStream.Length - entry.GetOffset());
        }

        return new(entry.GetName(), entry.GetOffset(), reader.ReadBytes(entry.GetLength()));
    }
}
