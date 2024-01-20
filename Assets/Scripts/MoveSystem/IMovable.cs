using UnityEngine;

namespace MoveSystem
{
    public interface IMovable
    {
        float WalkSpeed { get; }
        float MaximumSpeed { get; }
        void Move(Vector2 direction);
    }
}
