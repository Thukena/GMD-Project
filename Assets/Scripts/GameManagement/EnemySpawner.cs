using System;
using System.Collections;
using Player;
using Shared;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace GameManagement
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject EvilSamuPrefab;
        [SerializeField] private GameObject MoodSwingerPrefab;
        [SerializeField] private float startSpawnInterval;
        [SerializeField] private float spawnDistance;
        private Tilemap tilemap;
        private bool _spawnOnRightSide;
        private Transform playerTransform;
        private GameManager _gameManager;
        private DifficultyManager _difficultyManager;
        private float currentSpawnInterval;
        private float _spawnAreaMinX;
        private float _spawnAreaMaxX;

        private void Start()
        {
            currentSpawnInterval = startSpawnInterval;
            _gameManager = GameManager.Instance;
            _difficultyManager = _gameManager.DifficultyManager;
            playerTransform = PlayerController.Instance.transform;
            FindTileMap();
            SceneController.Instance.OnStageChange += FindTileMap;
            _difficultyManager.OnDifficultyChange += UpdateSpawnInterval;
            StartCoroutine(StartSpawningEnemies());
        }

        private void FindTileMap()
        {
            tilemap = FindObjectOfType<Tilemap>();
            tilemap.CompressBounds();

            var cellBounds = tilemap.cellBounds;
            _spawnAreaMinX = cellBounds.xMin;
            _spawnAreaMaxX = cellBounds.xMax;

            print("_spawnAreaMinX = " + _spawnAreaMinX);
            print("_spawnAreaMaxX = " + _spawnAreaMaxX);
        }

        private void UpdateSpawnInterval()
        {
            currentSpawnInterval = (float)Math.Pow(startSpawnInterval,(double) -_difficultyManager.Difficulty*10 / 100 + 1); // Update spawn interval based on current difficulty
        }
        
        private IEnumerator StartSpawningEnemies()
        {
            while (true)
            {
                yield return new WaitForSeconds(currentSpawnInterval);
                SpawnEnemy();
            }
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

            var offset = (_spawnOnRightSide ? 1 : -1) * spawnDistance;
            var spawnArea = new Vector2(playerTransform.position.x + offset, playerTransform.position.y + 10f);

            if (spawnArea.x > _spawnAreaMaxX || spawnArea.x < _spawnAreaMinX) // Invert the original offset if the enemy is going to spawn outside of the tilemap
            {
                spawnArea.x -= offset * 2;  
            }
            
            var enemy = Instantiate(chosenPrefab, spawnArea, Quaternion.identity);
            enemy.GetComponent<LevelUpManager>().IncreaseLevel(_difficultyManager.Difficulty);
            _spawnOnRightSide = !_spawnOnRightSide;
            var health = enemy.GetComponent<Health>();
            health.OnDeath += () => GameManager.Instance.OnEnemyDeath(34 * _difficultyManager.Difficulty);
            print("Enemy spawned!");
        }
    }
}
