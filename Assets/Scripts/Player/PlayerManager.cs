using Shared;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }
        public GameObject Player;

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

        private static void OnPlayerDeath()
        {
            SceneManager.LoadScene(0); //Restart the game
        }
        
    }
}