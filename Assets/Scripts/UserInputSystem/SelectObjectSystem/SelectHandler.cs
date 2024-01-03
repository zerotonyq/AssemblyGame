using UnityEngine;
using UnityEngine.InputSystem;

namespace UserInputSystem.SelectObjectSystem.KeyboardMouse
{
    public abstract class SelectHandler
    {
        protected GameObject _currentSelectedGameObject;
        private Mesh _currentSelectedMesh;
        protected bool _isSelected = false;

        protected void AssignObject(SelectView obj)
        {
            _currentSelectedGameObject = obj.gameObject;
            _currentSelectedMesh = obj.Mesh;
        }

        protected void UnassignObject()
        {
            _currentSelectedMesh = null;
        }
        public abstract void Select(InputAction.CallbackContext ctx);

        public GameObject CurrentSelectedGameObject => _currentSelectedGameObject;
        public Vector3 CurrentSelectedObjectPosition => _currentSelectedGameObject.transform.position;
        public Mesh CurrentSelectedMesh => _currentSelectedMesh;
        public bool IsSelected => _isSelected;
    }
}