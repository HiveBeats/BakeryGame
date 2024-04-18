using System.Numerics;
using BakeryGame.Components.Common;
using BakeryGame.Components.Environment;
using Raylib_cs;
using Scellecs.Morpeh;

namespace BakeryGame.Entities;

public class BlockFactory
{
    private readonly World _world;

    public BlockFactory(World world)
    {
        _world = world;
    }
    
    public Entity CreateBlock(float x, float z)
    {
        var position = new Vector3(x, 1.0f, z);
        var size = new Vector3(BlockSize, BlockSize, BlockSize);
        
        var block = _world.CreateEntity();
        block.SetComponent(new BlockComponent { Size = size });
        block.SetComponent(new ColorComponent { Color = Color.Gray });
        block.SetComponent(new PositionComponent(){ Position = position });
        
        return block;
    }

    

    public const float BlockSize = 1;
}