using UnityEngine;
using UnityEngine.InputSystem;

namespace GameManagement
{
    public class PlayerInput : MonoBehaviour
    {
        public InputActionAsset inputActions;

        private InputActionMap _playerActionMap;
        private InputActionMap _uiActionMap;
        public static PlayerInput Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _playerActionMap = inputActions.FindActionMap("Player");
                _uiActionMap = inputActions.FindActionMap("UI");
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
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
