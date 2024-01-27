using Game.Components.Inventory;
using Game.Components.Jumping;
using Game.Components.Jumping.Config;
using Game.Components.Rotating;
using Game.Components.Rotating.Config;
using MoveSystem;
using UnityEngine;

namespace Character
{
    public class CharacterFactory
    {
        private const string DEFAULT_NAME = "default name"; 
        public static Character CreateCharacter(
            GameObject prefab, 
            string name = DEFAULT_NAME,
            MoveConfig moveConfig = null,
            RotateConfig rotateConfig = null,
            JumpConfig jumpConfig = null)
        {
            var characterView = GameObject.Instantiate(prefab);
            
            characterView.name = name;
            
            characterView.layer = LayerMask.NameToLayer("Dynamic");
            
            var character = new Character(characterView);
            
            var moveComponent = character.AddComponentToCharacter<MoveComponent>();
            moveComponent.Init(moveConfig ?? MoveConfig.DefaultMoveConfig);

            var rotateComponent = character.AddComponentToCharacter<RotateComponent>();
            rotateComponent.Init(rotateConfig ?? RotateConfig.DefaultRotateConfig);

            var jumpComponent = character.AddComponentToCharacter<JumpComponent>();
            jumpComponent.Init(jumpConfig ?? JumpConfig.DefaultJumpConfig);

            var inventoryComponent = character.AddComponentToCharacter<InventoryComponent>();
            inventoryComponent.Init();
            
            return character;
        }
    }
}