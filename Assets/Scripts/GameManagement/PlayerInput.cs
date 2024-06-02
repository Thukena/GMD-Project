using UnityEngine;
using UnityEngine.InputSystem;

namespace GameManagement
{
    public class PlayerInput : MonoBehaviour
    {
        public InputActionAsset inputActions;

        private InputActionMap _playerActionMap;
        private InputActionMap _uiActionMap;

        private void Awake()
        {
            _playerActionMap = inputActions.FindActionMap("Player");
            _uiActionMap = inputActions.FindActionMap("UI");
        }

        public void EnablePlayerControls()
        {
            _uiActionMap.Disable();
            _playerActionMap.Enable();
        }

        public void EnableUIControls()
        {
            _playerActionMap.Disable();
            _uiActionMap.Enable();
        }
    }
}
