using System.Numerics;
using Raylib_cs;
using Scellecs.Morpeh;

public class BallRenderSystem : ISystem
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
            Raylib.DrawCubeV(entity.GetComponent<PositionComponent>().Position, entity.GetComponent<PlayerComponent>().Size,  Color.Green);
        }
    }
}