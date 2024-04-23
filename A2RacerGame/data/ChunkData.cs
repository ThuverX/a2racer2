using System.Numerics;
using Jitter2.LinearMath;
using Raylib_cs;
using static Raylib_cs.Raylib;

class ChunkData
{
    public record MeshData
    {
        public const float EXTRUSION = 1f;

        public required float[] vertices;
        public required float[] uvs;
        public required ushort[] indices;

        public List<JTriangle> GetCollisionTriangles()
        {
            List<JTriangle> triangles = new();

            for (int i = 0; i < indices.Length / 3; i++)
            {
                JTriangle triangle =
                    new()
                    {
                        V0 = new JVector(
                            vertices[indices[i * 3] * 3],
                            vertices[indices[i * 3] * 3 + 1],
                            vertices[indices[i * 3] * 3 + 2]
                        ),

                        V2 = new JVector(
                            vertices[indices[i * 3 + 1] * 3],
                            vertices[indices[i * 3 + 1] * 3 + 1],
                            vertices[indices[i * 3 + 1] * 3 + 2]
                        ),

                        V1 = new JVector(
                            vertices[indices[i * 3 + 2] * 3],
                            vertices[indices[i * 3 + 2] * 3 + 1],
                            vertices[indices[i * 3 + 2] * 3 + 2]
                        )
                    };

                JVector normal = (triangle.V2 - triangle.V0) % (triangle.V1 - triangle.V0);

                if (!MathHelper.CloseToZero(normal, 1e-12f))
                    triangles.Add(triangle);
            }

            return triangles;
        }

        public unsafe Mesh ToMesh()
        {
            Mesh mesh = new(vertices.Length / 3, indices.Length / 3);

            mesh.AllocVertices();
            mesh.AllocIndices();
            mesh.AllocTexCoords();

            for (int i = 0; i < vertices.Length / 3; i++)
            {
                mesh.Vertices[i * 3] = vertices[i * 3];
                mesh.Vertices[i * 3 + 1] = vertices[i * 3 + 1];
                mesh.Vertices[i * 3 + 2] = vertices[i * 3 + 2];

                mesh.TexCoords[i * 2] = uvs[i * 2];
                mesh.TexCoords[i * 2 + 1] = uvs[i * 2 + 1];
            }

            for (int i = 0; i < indices.Length / 3; i++)
            {
                mesh.Indices[i * 3] = indices[i * 3];
                mesh.Indices[i * 3 + 1] = indices[i * 3 + 1];
                mesh.Indices[i * 3 + 2] = indices[i * 3 + 2];
            }

            UploadMesh(ref mesh, false);

            return mesh;
        }

        public bool HasData()
        {
            return vertices.Length > 0 && uvs.Length > 0 && indices.Length > 0;
        }
    }

    private int chunkIndex;
    private RmpFile.Chunk rmpChunk;
    private PmpFile.Chunk pmpChunk;
    private MeshData roadMeshData;
    private MeshData landMeshData;
    private MeshData extraMeshData;
    private MeshData railMeshData;

    private Vector3 boundMin = new(float.MaxValue, float.MaxValue, float.MaxValue);
    private Vector3 boundMax = new(float.MinValue, float.MinValue, float.MinValue);

    private MeshData BuildMeshFromPlaneDataList(List<RmpFile.PlaneData> planes)
    {
        MeshData mesh = new MeshData
        {
            vertices = new float[planes.Count * RmpFile.PlaneData.NUM_VERTICES * 3],
            uvs = new float[planes.Count * RmpFile.PlaneData.NUM_VERTICES * 2],
            indices = new ushort[planes.Count * RmpFile.PlaneData.NUM_FACES * 3]
        };

        for (int i = 0; i < planes.Count; i++)
        {
            RmpFile.PlaneData plane = planes[i];

            for (int j = 0; j < RmpFile.PlaneData.NUM_VERTICES; j++)
            {
                RmpFile.VertexData vertex = plane.GetVertices()[j];

                mesh.vertices[(i * RmpFile.PlaneData.NUM_VERTICES + j) * 3] =
                    vertex.x / GameWorld.WORLD_SIZE;
                mesh.vertices[(i * RmpFile.PlaneData.NUM_VERTICES + j) * 3 + 1] =
                    vertex.y / GameWorld.WORLD_SIZE;
                mesh.vertices[(i * RmpFile.PlaneData.NUM_VERTICES + j) * 3 + 2] =
                    vertex.z / GameWorld.WORLD_SIZE;

                mesh.uvs[(i * RmpFile.PlaneData.NUM_VERTICES + j) * 2] = vertex.u;
                mesh.uvs[(i * RmpFile.PlaneData.NUM_VERTICES + j) * 2 + 1] = vertex.v;

                boundMin = Vector3.Min(
                    boundMin,
                    new Vector3(
                        vertex.x / GameWorld.WORLD_SIZE,
                        vertex.y / GameWorld.WORLD_SIZE,
                        vertex.z / GameWorld.WORLD_SIZE
                    )
                );
                boundMax = Vector3.Max(
                    boundMax,
                    new Vector3(
                        vertex.x / GameWorld.WORLD_SIZE,
                        vertex.y / GameWorld.WORLD_SIZE,
                        vertex.z / GameWorld.WORLD_SIZE
                    )
                );
            }

            for (int j = 0; j < RmpFile.PlaneData.NUM_FACES; j++)
            {
                Generic.Face face = plane.GetFaces()[j];

                mesh.indices[(i * RmpFile.PlaneData.NUM_FACES + j) * 3] = (ushort)(
                    face.idx0 + i * RmpFile.PlaneData.NUM_VERTICES
                );
                mesh.indices[(i * RmpFile.PlaneData.NUM_FACES + j) * 3 + 1] = (ushort)(
                    face.idx1 + i * RmpFile.PlaneData.NUM_VERTICES
                );
                mesh.indices[(i * RmpFile.PlaneData.NUM_FACES + j) * 3 + 2] = (ushort)(
                    face.idx2 + i * RmpFile.PlaneData.NUM_VERTICES
                );
            }
        }

        return mesh;
    }

    public ChunkData(int chunkIndex, RmpFile.Chunk rmpChunk, PmpFile.Chunk pmpChunk)
    {
        this.chunkIndex = chunkIndex;
        this.rmpChunk = rmpChunk;
        this.pmpChunk = pmpChunk;

        roadMeshData = BuildMeshFromPlaneDataList(rmpChunk.GetRoadPlanes());
        landMeshData = BuildMeshFromPlaneDataList(rmpChunk.GetLandPlanes());
        extraMeshData = BuildMeshFromPlaneDataList(rmpChunk.GetExtraPlanes());
        railMeshData = BuildMeshFromPlaneDataList(rmpChunk.GetRailPlanes());
    }

    public MeshData GetRoadMeshData()
    {
        return roadMeshData;
    }

    public MeshData GetLandMeshData()
    {
        return landMeshData;
    }

    public MeshData GetExtraMeshData()
    {
        return extraMeshData;
    }

    public MeshData GetRailMeshData()
    {
        return railMeshData;
    }

    public int GetChunkIndex()
    {
        return chunkIndex;
    }

    public List<RmpFile.PlaneData> GetRoadPlanes()
    {
        return rmpChunk.GetRoadPlanes();
    }

    public BoundingBox GetBoundingBox()
    {
        return new BoundingBox(boundMin, boundMax);
    }
}
