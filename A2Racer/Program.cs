// See https://aka.ms/new-console-template for more information
string name = "mrcs";
string indPath = "X:/a2racer/data/" + name + ".ind";
string rcsPath = "X:/a2racer/data/" + name + ".img";

IndFile indFile = new IndFile(indPath);
using (FileStream fileStream = new(indPath, FileMode.Open, FileAccess.Read))
{
    using BinaryReader reader = new(fileStream);
    indFile.Read(reader);
    Console.WriteLine(indFile.GetFilename());
    Console.WriteLine(indFile.GetNumEntries());

    IndFile.IndEntry entry = indFile.GetEntries()[0];
    Console.WriteLine("Name: " + entry.GetName());
    Console.WriteLine("Offset: " + entry.GetOffset());
    Console.WriteLine("Length: " + entry.GetLength());
    Console.WriteLine("End: " + entry.GetEnd());
}

RcsFile rcsFile = new RcsFile(rcsPath);
string outputDir = "X:/a2racer/data/" + name + "/";

if (!Directory.Exists(outputDir))
{
    Directory.CreateDirectory(outputDir);
}

using (FileStream fileStream = new(rcsPath, FileMode.Open, FileAccess.Read))
{
    using BinaryReader reader = new(fileStream);

    foreach (IndFile.IndEntry entry in indFile.GetEntries())
    {
        RcsFile.RcsEntry rcsEntry = rcsFile.GetEntry(reader, entry);
        string entryPath = outputDir + "/" + rcsEntry.GetName();

        if (File.Exists(entryPath))
        {
            File.Delete(entryPath);
        }

        using (FileStream entryStream = new(entryPath, FileMode.Create, FileAccess.Write)) {
            entryStream.Write(rcsEntry.GetData());
        }
     

        Console.WriteLine("Extracted " + rcsEntry.GetName());

        if(rcsEntry.GetName().ToLower().EndsWith(".d3d")) {
            D3dFile d3dFile = new(entryPath);
            using FileStream d3dStream = new(entryPath, FileMode.Open, FileAccess.Read);
            using BinaryReader d3dReader = new(d3dStream);
            d3dFile.Read(d3dReader);
            d3dFile.WriteToObj(entryPath + ".obj");
        }
    }
}

// Console.WriteLine("");
// Console.WriteLine("Wiel d3d");
// Console.WriteLine("");

// string d3dPath = "X:/a2racer/data/WIEL2.d3d";
// D3dFile d3dFile = new(d3dPath);
// using (FileStream fileStream = new(d3dPath, FileMode.Open, FileAccess.Read))
// {
//     using BinaryReader reader = new(fileStream);
//     d3dFile.Read(reader);
// }

// Console.WriteLine(d3dFile.GetFilename());
// Console.WriteLine(d3dFile.GetNumVertices());
// Console.WriteLine(d3dFile.GetNumTriangles());

// d3dFile.WriteToObj("X:/a2racer/data/WIEL2.obj");