using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    public class SceneController : MonoBehaviour
    {
        public static SceneController Instance { get; private set; }
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
            SceneManager.LoadScene("Stage1");
            Time.timeScale = 1;
        }
        
        public void ExitToMainMenu()
        {
            SceneManager.LoadScene("MainMenu"); 
        }

        public void ExitGame()
        {
            print("QUIT GAME!");
            Application.Quit();
        }
    }
}
