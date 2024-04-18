using Raylib_cs;

namespace BakeryGame.Models;

public struct EnvItem
{
    public EnvItem(Rectangle rect, Color color, bool canCollide)
    {
        Rect = rect;
        Color = color;
        CanCollide = canCollide;
    }

    public Rectangle Rect;
    public Color Color;
    public bool CanCollide;
}