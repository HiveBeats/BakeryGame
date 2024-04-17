// See https://aka.ms/new-console-template for more information

using System.Numerics;
using BakeryGame.Systems;
using Raylib_cs;
using Scellecs.Morpeh;

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

    private static Entity CreateBlock()
    {
        var position = new Vector3( -4.0f, 1.0f, 2.0f );
        var size = new Vector3( 2.0f, 2.0f, 2.0f );
        
        var block = _world.CreateEntity();
        block.SetComponent(new BlockComponent { Size = size });
        block.SetComponent(new ColorComponent { Color = Color.Gray });
        block.SetComponent(new PositionComponent(){ Position = position });

        return block;
    }

    private static Entity CreatePlayer()
    {
        var player = _world.CreateEntity();
        player.SetComponent(new HealthComponent { HealthPoints = 100 });
        player.SetComponent(new PositionComponent { Position = new Vector3(0.0f, 1.0f, 2.0f) });
        player.SetComponent(new MovementComponent() { Speed = 0.1f });
        // ball.SetComponent(new CameraComponent
        // {
        //     Camera = new Cam2qera2D
        //     {
        //         Target = new Vector2(800 / 2 + 10.0f, 480 / 2 + 10.0f),
        //         Offset = new Vector2(800 / 2, 480 / 2),
        //         Rotation = 0.0f,
        //         Zoom = 1.0f
        //     }
        // });
        player.SetComponent(new PlayerComponent
        {
            Size = new Vector3(1.0f, 2.0f, 1.0f ),
        });

        return player;
    }

    private static void Main(string[] args)
    {
        _world = World.Create();

        Raylib.InitWindow(800, 480, "Hello World");

        var player = CreatePlayer();
        var block = CreateBlock();
        var camera = new Camera3D(new ( 0.0f, 10.0f, 10.0f ), new( 0.0f, 0.0f, 0.0f ), new (   0.0f, 1.0f, 0.0f ), 45.0f, 0);

        var healthSystem = new HealthSystem { World = _world };
        var movementSystem = new MovementSystem { World = _world };
        var collisionSystem = new CollisionSystem(_world);
        var systemsGroup = _world.CreateSystemsGroup();
        systemsGroup.AddSystem(healthSystem);
        systemsGroup.AddSystem(movementSystem);
        systemsGroup.AddSystem(collisionSystem);

        systemsGroup.EnableSystem(movementSystem);
        systemsGroup.EnableSystem(healthSystem);
        systemsGroup.EnableSystem(collisionSystem);
        _world.AddSystemsGroup(0, systemsGroup);

        var renderSystemsGroup = _world.CreateSystemsGroup();
        var hpRenreSystem = new HPRenderSystem { World = _world };
        var ballRenderSystem = new BallRenderSystem { World = _world };
        //var cameraSystem = new CameraSystem(_world);
        var blockRenderSystem = new BlockRenderSystem(_world);

        renderSystemsGroup.AddSystem(blockRenderSystem);
        renderSystemsGroup.AddSystem(ballRenderSystem);
        renderSystemsGroup.AddSystem(hpRenreSystem);
        //renderSystemsGroup.AddSystem(cameraSystem);

        renderSystemsGroup.EnableSystem(blockRenderSystem);
        renderSystemsGroup.EnableSystem(ballRenderSystem);
        renderSystemsGroup.EnableSystem(hpRenreSystem);
        //renderSystemsGroup.EnableSystem(cameraSystem);

        _world.AddSystemsGroup(1, renderSystemsGroup);

        Raylib.SetTargetFPS(60);
        while (!Raylib.WindowShouldClose())
        {
            var deltaTime = Raylib.GetFrameTime();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            Raylib.BeginMode3D(camera);
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