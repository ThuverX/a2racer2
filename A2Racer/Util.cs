using System.Text;

class Util {
    public static string ReadCString(BinaryReader reader) {
        StringBuilder sb = new();
        char c;
        while ((c = reader.ReadChar()) != '\0') {
            sb.Append(c);
        }
        return sb.ToString();
    }
}