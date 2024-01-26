using UnityEngine;

namespace Game.Components.Jumping.Config
{
    public class JumpConfig
    {
        private const float DEFAULT_JUMP_FORCE = 5f;
        private readonly int DEFAULT_FLOOR_LAYERMASK = LayerMask.NameToLayer("Floor");
        
        private float _jumpForce;
        private int _floorLayerMask;
        
        public JumpConfig(
            int? floorLayerMask,
            float jumpForce = DEFAULT_JUMP_FORCE)
        {
            _jumpForce = jumpForce;
            _floorLayerMask = floorLayerMask ?? DEFAULT_FLOOR_LAYERMASK;
        }
        
        public float JumpForce => _jumpForce;
        public int FloorLayerMask => _floorLayerMask;
        
        public static JumpConfig DefaultJumpConfig = new(null);
    }
}