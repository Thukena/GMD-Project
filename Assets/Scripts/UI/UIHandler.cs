using Player;
using Shared;
using UnityEngine;

namespace UI
{
    public class UIHandler : MonoBehaviour
    {
        public static UIHandler Instance;
        [SerializeField] private GameOverPanel gameOverPanel;
        
        private void Awake()
        { 
            if (Instance == null)
            {
                Instance = this;
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
    }
}