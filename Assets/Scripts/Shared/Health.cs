using System;
using System.Collections;
using UnityEngine;

namespace Shared
{
    public class Health : MonoBehaviour
    {
        public event Action OnDeath;
        public event Action OnUpdateHealth;
        public float knockBackResistance;
        public float stunResistence;
        public bool isStunned;
        public bool isDead = false;
        public int maxHealth;
        public double healthRegenAmount;
        public int currentHealth;
        [SerializeField] private float healthRegenRate;
        [SerializeField] private KnockBackHandler knockBackHandler;
        
        private bool IsFullHealth => currentHealth == maxHealth;

        private double _excessRegenHealth;
        private void Start()
        {
            currentHealth = maxHealth;
            if (healthRegenAmount > 0)
            {
                StartCoroutine(RegenHealth());
            }
        }

        public void TakeDamage(int damage)
        {
            UpdateCurrentHealth(currentHealth - damage);

            if (currentHealth <= 0)
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
                UpdateCurrentHealth(currentHealth + healthDifference);    
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
                    
                    UpdateCurrentHealth(currentHealth + (int)regenAmount);
                }
            
                yield return new WaitForSeconds(healthRegenRate);
            }
        }

        private void UpdateCurrentHealth(int newCurrentHealthValue)
        {
            if (currentHealth > maxHealth)
            {
                newCurrentHealthValue = maxHealth;
            }

            currentHealth = newCurrentHealthValue;
            OnUpdateHealth?.Invoke();
        }
    }
}