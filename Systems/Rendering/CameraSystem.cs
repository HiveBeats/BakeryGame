using BakeryGame.Components.Common;
using BakeryGame.Components.Player;
using Raylib_cs;
using Scellecs.Morpeh;

namespace BakeryGame.Systems.Rendering;

public class CameraSystem: ILateSystem
{
    private Filter _player;
    float sliderValue = 50.0f;  // Initial value of the slider
    float minValue = -20.0f;      // Minimum value of the slider
    float maxValue = 20.0f;    // Maximum value of the slider
    Rectangle sliderRect = new Rectangle( 100, 50, 200, 20 );  // Rectangle defining the slider's position and size
    bool isSliderActive = false;
    private const float CameraSpeed = 0.1f;
    private const float DistanceToPlayer = 10.0f;
    
    public void Dispose()
    {
    }

    public void OnAwake()
    {
        _player = World.Filter.With<PlayerComponent>().Build();
    }

    public World World { get; set; }
    
    private float Lerp(float a, float b, float t) 
    {
        return a + (b - a) * t;
    }
    
    public void OnUpdate(float deltaTime)
    {
        var player = _player.First();
        ref var cameraComponent = ref player.GetComponent<CameraComponent>();
        var camera = cameraComponent.Camera.GetCamera3D();
        var playerPosition = player.GetComponent<PositionComponent>().Position;
        // Update camera position to follow the player
        cameraComponent.Camera.SetPosition(camera.Position with {X = Lerp(camera.Position.X, playerPosition.X, CameraSpeed), Z = Lerp(camera.Position.Z, playerPosition.Z + DistanceToPlayer, CameraSpeed)});
        // Update camera target to look at the player
        cameraComponent.Camera.SetTarget(playerPosition);
    }
}