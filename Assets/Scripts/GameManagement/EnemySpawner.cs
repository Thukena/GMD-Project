using UnityEngine;

namespace GameManagement
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject EvilSamuPrefab;
        [SerializeField] private GameObject MoodSwingerPrefab;
        [SerializeField] private float spawnInterval = 10.0f; 

        private void Start()
        {
            InvokeRepeating("SpawnEnemy", 0.0f, spawnInterval);
        }

        private void SpawnEnemy()
        {
            Instantiate(EvilSamuPrefab, new Vector3(0, 10, 0), Quaternion.identity);
        }
    }
}
