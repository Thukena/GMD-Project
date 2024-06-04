using Player;
using Shared;
using UI;
using UnityEngine;

namespace GameManagement
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public DifficultyManager DifficultyManager;
        [SerializeField] private LevelUpManager playerLevelUpManager;
        private AudioManager _audioManager;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                PlayerController.Instance.GetComponent<Health>().OnDeath += OnPlayerDeath;
                _audioManager = AudioManager.Instance;
                playerLevelUpManager.OnLevelUp += () => _audioManager.Play("LevelUp");
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnPlayerDeath()
        {
            Time.timeScale = 0; // Pause the game
            PlayerInput.Instance.EnableUIControls();
            UIHandler.Instance.ShowGameOverPanel(true);
        }

        public void OnEnemyDeath(int xp)
        {
            playerLevelUpManager.AddXp(xp);
        }
    }
}