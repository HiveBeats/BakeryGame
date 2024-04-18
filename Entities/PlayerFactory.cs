using System.Numerics;
using BakeryGame.Components.Common;
using BakeryGame.Components.Player;
using Raylib_cs;
using Scellecs.Morpeh;

namespace BakeryGame.Entities;

public  class PlayerFactory
{
    private readonly World _world;

    public PlayerFactory(World world)
    {
        _world = world;
    }
    
    public Entity CreatePlayer(out CameraComponent camera)
    {
        var player = _world.CreateEntity();
        player.SetComponent(new HealthComponent { HealthPoints = 100 });
        player.SetComponent(new PositionComponent { Position = new Vector3(0.0f, 1.0f, 2.0f) });
        player.SetComponent(new MovementComponent() { Speed = 0.1f });
        camera = new CameraComponent()
        {
            Camera = new Camera3D(new(0.0f, 10.0f, 10.0f), new(0.0f, 0.0f, 0.0f), new(0.0f, 1.0f, 0.0f), 45.0f, 0)
        };
        player.SetComponent(camera);
        
        player.SetComponent(new PlayerComponent
        {
            Size = new Vector3(1.0f, 2.0f, 1.0f ),
        });

        return player;
    }

}