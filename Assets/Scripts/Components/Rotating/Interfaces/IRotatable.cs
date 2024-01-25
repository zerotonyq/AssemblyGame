using UnityEngine;

namespace Game.Components.Rotating.Interfaces
{
    public interface IRotatable
    {
        bool IsRotating { get; }
        float RotationSpeed { get; }
        void LookAt(Vector3 position);

    }
}