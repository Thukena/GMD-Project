using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    public class SceneController : MonoBehaviour
    {
        public static SceneController Instance { get; private set; }
        private int _currentState = 1;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Keep SceneController between scenes
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void StartGame()
        {
            _currentState = 1;
            SceneManager.LoadScene("Stage1");
            Time.timeScale = 1;
        }
        
        public void ExitToMainMenu()
        {
            SceneManager.LoadScene("MainMenu"); 
        }

        public void StartNextStage()
        {
            _currentState++;
            SceneManager.LoadScene($"Stage{_currentState}"); 
        }

        public void ExitGame()
        {
            print("QUIT GAME!");
            Application.Quit();
        }
    }
}
