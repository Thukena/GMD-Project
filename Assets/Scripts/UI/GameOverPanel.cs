using GameManagement;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI stageText;
        [SerializeField] private TimeHandler timeHandler;
        private void OnEnable()
        {
            timerText.text = $"{timeHandler} Minutes";
            stageText.text = $"And made it to Stage {SceneController.Instance.currentStage}!";
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
