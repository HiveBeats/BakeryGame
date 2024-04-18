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

    private static readonly EnvItem[] _envItems =
    {
        new EnvItem(new Rectangle(0, 0, 1000, 400), Color.LightGray, false),
        new EnvItem(new Rectangle(0, 400, 1000, 200), Color.Gray, true),
        new EnvItem(new Rectangle(300, 200, 400, 100), Color.Gray, true),
        new EnvItem(new Rectangle(250, 300, 100, 10), Color.Gray, true),
        new EnvItem(new Rectangle(650, 300, 100, 10), Color.Gray, true)
    };

    

    private static void Main(string[] args)
    {
        _world = World.Create();
        var playerFactory = new PlayerFactory(_world);
        var blockFactory = new BlockFactory(_world);
        
        Raylib.InitWindow(800, 480, "Hello World");

        var player = playerFactory.CreatePlayer(out var camera);
        var block = blockFactory.GenerateMapOfBlocks().ToList();

        SystemRegistrations.RegisterLogicGroup(_world);
        SystemRegistrations.RegisterGraphicsGroup(_world);

        Raylib.SetTargetFPS(60);
        while (!Raylib.WindowShouldClose())
        {
            var deltaTime = Raylib.GetFrameTime();
            //Raylib.UpdateCamera(ref camera.Camera, CameraMode.Free);
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            Raylib.BeginMode3D(camera.Camera);
            _world.Update(deltaTime);
            Raylib.DrawGrid(10, 1.0f);
            _world.CleanupUpdate(deltaTime);
            Raylib.EndMode3D();
            Raylib.DrawFPS(100, 10);
            _world.LateUpdate(deltaTime);
            _world.Commit();
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}