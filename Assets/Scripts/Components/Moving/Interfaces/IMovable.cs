using UnityEngine;

namespace MoveSystem
{
    public interface IMovable
    {
        float AccelerationRate { get; }
        float MaximumWalkSpeed { get; }
        float MaximumFallSpeed { get; }
        void SetDirectionFromInput(Vector2 direction);
    }
}
