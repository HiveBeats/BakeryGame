using BakeryGame.Models;
using Raylib_cs;
using Scellecs.Morpeh;

public sealed class MovementSystem : ISystem
{
    private Filter _filter;
    public World World { get; set; }

    public void Dispose()
    {
    }

    public void OnAwake()
    {
        _filter = World.Filter.With<PlayerComponent>().Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            ref var positionComponent = ref entity.GetComponent<PositionComponent>();
            ref var directionComponent = ref entity.GetComponent<MovementComponent>();
            if (Raylib.IsKeyDown(KeyboardKey.Right))
            {
                positionComponent.Position.X += directionComponent.Speed;
                directionComponent.Direction = Direction.Right;
            }

            if (Raylib.IsKeyDown(KeyboardKey.Left))
            {
                positionComponent.Position.X -= directionComponent.Speed;
                directionComponent.Direction = Direction.Left;
            }

            if (Raylib.IsKeyDown(KeyboardKey.Up))
            {
                positionComponent.Position.Z -= directionComponent.Speed;
                directionComponent.Direction = Direction.Up;
            }

            if (Raylib.IsKeyDown(KeyboardKey.Down))
            {
                positionComponent.Position.Z += directionComponent.Speed;
                directionComponent.Direction = Direction.Down;
            }
        }
    }
}