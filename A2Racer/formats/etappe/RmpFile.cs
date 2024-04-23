public class RmpFile
{
    // #pragma pattern_limit 999999

    // u32 ukn @ 0x00;
    // u32 drawChunkCount @ 0x04;

    // struct VertexData {
    //     float x;
    //     float y;
    //     float z;
    //     float u;
    //     float v;
    // };

    // struct FaceData {
    //     u32 idx0;
    //     u32 idx1;
    //     u32 idx2;
    // };

    // struct PlaneData {
    //     VertexData vertices[4];
    //     FaceData faces[2];
    // };

    // struct Chunk {
    //     u32 roadPlaneCount;
    //     PlaneData roadPlanes[roadPlaneCount];
    //     u32 landPlaneCount;
    //     PlaneData landPlanes[landPlaneCount];
    //     u32 extraPlaneCount;
    //     PlaneData extraPlanes[extraPlaneCount];
    //     u32 railPlaneCount;
    //     PlaneData railPlanes[railPlaneCount];
    // };

    // Chunk chunks[drawChunkCount] @ 0x08;


    public class VertexData
    {
        public float x;
        public float y;
        public float z;
        public float u;
        public float v;

        public override string ToString()
        {
            return $"VertexData: {x}, {y}, {z} - uv: {u}, {v}";
        }
    }

    public class PlaneData
    {
        public const int NUM_VERTICES = 4;
        public const int NUM_FACES = 2;

        private VertexData[] vertices = new VertexData[NUM_VERTICES];
        private Generic.Face[] faces = new Generic.Face[NUM_FACES];

        public VertexData[] GetVertices()
        {
            return vertices;
        }

        public Generic.Face[] GetFaces()
        {
            return faces;
        }

        public PlaneData Read(BinaryReader reader)
        {
            for (int i = 0; i < 4; i++)
            {
                vertices[i] = new VertexData
                {
                    x = reader.ReadSingle(),
                    y = reader.ReadSingle(),
                    z = reader.ReadSingle(),
                    u = reader.ReadSingle(),
                    v = reader.ReadSingle()
                };
            }

            for (int i = 0; i < 2; i++)
            {
                faces[i] = new Generic.Face
                {
                    idx0 = reader.ReadInt32(),
                    idx1 = reader.ReadInt32(),
                    idx2 = reader.ReadInt32()
                };
            }

            return this;
        }

        public override string ToString()
        {
            return $"PlaneData: {vertices[0]}, {vertices[1]}, {vertices[2]}, {vertices[3]} - {faces[0]}, {faces[1]}";
        }
    }

    public class Chunk
    {
        private int roadPlaneCount;
        private List<PlaneData> roadPlanes = new List<PlaneData>();
        private int landPlaneCount;
        private List<PlaneData> landPlanes = new List<PlaneData>();
        private int extraPlaneCount;
        private List<PlaneData> extraPlanes = new List<PlaneData>();
        private int railPlaneCount;
        private List<PlaneData> railPlanes = new List<PlaneData>();

        public int GetRoadPlaneCount()
        {
            return roadPlaneCount;
        }

        public List<PlaneData> GetRoadPlanes()
        {
            return roadPlanes;
        }

        public int GetLandPlaneCount()
        {
            return landPlaneCount;
        }

        public List<PlaneData> GetLandPlanes()
        {
            return landPlanes;
        }

        public int GetExtraPlaneCount()
        {
            return extraPlaneCount;
        }

        public List<PlaneData> GetExtraPlanes()
        {
            return extraPlanes;
        }

        public int GetRailPlaneCount()
        {
            return railPlaneCount;
        }

        public List<PlaneData> GetRailPlanes()
        {
            return railPlanes;
        }

        public Chunk Read(BinaryReader reader)
        {
            roadPlaneCount = reader.ReadInt32();

            for (int i = 0; i < roadPlaneCount; i++)
            {
                roadPlanes.Add(new PlaneData().Read(reader));
            }

            landPlaneCount = reader.ReadInt32();

            for (int i = 0; i < landPlaneCount; i++)
            {
                landPlanes.Add(new PlaneData().Read(reader));
            }

            extraPlaneCount = reader.ReadInt32();

            for (int i = 0; i < extraPlaneCount; i++)
            {
                extraPlanes.Add(new PlaneData().Read(reader));
            }

            railPlaneCount = reader.ReadInt32();

            for (int i = 0; i < railPlaneCount; i++)
            {
                railPlanes.Add(new PlaneData().Read(reader));
            }

            return this;
        }
    }

    private int ukn;
    private int drawChunkCount;
    private List<Chunk> chunks = new List<Chunk>();
    private readonly string filename;

    public RmpFile(string filename)
    {
        this.filename = filename;
    }

    public string GetFilename()
    {
        return filename;
    }

    public int GetUkn()
    {
        return ukn;
    }

    public int GetDrawChunkCount()
    {
        return drawChunkCount;
    }

    public List<Chunk> GetChunks()
    {
        return chunks;
    }

    public RmpFile Read(BinaryReader reader)
    {
        ukn = reader.ReadInt32();
        drawChunkCount = reader.ReadInt32();

        for (int i = 0; i < drawChunkCount; i++)
        {
            chunks.Add(new Chunk().Read(reader));
        }

        return this;
    }
}
