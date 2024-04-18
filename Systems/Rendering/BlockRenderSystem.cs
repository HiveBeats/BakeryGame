using BakeryGame.Components.Common;
using BakeryGame.Components.Environment;
using Raylib_cs;
using Scellecs.Morpeh;

namespace BakeryGame.Systems.Rendering;

public class BlockRenderSystem : ISystem
{
    private Filter _filter;

    public BlockRenderSystem(World world)
    {
        World = world;
    }

    public World World { get; set; }

    public void Dispose()
    {
    }

    public void OnAwake()
    {
        _filter = World.Filter.With<BlockComponent>().Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var item in _filter)
        {
            var size = item.GetComponent<BlockComponent>().Size;
            var positionComponent = item.GetComponent<PositionComponent>();
            var itemColorComponent = item.GetComponent<ColorComponent>();
            
            // Draw enemy-box
            Raylib.DrawCube(positionComponent.Position, size.X, size.Y, size.Z, itemColorComponent.Color);
        }
    }
}