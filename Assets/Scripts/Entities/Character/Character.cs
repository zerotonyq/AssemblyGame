using System;
using System.Collections.Generic;
using MoveSystem;
using UnityEngine;

namespace Character
{
    public class Character
    {

        private Dictionary<Type, Component> _components = new();
        private GameObject _characterView;
        
        public Character(GameObject characterView)
        {
            _characterView = characterView;
        }

        public T AddComponentToCharacter<T>() where T : Component
        {
            if (_characterView.TryGetComponent(out T comp))
                throw new Exception(_characterView.name + "The object already has this component");
            var component = _characterView.AddComponent<T>();
            
            _components.Add(typeof(T), component);
            return component;
        }
        public IReadOnlyDictionary<Type, Component> Components => _components;
    }
}