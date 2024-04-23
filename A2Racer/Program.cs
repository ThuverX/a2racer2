

// // RcsFile rcsFile = new RcsFile(rcsPath);
// // string outputDir = "X:/a2racer/data/" + name + "/";

// // if (!Directory.Exists(outputDir))
// // {
// //     Directory.CreateDirectory(outputDir);
// // }

// // using (FileStream fileStream = new(rcsPath, FileMode.Open, FileAccess.Read))
// // {
// //     using BinaryReader reader = new(fileStream);

// //     foreach (IndFile.IndEntry entry in indFile.GetEntries())
// //     {
// //         RcsFile.RcsEntry rcsEntry = rcsFile.GetEntry(reader, entry);
// //         string entryPath = outputDir + "/" + rcsEntry.GetName();

// //         if (File.Exists(entryPath))
// //         {
// //             File.Delete(entryPath);
// //         }

// //         using (FileStream entryStream = new(entryPath, FileMode.Create, FileAccess.Write)) {
// //             entryStream.Write(rcsEntry.GetData());
// //         }
     

// //         Console.WriteLine("Extracted " + rcsEntry.GetName());

// //         if(rcsEntry.GetName().ToLower().EndsWith(".d3d")) {
// //             D3dFile d3dFile = new(entryPath);
// //             using FileStream d3dStream = new(entryPath, FileMode.Open, FileAccess.Read);
// //             using BinaryReader d3dReader = new(d3dStream);
// //             d3dFile.Read(d3dReader);
// //             d3dFile.WriteToObj(entryPath + ".obj");
// //         }
// //     }
// // }

// // Console.WriteLine("");
// // Console.WriteLine("Wiel d3d");
// // Console.WriteLine("");

// // string d3dPath = "X:/a2racer/data/WIEL2.d3d";
// // D3dFile d3dFile = new(d3dPath);
// // using (FileStream fileStream = new(d3dPath, FileMode.Open, FileAccess.Read))
// // {
// //     using BinaryReader reader = new(fileStream);
// //     d3dFile.Read(reader);
// // }

// // Console.WriteLine(d3dFile.GetFilename());
// // Console.WriteLine(d3dFile.GetNumVertices());
// // Console.WriteLine(d3dFile.GetNumTriangles());

// // d3dFile.WriteToObj("X:/a2racer/data/WIEL2.obj");

// string etappe0Path = "X:/a2racer/data/etappe0";
// string etappe0RmpPath = etappe0Path + ".rmp";

// //  using (FileStream fileStream = new(etappe0RmpPath, FileMode.Open, FileAccess.Read))
// //  {
// //     using BinaryReader reader = new(fileStream);

// //     RmpFile rmpFile = new RmpFile(etappe0RmpPath).Read(reader);
// //     RmpFile.Chunk chunk = rmpFile.GetChunks()[0];

// //     Console.WriteLine("Chunk: ");

// //     Console.WriteLine("Road Planes: " + chunk.GetRoadPlaneCount());
// //     Console.WriteLine("Land Planes: " + chunk.GetLandPlaneCount());
// //     Console.WriteLine("Extra Planes: " + chunk.GetExtraPlaneCount());
// //     Console.WriteLine("Rail Planes: " + chunk.GetRailPlaneCount());

// //     Console.WriteLine("");

// //     Console.WriteLine("Writing to obj...");

// //     string outputPath = etappe0Path + ".chunks.obj";

// //     rmpFile.WriteToObj(outputPath);
// // }

// string etappe0ompPath = etappe0Path + ".omp";

// using (FileStream fileStream = new(etappe0ompPath, FileMode.Open, FileAccess.Read))
// {
//     using BinaryReader reader = new(fileStream);

//     Console.WriteLine("Reading: " + etappe0ompPath);

//     OmpFile ompFile = new OmpFile(etappe0ompPath).Read(reader);

//     Console.WriteLine("Object Count: " + ompFile.GetObjectCount());


//     OmpFile.StaticObject obj = ompFile.GetObjects()[0];
//     Console.WriteLine("Object " + 0);
//     Console.WriteLine("Chunk: " + obj.GetChunk());
//     Console.WriteLine("Type: " + obj.GetObjectType());
//     Console.WriteLine("X: " + obj.GetX());
//     Console.WriteLine("Y: " + obj.GetY());
//     Console.WriteLine("Z: " + obj.GetZ());
//     Console.WriteLine("RotX: " + obj.GetRotX());
//     Console.WriteLine("RotY: " + obj.GetRotY());
//     Console.WriteLine("RotZ: " + obj.GetRotZ());
//     Console.WriteLine("Animname: " + obj.GetAnimname());
//     Console.WriteLine("Animindex: " + obj.GetAnimindex());
//     Console.WriteLine("");
// }


// int E00_ZIJS_ANIM__Location = 0x70354900;

// string name = "rcs";
// string indPath = "X:/a2racer/data/" + name + ".ind";

// IndFile indFile = new IndFile(indPath);
// using (FileStream fileStream = new(indPath, FileMode.Open, FileAccess.Read))
// {
//     using BinaryReader reader = new(fileStream);
//     indFile.Read(reader);
//     Console.WriteLine(indFile.GetFilename());
//     Console.WriteLine(indFile.GetNumEntries());

//     int findIndex = 28;

//     IndFile.IndEntry entry = indFile.GetEntries()[findIndex];

//     if(entry != null) {
//         // log stuff about it
//         Console.WriteLine("Entry: " + entry.GetName());
//         Console.WriteLine("Offset: " + entry.GetOffset());
//         Console.WriteLine("Length: " + entry.GetLength());
//     }
// }

// ConvertAnimIndicies.Convert("X:/a2racer/A2Racer/static/anim_indices_raw.txt", "X:/a2racer/A2Racer/static/anim_indices.csv");