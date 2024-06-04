using System;
using UI;
using UnityEngine;

namespace GameManagement
{
    public class DifficultyManager : MonoBehaviour
    {
        [SerializeField] private TimeHandler timeHandler;
        public int Difficulty = 1;
        public event Action OnDifficultyChange;
        private SceneController sceneController;
        private void Start()
        {
            sceneController = SceneController.Instance;
            timeHandler.OnMinutePassed += UpdateDifficulty;
            sceneController.OnStageChange += UpdateDifficulty;
        }

        private void UpdateDifficulty()
        {
            Difficulty =  1 + (int) timeHandler.time / 60 * sceneController.currentStage;
            OnDifficultyChange?.Invoke();
        }
    }
}
