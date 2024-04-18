using BakeryGame.Components.Common;
using BakeryGame.Components.Player;
using Raylib_cs;
using Scellecs.Morpeh;

namespace BakeryGame.Systems.Rendering;

public class SliderSystem:ILateSystem
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
        
        if (Raylib.IsMouseButtonPressed(MouseButton.Left)) {
            // Check if mouse is clicked within the slider rectangle
            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), sliderRect)) {
                isSliderActive = true;  // Slider is being interacted with
            }
        }
        
        if (Raylib.IsMouseButtonReleased(MouseButton.Left)) {
            isSliderActive = false;  // Stop slider interaction
        }
            
        // Update slider value when slider is active
        if (isSliderActive) {
            // // Calculate new slider value based on mouse position
             float mousePosX = Raylib.GetMouseX();
             sliderValue = minValue + (mousePosX - sliderRect.X) / sliderRect.Width * (maxValue - minValue);

            // Clamp slider value within the valid range
            if (sliderValue < minValue) sliderValue = minValue;
            if (sliderValue > maxValue) sliderValue = maxValue;

            // ref var cameraComponent = ref player.GetComponent<CameraComponent>();
            // var camera = cameraComponent.Camera.GetCamera3D();
            // cameraComponent.Camera.SetTarget(camera.Target with {X = sliderValue});
        }
        
        // Draw slider background
        Raylib.DrawRectangleRec(sliderRect, Color.LightGray);

        // Draw slider handle
        float sliderHandlePosX = sliderRect.X + (sliderValue - minValue) / (maxValue - minValue) * sliderRect.Width;
        var sliderHandleRect = new Rectangle( sliderHandlePosX - 5, sliderRect.Y - 5, 10, sliderRect.Height + 10 );
        Raylib.DrawRectangleRec(sliderHandleRect, Color.Blue);
        isSliderActive = false;
    }
}