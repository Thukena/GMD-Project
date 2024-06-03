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
        public int maxHealth;
        public double healthRegenAmount;
        [SerializeField] private float healthRegenRate;
        [SerializeField] private Image greenHealthBar;
        [SerializeField] private KnockBackHandler knockBackHandler;
        
        private int _currentHealth;
        private bool IsFullHealth => _currentHealth == maxHealth;

        private double _excessRegenHealth;
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

        public void SetMaxHealth(int newMaxHealth)
        {
            var healthDifference = newMaxHealth - maxHealth;
            
            maxHealth = newMaxHealth;

            if (healthDifference > 0) //Heal if maxHealth increased
            {
                UpdateCurrentHealth(_currentHealth + healthDifference);    
            }
        }

        private IEnumerator RegenHealth()
        {
            while (!isDead)
            {
                if (!IsFullHealth)
                {
                    var regenAmount = Math.Floor(healthRegenAmount);
                    
                    _excessRegenHealth += healthRegenAmount - regenAmount;

                    if (_excessRegenHealth >= 1)
                    {
                        _excessRegenHealth -= regenAmount;
                        regenAmount += 1;
                    }
                    
                    UpdateCurrentHealth(_currentHealth + (int)regenAmount);
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