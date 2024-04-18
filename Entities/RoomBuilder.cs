using Scellecs.Morpeh;

namespace BakeryGame.Entities;

public class RoomBuilder
{
    public static int RoomSize = 16;
    
    public static IEnumerable<Entity> GenerateMapOfBlocks(BlockFactory blockFactory)
    {
        for (int x = -RoomSize / 2; x <= RoomSize / 2; x++) {
            for (int z = -RoomSize / 2; z <= RoomSize / 2; z++) {
                if (x == -RoomSize / 2 || x == RoomSize / 2 || z == -RoomSize / 2 || z == RoomSize / 2)
                {
                    yield return blockFactory.CreateBlock(x, z);
                }
            }
        }
    }
}