// See https://aka.ms/new-console-template for more information

using System.Numerics;
using BakeryGame.Components.Common;
using BakeryGame.Components.Environment;
using BakeryGame.Components.Player;
using BakeryGame.Entities;
using BakeryGame.Models;
using BakeryGame.Systems;
using BakeryGame.Systems.Common;
using BakeryGame.Systems.Player;
using BakeryGame.Systems.Rendering;
using Raylib_cs;
using Scellecs.Morpeh;

namespace BakeryGame;

internal class Program
{
    private static World _world;
    private static int WindowWidth = 800;
    private static int WindowHeight = 480;

    private static void Main(string[] args)
    {
        _world = World.Create();
        var playerFactory = new PlayerFactory(_world);
        var blockFactory = new BlockFactory(_world);
        
        Raylib.InitWindow(WindowWidth, WindowHeight, "Hello World");

        CameraComponent camera;
        var player = playerFactory.CreatePlayer(out camera);
        var block = RoomBuilder.GenerateMapOfBlocks(blockFactory).ToList();

        SystemRegistrations.RegisterLogicGroup(_world);
        SystemRegistrations.RegisterGraphicsGroup(_world);

        Raylib.SetTargetFPS(60);
        while (!Raylib.WindowShouldClose())
        {
            var deltaTime = Raylib.GetFrameTime();
            //Raylib.UpdateCamera(ref camera.Camera, CameraMode.Custom);
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            Raylib.BeginMode3D(camera.Camera.GetCamera3D());
            _world.Update(deltaTime);
            Raylib.DrawGrid(RoomBuilder.RoomSize, 1.0f);
            _world.CleanupUpdate(deltaTime);
            Raylib.EndMode3D();
            Raylib.DrawFPS(WindowWidth - 100, 12);
            _world.LateUpdate(deltaTime);
            _world.Commit();
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}