using BakeryGame.Systems.Common;
using BakeryGame.Systems.Player;
using BakeryGame.Systems.Rendering;
using Scellecs.Morpeh;

namespace BakeryGame;

public static class SystemRegistrations
{
    public static void RegisterLogicGroup(World world)
    {
        var healthSystem = new HealthSystem { World = world };
        var movementSystem = new MovementSystem { World = world };
        var collisionSystem = new CollisionSystem(world);
        
        var systemsGroup = world.CreateSystemsGroup();
        systemsGroup.AddSystem(healthSystem);
        systemsGroup.AddSystem(movementSystem);
        systemsGroup.AddSystem(collisionSystem);
        
        
        systemsGroup.EnableSystem(movementSystem);
        systemsGroup.EnableSystem(healthSystem);
        systemsGroup.EnableSystem(collisionSystem);
        world.AddSystemsGroup(0, systemsGroup);
    }

    public static void RegisterGraphicsGroup(World world)
    {
        var renderSystemsGroup = world.CreateSystemsGroup();
        var hpRenreSystem = new HPRenderSystem { World = world };
        var ballRenderSystem = new PlayerRenderSystem { World = world };
        var cameraSystem = new CameraSystem() {World = world };
        var blockRenderSystem = new BlockRenderSystem(world);

        renderSystemsGroup.AddSystem(blockRenderSystem);
        renderSystemsGroup.AddSystem(ballRenderSystem);
        renderSystemsGroup.AddSystem(hpRenreSystem);
        renderSystemsGroup.AddSystem(cameraSystem);

        renderSystemsGroup.EnableSystem(blockRenderSystem);
        renderSystemsGroup.EnableSystem(ballRenderSystem);
        renderSystemsGroup.EnableSystem(hpRenreSystem);
        renderSystemsGroup.EnableSystem(cameraSystem);

        world.AddSystemsGroup(1, renderSystemsGroup);
    }
}