using BakeryGame.Components.Common;
using BakeryGame.Components.Player;
using Raylib_cs;
using Scellecs.Morpeh;

namespace BakeryGame.Systems.Player;

public class PlayerRenderSystem : ISystem
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