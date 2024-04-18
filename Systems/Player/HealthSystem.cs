using BakeryGame.Components.Player;
using Raylib_cs;
using Scellecs.Morpeh;

namespace BakeryGame.Systems.Player;

public sealed class HealthSystem : ISystem
{
    private Filter filter;

    public World World { get; set; }

    public void Dispose()
    {
    }

    public void OnAwake()
    {
        filter = World.Filter.With<HealthComponent>().Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in filter)
        {
            ref var healthComponent = ref entity.GetComponent<HealthComponent>();
            if (Raylib.IsKeyDown(KeyboardKey.Enter))
                healthComponent.HealthPoints++;
        }
    }
}