using TMPro;
using UnityEngine;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        private float _time;
        [SerializeField] private TextMeshProUGUI timerText;

        private void Update()
        {
            _time += Time.deltaTime;
            var minutes = (int) _time / 60; 
            var seconds = (int) _time % 60;      

            timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }
    }
}