using GameManagement;
using UnityEngine;

namespace UI
{
    public class GameOverPanel : MonoBehaviour
    {
        public void RestartGame()
        {
            SceneController.Instance.RestartGame();
        }
    
        public void ExitToMainMenu()
        {
            SceneController.Instance.ExitToMainMenu();
        }
    }
}
