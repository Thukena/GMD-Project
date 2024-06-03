using Player;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    public class SceneController : MonoBehaviour
    {
        public static SceneController Instance { get; private set; }
        private int _currentStage = 1;
        
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
        
        public void StartGame()
        {
            SceneManager.LoadScene("Stage1");
            PlayerInput.Instance.EnablePlayerControls();
            Time.timeScale = 1;
        }

        public void RestartGame()
        {
            ResetGame();
            StartGame();
        }

        private void ResetGame()
        {
            Destroy(GameManager.Instance.gameObject);
            Destroy(UIHandler.Instance.gameObject);
            Destroy(PlayerController.Instance.gameObject);
            _currentStage = 1;
        }
        
        public void ExitToMainMenu()
        {
            ResetGame();
            SceneManager.LoadScene("MainMenu"); 
        }

        public void StartNextStage()
        {
            _currentStage++;
            PlayerController.Instance.transform.position = new Vector2(0, 0);
            SceneManager.LoadScene($"Stage{_currentStage}"); 
        }

        public void ExitGame()
        {
            print("QUIT GAME!");
            Application.Quit();
        }
    }
}
