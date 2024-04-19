class RcsFile {

    public class RcsEntry {
        private string name;
        private int offset;
        private byte[] data;

        public RcsEntry(string name, int offset, byte[] data) {
            this.name = name;
            this.offset = offset;
            this.data = data;
        }

        public string GetName() {
            return name;
        }

        public int GetOffset() {
            return offset;
        }

        public byte[] GetData() {
            return data;
        }
    }

    readonly string filename;

    public RcsFile(string filename) {
        this.filename = filename;
    }

    public string GetFilename() {
        return filename;
    }

    public RcsEntry GetEntry(BinaryReader reader, IndFile.IndEntry entry) {
        reader.BaseStream.Position = entry.GetOffset();
        int length = entry.GetLength();
        
        if(length == -1) {
            entry.SetLength((int)reader.BaseStream.Length - entry.GetOffset());
        }

        return new(entry.GetName(), entry.GetOffset(), reader.ReadBytes(entry.GetLength()));
    }
}