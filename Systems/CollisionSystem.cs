using System.Numerics;
using BakeryGame.Models;
using Raylib_cs;
using Scellecs.Morpeh;

namespace BakeryGame.Systems;

public class CollisionSystem: ISystem
{
    private Filter _playerFilter;
    private Filter _blockFilter;
    public World World { get; set; }

    public CollisionSystem(World world)
    {
        World = world;
    }
    
    public void Dispose()
    {
        
    }

    public void OnAwake()
    {
        _playerFilter = World.Filter.With<PlayerComponent>().Build();
        _blockFilter = World.Filter.With<BlockComponent>().Build();
    }

    
    public void OnUpdate(float deltaTime)
    {
        var player = _playerFilter.First();
        ref var playerPosition = ref player.GetComponent<PositionComponent>();
        var playerSize = player.GetComponent<PlayerComponent>().Size;
        var playerMovement = player.GetComponent<MovementComponent>();
        foreach (var block in _blockFilter)
        {
            var blockPosition = block.GetComponent<PositionComponent>().Position;
            var blockSize = block.GetComponent<BlockComponent>().Size;
            var blockBounds = new BoundingBox(
                new Vector3(blockPosition.X - blockSize.X / 2, blockPosition.Y - blockSize.Y / 2, 
                    blockPosition.Z - blockSize.Z / 2), 
                new Vector3(blockPosition.X + blockSize.X / 2, blockPosition.Y + blockSize.Y / 2, 
                    blockPosition.Z + blockSize.Z / 2));
            
            
            var collided = false;
            do
            {
                var playerBounds = new BoundingBox(
                    new Vector3(playerPosition.Position.X - playerSize.X / 2, playerPosition.Position.Y - playerSize.Y / 2,
                        playerPosition.Position.Z - playerSize.Z / 2),
                    new Vector3(playerPosition.Position.X + playerSize.X / 2, playerPosition.Position.Y + playerSize.Y / 2,
                        playerPosition.Position.Z + playerSize.Z / 2));
                
                collided = Raylib.CheckCollisionBoxes(playerBounds, blockBounds);
                if (collided)
                {
                    var delta = playerMovement.Speed / 2;
                    switch (playerMovement.Direction)
                    {
                        case Direction.Left:
                            playerPosition.Position.X += delta;
                            break;
                        case Direction.Right:
                            playerPosition.Position.X -= delta;
                            break;
                        case Direction.Up:
                            playerPosition.Position.Z += delta;
                            break;
                        case Direction.Down:
                            playerPosition.Position.Z -= delta;
                            break;
                    }
                }
            } while (collided);
        }
    }
}