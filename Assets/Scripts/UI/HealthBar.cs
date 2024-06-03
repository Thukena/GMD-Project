using JetBrains.Annotations;
using Shared;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        public Health health;
        [SerializeField] private Image greenHealthBar;
        [SerializeField] private TextMeshProUGUI healthText;

        private bool _hasHealthText;
        private void Start()
        {
            health.OnUpdateHealth += UpdateHealthBar;
            if (healthText != null)
            {
                _hasHealthText = true;
                healthText.text = health.currentHealth.ToString();
            }
        }

        private void UpdateHealthBar()
        {
            greenHealthBar.fillAmount = (float)health.currentHealth / health.maxHealth;

            if (_hasHealthText)
            {
                healthText.text = health.currentHealth.ToString();
            }
        }
    }
}