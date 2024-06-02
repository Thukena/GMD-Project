using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Shared
{
    public class Health : MonoBehaviour
    {
        public event Action OnDeath;
        public float knockBackResistance;
        public float stunResistence;
        public bool isStunned;
        public bool isDead = false;
        [SerializeField] private int maxHealth;
        [SerializeField] private float healthRegenRate;
        [SerializeField] private int healthRegenAmount;
        [SerializeField] private Image greenHealthBar;
        [SerializeField] private KnockBackHandler knockBackHandler;
        
        private int _currentHealth;
        private bool IsFullHealth => _currentHealth == maxHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
            greenHealthBar.fillAmount = 1f;
            if (healthRegenAmount > 0)
            {
                StartCoroutine(RegenHealth());
            }
        }

        public void TakeDamage(int damage)
        {
            UpdateCurrentHealth(_currentHealth - damage);

            if (_currentHealth <= 0)
            {
                isDead = true;
                OnDeath?.Invoke();
            }
        }
        
        public void GetKnockedBack(float knockBackSpeed, float knockBackDuration, float stunDuration, float attackerPosition)
        {
            knockBackHandler.KnockBack(knockBackSpeed, knockBackDuration, stunDuration, attackerPosition);
        }

        private IEnumerator RegenHealth()
        {
            while (!isDead)
            {
                if (!IsFullHealth)
                {
                    UpdateCurrentHealth(_currentHealth + healthRegenAmount);
                }
            
                yield return new WaitForSeconds(healthRegenRate);
            }
        }

        private void UpdateCurrentHealth(int newCurrentHealthValue)
        {
            if (_currentHealth > maxHealth)
            {
                newCurrentHealthValue = maxHealth;
            }

            _currentHealth = newCurrentHealthValue;
            
            greenHealthBar.fillAmount = (float)_currentHealth / maxHealth;
        }
    }
}