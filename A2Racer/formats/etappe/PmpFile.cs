public class PmpFile
{
    // u32 dataChunkCount @ $;

    // struct Chunk {
    //     float x;
    //     float y;
    //     float z;
    //     u32 ukn4[2];
    //     u32 count;
    //     u32 ukn5[count];

    //     u32 waypointCount;
    //     u32 waypoints[waypointCount];

    //     u32 waypointCount2;
    //     u32 waypoints2[waypointCount2];
    // };

    // Chunk chunks[dataChunkCount] @ $;

    public class Chunk
    {
        private float x;
        private float y;
        private float z;
        private uint[] ukn4 = new uint[2];
        private uint count;
        private uint[] ukn5 = Array.Empty<uint>();

        private uint waypointCount;
        private uint[] waypoints = Array.Empty<uint>();

        private uint waypointCount2;
        private uint[] waypoints2 = Array.Empty<uint>();

        public float GetX()
        {
            return x;
        }

        public float GetY()
        {
            return y;
        }

        public float GetZ()
        {
            return z;
        }

        public uint[] GetUkn4()
        {
            return ukn4;
        }

        public uint GetCount()
        {
            return count;
        }

        public uint[] GetUkn5()
        {
            return ukn5;
        }

        public uint GetWaypointCount()
        {
            return waypointCount;
        }

        public uint[] GetWaypoints()
        {
            return waypoints;
        }

        public uint GetWaypointCount2()
        {
            return waypointCount2;
        }

        public uint[] GetWaypoints2()
        {
            return waypoints2;
        }

        public void Read(BinaryReader reader)
        {
            x = reader.ReadSingle();
            y = reader.ReadSingle();
            z = reader.ReadSingle();
            ukn4[0] = reader.ReadUInt32();
            ukn4[1] = reader.ReadUInt32();
            count = reader.ReadUInt32();
            ukn5 = new uint[count];
            for (int i = 0; i < count; i++)
            {
                ukn5[i] = reader.ReadUInt32();
            }

            waypointCount = reader.ReadUInt32();
            waypoints = new uint[waypointCount];
            for (int i = 0; i < waypointCount; i++)
            {
                waypoints[i] = reader.ReadUInt32();
            }

            waypointCount2 = reader.ReadUInt32();
            waypoints2 = new uint[waypointCount2];
            for (int i = 0; i < waypointCount2; i++)
            {
                waypoints2[i] = reader.ReadUInt32();
            }
        }
    }

    private string filename;
    private int chunkCount;
    private List<Chunk> chunks = new();

    public PmpFile(string filename)
    {
        this.filename = filename;
    }

    public int GetChunkCount()
    {
        return chunkCount;
    }

    public List<Chunk> GetChunks()
    {
        return chunks;
    }

    public void Read(BinaryReader reader)
    {
        chunkCount = reader.ReadInt32();

        for (int i = 0; i < chunkCount; i++)
        {
            Chunk chunk = new();
            chunk.Read(reader);
            chunks.Add(chunk);
        }
    }
}
