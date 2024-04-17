using Raylib_cs;
using Scellecs.Morpeh;

public class HPRenderSystem : ILateSystem
{
    private Filter _filter;
    public World World { get; set; }

    public void Dispose()
    {
    }

    public void OnAwake()
    {
        _filter = World.Filter.With<HealthComponent>().Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            var healthComponent = entity.GetComponent<HealthComponent>();
            Raylib.DrawText($"HP: {healthComponent.HealthPoints}", 12, 12, 20, Color.Black);
        }
    }
}