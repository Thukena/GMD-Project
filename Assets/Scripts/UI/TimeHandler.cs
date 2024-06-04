using TMPro;
using UnityEngine;

namespace UI
{
    public class TimeHandler : MonoBehaviour
    {
        public float time;
        [SerializeField] private TextMeshProUGUI timerText;

        private void Update()
        {
            time += Time.deltaTime;
            var minutes = (int) time / 60; 
            var seconds = (int) time % 60;      

            timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }
    }
}