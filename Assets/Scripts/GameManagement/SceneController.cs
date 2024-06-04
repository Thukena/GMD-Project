using System;
using Player;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    public class SceneController : MonoBehaviour
    {
        public static SceneController Instance { get; private set; }
        public int currentStage = 1;
        public event Action OnStageChange;
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
            Destroy(AudioManager.Instance.gameObject);
            currentStage = 1;
        }
        
        public void ExitToMainMenu()
        {
            ResetGame();
            SceneManager.LoadScene("MainMenu"); 
        }

        public void StartNextStage()
        {
            currentStage++;
            PlayerController.Instance.transform.position = new Vector2(0, 0);
            SceneManager.LoadScene($"Stage{currentStage}");
        }

        public void ExitGame()
        {
            print("QUIT GAME!");
            Application.Quit();
        }
    }
}
