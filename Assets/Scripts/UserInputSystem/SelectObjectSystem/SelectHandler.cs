using UnityEngine;
using UnityEngine.InputSystem;

namespace UserInputSystem.SelectObjectSystem.KeyboardMouse
{
    public abstract class SelectHandler
    {
        private GameObject _currentSelectedGameObject;
        private Mesh _currentSelectedMesh;
        protected bool _isSelected = false;

        protected void AssignObject(SelectObject obj)
        {
            _currentSelectedGameObject = obj.gameObject;
            _currentSelectedMesh = obj.Mesh;
        }

        protected void UnassignObject()
        {
            _currentSelectedMesh = null;
            _currentSelectedMesh = null;
        }
        public abstract void Select(InputAction.CallbackContext ctx);

        public GameObject CurrentSelectedGameObject => _currentSelectedGameObject;
        public Mesh CurrentSelectedMesh => _currentSelectedMesh;
        public bool IsSelected => _isSelected;
    }
}