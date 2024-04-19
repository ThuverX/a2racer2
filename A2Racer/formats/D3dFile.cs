class D3dFile {

    public class Vertex {
        public float x, y, z;
        public float u, v;
    }

    public class Triangle {
        public int a;
        public int b;
        public int c;
    }

    private readonly string filename;
    private int numVertices;
    private int numTriangles;
    private List<Vertex> vertices = new();
    private List<Triangle> triangles = new();

    public D3dFile(string filename) {
        this.filename = filename;
    }

    public string GetFilename() {
        return filename;
    }

    public int GetNumVertices() {
        return numVertices;
    }

    public int GetNumTriangles() {
        return numTriangles;
    }

    public D3dFile Read(BinaryReader reader) {

        numVertices = reader.ReadInt32();
        numTriangles = reader.ReadInt32();

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

        for (int i = 0; i < numTriangles; i++) {
            Triangle triangle = new()
            {
                a = reader.ReadInt16(),
                b = reader.ReadInt16(),
                c = reader.ReadInt16()
            };

            reader.BaseStream.Position += 3;

            triangles.Add(triangle);
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

        foreach (Triangle triangle in triangles) {
            writer.WriteLine("f " + (triangle.a + 1) + "/" + (triangle.a + 1) + " " + (triangle.b + 1) + "/" + (triangle.b + 1) + " " + (triangle.c + 1) + "/" + (triangle.c + 1));
        }
    }
}