using GameManagement;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIHandler : MonoBehaviour
    {
        public static UIHandler Instance;
        [SerializeField] private GameOverPanel gameOverPanel;
        [SerializeField] private TextMeshProUGUI stageText;
        
        private void Awake()
        { 
            if (Instance == null)
            {
                Instance = this;
                SceneController.Instance.OnStageChange += UpdateStageText;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void ShowGameOverPanel(bool show)
        {
            gameOverPanel.gameObject.SetActive(show);
        }

        private void UpdateStageText()
        {
            stageText.text = $"Stage {SceneController.Instance.currentStage}";
        }
    }
}