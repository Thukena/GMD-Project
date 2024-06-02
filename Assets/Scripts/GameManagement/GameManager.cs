using Shared;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public GameObject Player;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private PlayerInput playerInput;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Player.GetComponent<Health>().OnDeath += OnPlayerDeath;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnPlayerDeath()
        {
            Time.timeScale = 0; // Pause the game
            playerInput.EnableUIControls();
            gameOverPanel.SetActive(true);
        }
        
        public void RetryGame()
        {
            SceneManager.LoadScene(0); //Restart game
            playerInput.EnableUIControls();
            Time.timeScale = 1; //Resume game
        }

        public void QuitGame()
        {
            print("QUIT GAME!");
        }
        
    }
}