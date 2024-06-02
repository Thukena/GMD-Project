using GameManagement;
using UnityEngine;

namespace UI
{
    public class GameOverCanvas : MonoBehaviour
    {
        public void RestartGame()
        {
            SceneController.Instance.StartGame();
        }
    
        public void ExitToMainMenu()
        {
            SceneController.Instance.ExitToMainMenu();
        }
    }
}
