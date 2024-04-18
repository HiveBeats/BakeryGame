using BakeryGame.Components.Common;
using BakeryGame.Components.Player;
using Scellecs.Morpeh;

namespace BakeryGame.Systems.Player;

public class CameraSystem : ISystem
{
    private Filter _filter;

    public CameraSystem(World world)
    {
        World = world;
    }

    public World World { get; set; }


    public void OnAwake()
    {
        _filter = World.Filter.With<CameraComponent>().Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            var position = entity.GetComponent<PositionComponent>().Position;
            ref var cameraComponent = ref entity.GetComponent<CameraComponent>();

            ref var camera = ref cameraComponent.Camera;
            // Camera target follows player
            //camera.Target.X = position.X;
            //camera.Target.Z = position.Z;

            // // Camera rotation controls
            // if (IsKeyDown(KEY_A)) camera.rotation--;
            // else if (IsKeyDown(KEY_S)) camera.rotation++;

            // Limit camera rotation to 80 degrees (-40 to 40)
            // if (camera.Rotation > 40) camera.Rotation = 40;
            // else if (camera.Rotation < -40) camera.Rotation = -40;

            // Camera zoom controls
            // camera.Zoom += Raylib.GetMouseWheelMove() * 0.05f;
            //
            // if (camera.Zoom > 3.0f) camera.Zoom = 3.0f;
            // else if (camera.Zoom < 0.1f) camera.Zoom = 0.1f;

            // // Camera reset (zoom and rotation)
            // if (IsKeyPressed(KEY_R))
            // {
            //     camera.zoom = 1.0f;
            //     camera.rotation = 0.0f;
            // }
        }
    }

    public void Dispose()
    {
    }
}