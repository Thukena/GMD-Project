using Player;
using Shared;
using UnityEngine;

namespace GameManagement
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject EvilSamuPrefab;
        [SerializeField] private GameObject MoodSwingerPrefab;
        [SerializeField] private float spawnInterval;
        [SerializeField] private float spawnDistance;
        private bool _spawnOnRightSide;
        private Transform playerTransform;
        private GameManager _gameManager;
        private DifficultyManager _difficultyManager;
        private void Start()
        {
            _gameManager = GameManager.Instance;
            _difficultyManager = _gameManager.DifficultyManager;
            playerTransform = PlayerController.Instance.transform;
            InvokeRepeating("SpawnEnemy", 0.0f, spawnInterval);
        }

        private void SpawnEnemy()
        {
            var random = Random.Range(0f, 100f);

            GameObject chosenPrefab;
            
            if (random < 50f)
            {
                chosenPrefab = EvilSamuPrefab;
            }
            else
            {
                chosenPrefab = MoodSwingerPrefab;
            }

            var spawnArea = new Vector2(playerTransform.position.x + (_spawnOnRightSide ? 1 : -1) * spawnDistance, playerTransform.position.y + 10);
            var enemy = Instantiate(chosenPrefab, spawnArea, Quaternion.identity);
            enemy.GetComponent<LevelUpManager>().IncreaseLevel(_difficultyManager.Difficulty);
            _spawnOnRightSide = !_spawnOnRightSide;
            var health = enemy.GetComponent<Health>();
            health.OnDeath += () => GameManager.Instance.OnEnemyDeath(34 * _difficultyManager.Difficulty);
        }
    }
}
