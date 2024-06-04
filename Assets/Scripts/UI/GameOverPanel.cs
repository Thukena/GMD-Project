using GameManagement;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TimeHandler timeHandler;
        private void OnEnable()
        {
            timerText.text = $"{timeHandler} Minutes!";
        }

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
