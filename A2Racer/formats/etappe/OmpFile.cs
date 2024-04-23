// Object Map File

public class OmpFile
{
    // u32 count @ 0;

    // struct StaticObject {
    //     u32 chunk;
    //     u32 type;
    //     float x;
    //     float y;
    //     float z;
    //     float rotX;
    //     float rotY;
    //     float rotZ;
    //     char animname[0x20];
    //     u32 animindex;
    // };

    // StaticObject objects[count] @ 0x04;

    public class StaticObject
    {
        private int chunk;
        private int type;
        private float x;
        private float y;
        private float z;

        // rotation in degrees 0 - 360
        private float rotX;
        private float rotY;
        private float rotZ;

        private string animname = "";
        private int animindex;

        public int GetChunk()
        {
            return chunk;
        }

        // possible values, i have no idea what they mean
        // 0x5 = 0x3dcccccd00000000
        // 0x3 = 0x3e4ccccd464b2000
        // 0x9 = FUN_0040c920(iVar3);
        // 0xa = 0x3e4ccccd44a28000

        // From A2Racer 3, may or may not be the same for A2Racer 2
        // 0=Static
        // 1=PlaySample
        // 2=Pilon
        // 3=RoadWork Sign
        // 4=TrafficLight
        // 5=CollisionPhysics
        // 6=Roadworks Lamp
        // 7=RadarTrap
        // 8=RoadworkArrowSign
        // 9=Police Car
        // 10=Police Blockade
        // 11=Cached 1
        // 12=animated object
        // 13=start animation
        // 14=Cached 1 + collision
        // 15=animation + collision
        // 16=Cached 2
        // 17=Cached 2 + collision
        // 18=Lantaarnpaal
        // 19=Particle trashcan
        // 20=Particle mailbox
        // 21=Light obstacle
        // 22=Particle Paper
        // 23=Particle Birds
        // 24=Particle Leafs

        public int GetObjectType()
        {
            return type;
        }

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

        public float GetRotX()
        {
            return rotX;
        }

        public float GetRotY()
        {
            return rotY;
        }

        public float GetRotZ()
        {
            return rotZ;
        }

        public string GetAnimname()
        {
            return animname;
        }

        public int GetAnimindex()
        {
            return animindex;
        }

        public StaticObject Read(BinaryReader reader)
        {
            chunk = reader.ReadInt32();
            type = reader.ReadInt32();
            x = reader.ReadSingle();
            y = reader.ReadSingle();
            z = reader.ReadSingle();
            rotX = reader.ReadSingle();
            rotY = reader.ReadSingle();
            rotZ = reader.ReadSingle();
            animname = Util.ReadString(reader, 0x20);

            int nullIndex = animname.IndexOf('\0');
            if (nullIndex != -1)
            {
                animname = animname[..nullIndex];
            }

            animindex = reader.ReadInt32();
            return this;
        }

        public override string ToString()
        {
            return $"StaticObject(chunk={chunk}, type={type}, x={x}, y={y}, z={z}, rotX={rotX}, rotY={rotY}, rotZ={rotZ}, animname={animname}, animindex={animindex})";
        }
    }

    private int objectCount;
    private List<StaticObject> objects = new List<StaticObject>();
    private readonly string filepath;

    public int GetObjectCount()
    {
        return objectCount;
    }

    public List<StaticObject> GetObjects()
    {
        return objects;
    }

    public OmpFile(string filepath)
    {
        this.filepath = filepath;
    }

    public OmpFile Read(BinaryReader reader)
    {
        objectCount = reader.ReadInt32();

        for (int i = 0; i < objectCount; i++)
        {
            objects.Add(new StaticObject().Read(reader));
        }

        return this;
    }
}
