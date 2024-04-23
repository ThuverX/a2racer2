// Collision mesh

public class CmpFile
{
    // u32 count @ 0x00;

    // struct Chunk {
    //     float x;
    //     float y;
    //     float z;
    //     float param_4; // maybe normals?
    //     float param_5;
    //     float param_6;
    // };

    // Chunk chunks[count] @ 0x04;

    public record CollisionVertex
    {
        public float x;
        public float y;
        public float z;
        public float normalX;
        public float normalY;
        public float normalZ;
    }

    private string filename;

    private int count;
    private List<CollisionVertex> vertices = new();

    public int GetCount()
    {
        return count;
    }

    public CollisionVertex GetVertex(int index)
    {
        return vertices[index];
    }

    public List<CollisionVertex> GetVertices()
    {
        return vertices;
    }

    public CmpFile(string filename)
    {
        this.filename = filename;
    }

    public void Read(BinaryReader reader)
    {
        count = reader.ReadInt32();

        for (int i = 0; i < count; i++)
        {
            vertices.Add(
                new CollisionVertex
                {
                    x = reader.ReadSingle(),
                    y = reader.ReadSingle(),
                    z = reader.ReadSingle(),
                    normalX = reader.ReadSingle(),
                    normalY = reader.ReadSingle(),
                    normalZ = reader.ReadSingle()
                }
            );
        }
    }
}
