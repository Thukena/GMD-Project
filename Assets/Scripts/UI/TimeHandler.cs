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
        private void Update()
        {
            time += Time.deltaTime;
            var newMinutes = (int) time / 60;
            var seconds = (int) time % 60;      
            if (newMinutes > _minutes)
            {
                OnMinutePassed?.Invoke();
            }

            _minutes = newMinutes;
            
            timerText.text = $"Time: {_minutes:00}:{seconds:00}";
        }
    }
}