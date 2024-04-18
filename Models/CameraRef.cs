using System.Numerics;
using Raylib_cs;

namespace BakeryGame.Models;

public class CameraRef
{
    private Camera3D _cam;

    public CameraRef(Vector3 position, Vector3 target, Vector3 up, float fovy)
    {
        _cam = new Camera3D(position, target, up, fovy, 0);
    }

    /// <summary>
    /// Camera position
    /// </summary>
    public void SetPosition(Vector3 position)
    {
        _cam.Position = position;
    }

    /// <summary>
    /// Camera target it looks-at
    /// </summary>
    public void SetTarget(Vector3 target)
    {
        _cam.Target = target;
    }

    /// <summary>
    /// Camera up vector (rotation over its axis)
    /// </summary>
    public void SetUp(Vector3 up)
    {
        _cam.Up = up;
    }

    /// <summary>
    /// Camera field-of-view apperture in Y (degrees) in perspective, used as near plane width in orthographic
    /// </summary>
    public void FovY(float fovy)
    {
        _cam.FovY = fovy;
    }

    /// <summary>
    /// Camera type, defines projection type: CAMERA_PERSPECTIVE or CAMERA_ORTHOGRAPHIC
    /// </summary>
    public void SetProjection(CameraProjection projection)
    {
        _cam.Projection = projection;
    }

    public Camera3D GetCamera3D() => _cam;
}