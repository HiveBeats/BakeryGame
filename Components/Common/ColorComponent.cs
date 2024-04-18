using Raylib_cs;
using Scellecs.Morpeh;

namespace BakeryGame.Components.Common;

public struct ColorComponent : IComponent
{
    public Color Color { get; set; }
}