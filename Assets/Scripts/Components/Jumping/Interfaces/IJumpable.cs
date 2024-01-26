namespace Game.Components.Jumping.Interfaces
{
    public interface IJumpable
    {
        void Jump();
        float JumpForce { get; }
    }
}