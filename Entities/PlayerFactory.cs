using System.Numerics;
using BakeryGame.Components.Common;
using BakeryGame.Components.Player;
using BakeryGame.Models;
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
            Camera = new CameraRef(new(0.0f, 20.0f, 10.0f), new(0.0f, 0.0f, 0.0f), new(0.0f, 1.0f, 0.0f), 60.0f)
        };
        player.SetComponent(camera);
        
        player.SetComponent(new PlayerComponent
        {
            Size = new Vector3(1.0f, 2.0f, 1.0f ),
        });

        return player;
    }

}