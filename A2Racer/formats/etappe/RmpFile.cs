class RmpFile {
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

    public class VertexData {
        public float x;
        public float y;
        public float z;
        public float u;
        public float v;
    }

    public class PlaneData {
        private VertexData[] vertices = new VertexData[4];
        private Generic.Face[] faces = new Generic.Face[2];

        public VertexData[] GetVertices() {
            return vertices;
        }

        public Generic.Face[] GetFaces() {
            return faces;
        }

        public PlaneData Read(BinaryReader reader) {
            for(int i = 0; i < 4; i++) {
                vertices[i] = new VertexData {
                    x = reader.ReadSingle(),
                    y = reader.ReadSingle(),
                    z = reader.ReadSingle(),
                    u = reader.ReadSingle(),
                    v = reader.ReadSingle()
                };
            }

            for(int i = 0; i < 2; i++) {
                faces[i] = new Generic.Face {
                    idx0 = reader.ReadInt32(),
                    idx1 = reader.ReadInt32(),
                    idx2 = reader.ReadInt32()
                };
            }

            return this;
        }
    }

    public class Chunk {
        private int roadPlaneCount;
        private List<PlaneData> roadPlanes = new List<PlaneData>();
        private int landPlaneCount;
        private List<PlaneData> landPlanes = new List<PlaneData>();
        private int extraPlaneCount;
        private List<PlaneData> extraPlanes = new List<PlaneData>();
        private int railPlaneCount;
        private List<PlaneData> railPlanes = new List<PlaneData>();

        public int GetRoadPlaneCount() {
            return roadPlaneCount;
        }

        public List<PlaneData> GetRoadPlanes() {
            return roadPlanes;
        }

        public int GetLandPlaneCount() {
            return landPlaneCount;
        }

        public List<PlaneData> GetLandPlanes() {
            return landPlanes;
        }

        public int GetExtraPlaneCount() {
            return extraPlaneCount;
        }

        public List<PlaneData> GetExtraPlanes() {
            return extraPlanes;
        }

        public int GetRailPlaneCount() {
            return railPlaneCount;
        }

        public List<PlaneData> GetRailPlanes() {
            return railPlanes;
        }

        public Chunk Read(BinaryReader reader) {
            roadPlaneCount = reader.ReadInt32();

            for(int i = 0; i < roadPlaneCount; i++) {
                roadPlanes.Add(new PlaneData().Read(reader));
            }

            landPlaneCount = reader.ReadInt32();

            for(int i = 0; i < landPlaneCount; i++) {
                landPlanes.Add(new PlaneData().Read(reader));
            }

            extraPlaneCount = reader.ReadInt32();

            for(int i = 0; i < extraPlaneCount; i++) {
                extraPlanes.Add(new PlaneData().Read(reader));
            }

            railPlaneCount = reader.ReadInt32();

            for(int i = 0; i < railPlaneCount; i++) {
                railPlanes.Add(new PlaneData().Read(reader));
            }

            return this;
        }
    }

    private int ukn;
    private int drawChunkCount;
    private List<Chunk> chunks = new List<Chunk>();
    private readonly string filename;

    public RmpFile(string filename) {
        this.filename = filename;
    }

    public string GetFilename() {
        return filename;
    }

    public int GetUkn() {
        return ukn;
    }

    public int GetDrawChunkCount() {
        return drawChunkCount;
    }

    public List<Chunk> GetChunks() {
        return chunks;
    }

    public RmpFile Read(BinaryReader reader) {
        ukn = reader.ReadInt32();
        drawChunkCount = reader.ReadInt32();

        for(int i = 0; i < drawChunkCount; i++) {
            chunks.Add(new Chunk().Read(reader));
        }

        return this;
    }


    // This may not be structure that needs to be rendered. It reminds me of Valve's BSP format.
    // Aka, this aren't meant to render but split the world into chunks.
    // Further research is needed to understand the purpose of this format.

    // private string FormatMax(float value) {
    //     return value.ToString("0.000000");
    // }

    // private void WritePlaneData(StreamWriter writer, PlaneData plane) {
    //     VertexData[] vertices = plane.GetVertices();
    //     Generic.Face[] faces = plane.GetFaces();

    //     for(int k = 0; k < 4; k++) {
    //         VertexData vertex = vertices[k];
    //         writer.WriteLine("v " + FormatMax(vertex.x) + " " + FormatMax(vertex.y) + " " + FormatMax(vertex.z));
    //     }

    //     for(int k = 0; k < 4; k++) {
    //         VertexData vertex = vertices[k];
    //         writer.WriteLine("vt " + vertex.u + " " + vertex.v);
    //     }

    //     for(int k = 0; k < 2; k++) {
    //         Generic.Face face = faces[k];
    //         writer.WriteLine("f " + (face.idx0 + 1) + "/" + (face.idx0 + 1) + " " + (face.idx1 + 1) + "/" + (face.idx1 + 1) + " " + (face.idx2 + 1) + "/" + (face.idx2 + 1));
    //     }
    // }

    // public void WriteToObj(string filepath) {
    //     using StreamWriter writer = new(filepath);

    //     string filename = Path.GetFileName(filepath).Replace(".obj", "");

    //     writer.WriteLine("# " + filepath);

    //     for(int i = 0; i < 1; i++) {
    //         Chunk chunk = chunks[i];

    //         List<PlaneData> roadPlanes = chunk.GetRoadPlanes();

    //         if(roadPlanes.Count > 0) writer.WriteLine("o road_chunk" + i);

    //         for(int j = 0; j < roadPlanes.Count; j++) {
    //             WritePlaneData(writer, roadPlanes[j]);
    //         }

    //         // List<PlaneData> landPlanes = chunk.GetLandPlanes();

    //         // if(landPlanes.Count > 0) writer.WriteLine("o land_chunk" + i);

    //         // for(int j = 0; j < landPlanes.Count; j++) {
    //         //     WritePlaneData(writer, landPlanes[j]);
    //         // }

    //         // List<PlaneData> extraPlanes = chunk.GetExtraPlanes();

    //         // if(extraPlanes.Count > 0) writer.WriteLine("o extra_chunk" + i);

    //         // for(int j = 0; j < extraPlanes.Count; j++) {
    //         //     WritePlaneData(writer, extraPlanes[j]);
    //         // }

    //         // List<PlaneData> railPlanes = chunk.GetRailPlanes();

    //         // if(railPlanes.Count > 0) writer.WriteLine("o rail_chunk" + i);

    //         // for(int j = 0; j < railPlanes.Count; j++) {
    //         //     WritePlaneData(writer, railPlanes[j]);
    //         // }
    //     }

    //     writer.Close();
    // }
}