using BakeryGame.Models;
using Scellecs.Morpeh;

namespace BakeryGame.Components.Player;

public struct MovementComponent : IComponent
{
    public Direction Direction;
    public float Speed;
}