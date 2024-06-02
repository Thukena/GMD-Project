using GameManagement;
using UnityEngine;

namespace UI
{
    public class MainMenuCanvas : MonoBehaviour
    {
        public void StartGame()
        {
            SceneController.Instance.StartGame();
        }
    
        public void ExitGame()
        {
            SceneController.Instance.ExitGame();
        }
    }
}
