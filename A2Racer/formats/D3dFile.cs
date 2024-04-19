class D3dFile {

    public class Vertex {
        public float x, y, z;
        public float u, v;
    }

    private readonly string filename;
    private int numVertices;
    private int numFaces;
    private List<Vertex> vertices = new();
    private List<Generic.Face> faces = new();

    public D3dFile(string filename) {
        this.filename = filename;
    }

    public string GetFilename() {
        return filename;
    }

    public int GetNumVertices() {
        return numVertices;
    }

    public int GetNumFaces() {
        return numFaces;
    }

    public D3dFile Read(BinaryReader reader) {

        numVertices = reader.ReadInt32();
        numFaces = reader.ReadInt32();

        for (int i = 0; i < numVertices; i++) {
            Vertex vertex = new()
            {
                x = reader.ReadSingle(),
                y = reader.ReadSingle(),
                z = reader.ReadSingle()
            };

            reader.ReadSingle();
            reader.ReadSingle();
            reader.ReadSingle();

            vertex.u = reader.ReadSingle();
            vertex.v = 1f - reader.ReadSingle();
            
            reader.BaseStream.Position += 1;

            vertices.Add(vertex);
        }

        for (int i = 0; i < numFaces; i++) {
            Generic.Face face = new()
            {
                idx0 = reader.ReadInt16(),
                idx1 = reader.ReadInt16(),
                idx2 = reader.ReadInt16()
            };

            reader.BaseStream.Position += 3;

            faces.Add(face);
        }

        return this;
    }

    private string FormatMax(float value) {
        return value.ToString("0.000000");
    }

    public void WriteToObj(string filepath) {
        using StreamWriter writer = new(filepath);

        string filename = Path.GetFileName(filepath).Replace(".obj", "");

        writer.WriteLine("# " + filepath);
        writer.WriteLine("o " + filename);

        foreach (Vertex vertex in vertices) {
            writer.WriteLine("v " + FormatMax(vertex.x) + " " + FormatMax(vertex.y) + " " + FormatMax(vertex.z));
        }

        foreach (Vertex vertex in vertices) {
            writer.WriteLine("vt " + FormatMax(vertex.u) + " " + FormatMax(vertex.v));
        }

        writer.WriteLine("s 0");

        foreach (Generic.Face triangle in faces) {
            writer.WriteLine("f " + (triangle.idx0 + 1) + "/" + (triangle.idx0 + 1) + " " + (triangle.idx1 + 1) + "/" + (triangle.idx1 + 1) + " " + (triangle.idx2 + 1) + "/" + (triangle.idx2 + 1));
        }
        
        writer.Close();
    }
}