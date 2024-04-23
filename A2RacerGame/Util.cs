using System.Numerics;
using Jitter2.Dynamics;
using Jitter2.LinearMath;
using Raylib_cs;
using static Raylib_cs.Raylib;

class Util
{
    public static bool IsInBounds(Vector3 pos, BoundingBox box)
    {
        return pos.X >= box.Min.X
            && pos.X <= box.Max.X
            && pos.Y >= box.Min.Y
            && pos.Y <= box.Max.Y
            && pos.Z >= box.Min.Z
            && pos.Z <= box.Max.Z;
    }

    public static int Wrap(int value, int min, int max)
    {
        int range = max - min;
        return ((value - min) % range + range) % range + min;
    }

    public static bool IsWrappedBetween(int value, int minValue, int maxValue)
    {
        return minValue < maxValue
            ? value >= minValue && value < maxValue
            : value >= minValue || value < maxValue;
    }

    public static unsafe Mesh D3DToMesh(D3dFile d3dFile)
    {
        Mesh mesh = new(d3dFile.GetNumVertices(), d3dFile.GetNumFaces());
        mesh.AllocVertices();
        mesh.AllocIndices();
        mesh.AllocTexCoords();

        for (int i = 0; i < d3dFile.GetNumVertices(); i++)
        {
            D3dFile.Vertex vertex = d3dFile.GetVertices()[i];

            mesh.Vertices[i * 3] = vertex.x / GameWorld.WORLD_SIZE;
            mesh.Vertices[i * 3 + 1] = vertex.y / GameWorld.WORLD_SIZE;
            mesh.Vertices[i * 3 + 2] = vertex.z / GameWorld.WORLD_SIZE;

            mesh.TexCoords[i * 2] = vertex.u;
            mesh.TexCoords[i * 2 + 1] = 1f - vertex.v;

            // boundMin = Vector3.Min(
            //     boundMin,
            //     new Vector3(
            //         vertex.x / GameWorld.WORLD_SIZE,
            //         vertex.y / GameWorld.WORLD_SIZE,
            //         vertex.z / GameWorld.WORLD_SIZE
            //     )
            // );
            // boundMax = Vector3.Max(
            //     boundMax,
            //     new Vector3(
            //         vertex.x / GameWorld.WORLD_SIZE,
            //         vertex.y / GameWorld.WORLD_SIZE,
            //         vertex.z / GameWorld.WORLD_SIZE
            //     )
            // );
        }

        for (int i = 0; i < d3dFile.GetNumFaces(); i++)
        {
            mesh.Indices[i * 3] = (ushort)d3dFile.GetFaces()[i].idx0;
            mesh.Indices[i * 3 + 1] = (ushort)d3dFile.GetFaces()[i].idx1;
            mesh.Indices[i * 3 + 2] = (ushort)d3dFile.GetFaces()[i].idx2;
        }

        UploadMesh(ref mesh, false);

        return mesh;
    }

    public static Matrix4x4 GetRayLibTransformMatrix(RigidBody body)
    {
        JMatrix ori = body.Orientation;
        JVector pos = body.Position;

        return new Matrix4x4(
            ori.M11,
            ori.M12,
            ori.M13,
            pos.X,
            ori.M21,
            ori.M22,
            ori.M23,
            pos.Y,
            ori.M31,
            ori.M32,
            ori.M33,
            pos.Z,
            0,
            0,
            0,
            1.0f
        );
    }
}
