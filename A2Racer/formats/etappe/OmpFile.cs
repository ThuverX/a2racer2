// Object Map File

class OmpFile {
    // u32 count @ 0;

    // struct StaticObject {
    //     u32 chunk;
    //     u32 type;
    //     float x;
    //     float y;
    //     float z;
    //     u32;
    //     u32;
    //     u32;
    //     char animname[0x20];
    //     u32 animindex;
    // };

    // StaticObject objects[count] @ 0x04;

    public class StaticObject {
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

        public int GetChunk() {
            return chunk;
        }

        // possible values:
        // 0x5 = 0x3dcccccd00000000
        // 0x3 = 0x3e4ccccd464b2000
        // 0x9 = FUN_0040c920(iVar3);
        // 0xa = 0x3e4ccccd44a28000
        public int GetObjectType() {
            return type;
        }

        public float GetX() {
            return x;
        }

        public float GetY() {
            return y;
        }

        public float GetZ() {
            return z;
        }

        public float GetRotX() {
            return rotX;
        }

        public float GetRotY() {
            return rotY;
        }

        public float GetRotZ() {
            return rotZ;
        }

        public string GetAnimname() {
            return animname;
        }

        public int GetAnimindex() {
            return animindex;
        }

        public StaticObject Read(BinaryReader reader) {
            chunk = reader.ReadInt32();
            type = reader.ReadInt32();
            x = reader.ReadSingle();
            y = reader.ReadSingle();
            z = reader.ReadSingle();
            rotX = reader.ReadSingle();
            rotY = reader.ReadSingle();
            rotZ = reader.ReadSingle();
            animname = Util.ReadString(reader, 0x20);
            animindex = reader.ReadInt32();
            return this;
        }
    }

    private int objectCount;
    private List<StaticObject> objects = new List<StaticObject>();
    private readonly string filepath;

    public int GetObjectCount() {
        return objectCount;
    }

    public List<StaticObject> GetObjects() {
        return objects;
    }

    public OmpFile(string filepath) {
        this.filepath = filepath;
    }

    public OmpFile Read(BinaryReader reader) {
        objectCount = reader.ReadInt32();

        for (int i = 0; i < objectCount; i++) {
            objects.Add(new StaticObject().Read(reader));
        }

        return this;
    }
}