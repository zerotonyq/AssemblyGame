using Game.Components.Inventory;
using Game.Components.Inventory.Data;
using Game.Components.Jumping;
using Game.Components.Jumping.Config;
using Game.Components.Rotating;
using Game.Components.Rotating.Config;
using MoveSystem;
using UnityEngine;
using Zenject;

namespace Character
{
    public class CharacterFactory
    {
        private const string DEFAULT_NAME = "default name";

        private GameObject _defaultCharacterPrefab;
        private InventoryInitData _defaultInventoryInitData;
        
        [Inject]
        private void Init(GameObject defaultCharacterPrefab, InventoryInitData defaultInventoryInitData)
        {
            _defaultCharacterPrefab = defaultCharacterPrefab;
            _defaultInventoryInitData = defaultInventoryInitData;
        }
        
        public Character CreateCharacter(
            GameObject prefab = null, 
            InventoryInitData inventoryInitData = null,
            string name = DEFAULT_NAME,
            MoveConfig moveConfig = null,
            RotateConfig rotateConfig = null,
            JumpConfig jumpConfig = null)
        {
            var characterView = GameObject.Instantiate(prefab ? prefab : _defaultCharacterPrefab);
            
            characterView.name = name;
            
            characterView.layer = LayerMask.NameToLayer("Dynamic");
            
            var character = new Character(characterView);
            
            CreateMoveComponent(character, moveConfig);

            CreateRotateComponent(character, rotateConfig);

            CreateJumpComponent(character, jumpConfig);

            CreateInventoryComponent(character, inventoryInitData);

            return character;
        }

        private void CreateMoveComponent(Character character, MoveConfig moveConfig)
        {
            var moveComponent = character.AddComponentToCharacter<MoveComponent>();
            moveComponent.Init(moveConfig ?? MoveConfig.DefaultMoveConfig);
        }

        private void CreateRotateComponent(Character character, RotateConfig rotateConfig)
        {
            var rotateComponent = character.AddComponentToCharacter<RotateComponent>();
            rotateComponent.Init(rotateConfig ?? RotateConfig.DefaultRotateConfig);
        }

        private void CreateJumpComponent(Character character, JumpConfig jumpConfig)
        {
            var jumpComponent = character.AddComponentToCharacter<JumpComponent>();
            jumpComponent.Init(jumpConfig ?? JumpConfig.DefaultJumpConfig);
        }
        
        private void CreateInventoryComponent(Character character, InventoryInitData inventoryInitData)
        {
            var inventoryComponent = character.AddComponentToCharacter<InventoryComponent>();
            inventoryComponent.Init(inventoryInitData ? inventoryInitData : _defaultInventoryInitData);
        }
    }
}