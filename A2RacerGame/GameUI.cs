using Raylib_cs;
using static Raylib_cs.Raylib;

class GameUI
{
    public void Update() { }

    public void Render()
    {
        DrawText($"FPS: {GetFPS()}", 10, 10, 20, Color.DarkBlue);
        DrawText($"Pos: {Game.GameCamera.GetPosition()}", 10, 40, 20, Color.DarkBlue);
        DrawText($"Chunk: {Game.GameWorldManager.GetChunkID()}", 10, 60, 20, Color.DarkBlue);
        DrawText(
            $"Speed: {Game.GameWorldManager.GetCar().GetSpeed():0} km/h",
            10,
            80,
            20,
            Color.DarkBlue
        );
    }
}
