class IndFile {

    public class IndEntry {
        private string name;
        private int offset;
        private int length = -1;

        public IndEntry() {
            name = "";
            offset = 0;
        }

        public string GetName() {
            return name;
        }

        public int GetOffset() {
            return offset;
        }

        public int GetLength() {
            return length;
        }

        public int GetEnd() {
            return offset + length;
        }

        public void SetLength(int length) {
            this.length = length;
        }

        public IndEntry Read(BinaryReader reader) {
            long start = reader.BaseStream.Position;

            name = Util.ReadCString(reader);

            reader.BaseStream.Position = start + 0x14;

            offset = reader.ReadInt32();

            return this;
        }
    }

    readonly string filename;
    readonly List<IndEntry> entries = new();
    int numEntries;


    public IndFile(string filename) {
        this.filename = filename;
    }

    public string GetFilename() {
        return filename;
    }

    public int GetNumEntries() {
        return numEntries;
    }

    public List<IndEntry> GetEntries() {
        return entries;
    }

    public IndFile Read(BinaryReader reader) {
        numEntries = reader.ReadUInt16();

        for (int i = 0; i < numEntries; i++) {
            IndEntry entry = new();
            entry.Read(reader);

            if(i > 0) {
                IndEntry prevEntry = entries[i - 1];
                prevEntry.SetLength(entry.GetOffset() - prevEntry.GetOffset());
            }

            entries.Add(entry);
        }

        return this;
    }
}