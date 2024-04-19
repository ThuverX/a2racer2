using System.Text;

class Util {
    public static string ReadCString(BinaryReader reader) {
        StringBuilder sb = new();
        char c;
        while ((c = Convert.ToChar(reader.ReadByte())) != '\0') {
            sb.Append(c);
        }
        return sb.ToString();
    }

    public static string ReadString(BinaryReader reader, int length) {
        StringBuilder sb = new();
        for (int i = 0; i < length; i++) {
            sb.Append(Convert.ToChar(reader.ReadByte()));
        }

        return sb.ToString();
    }
}