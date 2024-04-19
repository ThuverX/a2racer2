using System.Text.RegularExpressions;

partial class ConvertAnimIndicies {

    [GeneratedRegex("\"(.*?)\"")]
    private static partial Regex QuotedStringRegex();
    public static void Convert(string inputPath, string outputPath) {
        using StreamReader reader = new(inputPath);
        using StreamWriter writer = new(outputPath);

        writer.WriteLine("Name,Index");

        int start_index = -1;

        string line;
        while ((line = reader.ReadLine()) != null) {
            string[] parts = line.Trim().Split(' ');

            string strAddress = "0x" + parts[0];
            int address = System.Convert.ToInt32(strAddress, 16);

            if(start_index == -1) {
                start_index = address;
            }

            string name = QuotedStringRegex().Match(line).Groups[1].Value;

            int index = (address - start_index) / 4;

            writer.WriteLine(name + "," + index);
        }

        Console.WriteLine("Converted " + inputPath + " to " + outputPath);

        reader.Close();
        writer.Close();
    }
}