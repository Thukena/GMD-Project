using System;
using UI;
using UnityEngine;

namespace GameManagement
{
    public class DifficultyManager : MonoBehaviour
    {

        [SerializeField] private TimeHandler timeHandler;
        public int Difficulty { get; private set; }
        private SceneController sceneController;
        private void Start()
        {
            sceneController = SceneController.Instance;
        }

        private void Update()
        {
            //TODO Should be event every minute and stageChange instead
            Difficulty = (int) timeHandler.time / 60 * sceneController.currentStage;
        }
    }
}
