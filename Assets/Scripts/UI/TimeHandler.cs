using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TimeHandler : MonoBehaviour
    {
        public float time;
        [SerializeField] private TextMeshProUGUI timerText;
        public event Action OnMinutePassed;
        private int _minutes;
        private int _seconds;
        private void Update()
        {
            time += Time.deltaTime;
            var newMinutes = (int) time / 60;
            var newseconds = (int) time % 60;      
            if (newMinutes > _minutes)
            {
                OnMinutePassed?.Invoke();
            }

            _minutes = newMinutes;
            _seconds = newseconds;
            timerText.text = $"Time: {ToString()}";
        }
        
        public override string ToString()
        {
            return $"{_minutes:00}:{_seconds:00}";
        }
    }
}