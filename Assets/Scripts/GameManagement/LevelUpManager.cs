using System;
using Shared;
using UnityEngine;

namespace GameManagement
{
    public class LevelUpManager : MonoBehaviour
    {
        public int currentXp;
        public int xpToNextLevel;
        public int playerLevel;
        public event Action OnXpGain;

        [SerializeField] private AttackHandler playerAttackHandler;
        [SerializeField] private Health playerHealth;
        [SerializeField] private float increaseAttackDamagePerLevel;
        [SerializeField] private float increaseMaxHealthPerLevel;
        [SerializeField] private float increaseHealthRegenPerLevel;
        [SerializeField] private float increaseAttackSpeedPerLevel;
        
        private void Start()
        {
            playerLevel = 1;
            xpToNextLevel = 100;
        }
        
        public void AddXp(int xp)
        {
            currentXp += xp;
            if (currentXp >= xpToNextLevel)
            {
                LevelUp();
            }
            OnXpGain?.Invoke();
        }
        
        private void LevelUp()
        {
            playerLevel++;
            currentXp %= xpToNextLevel;
            xpToNextLevel *= 2;
            playerAttackHandler.damage *= increaseAttackDamagePerLevel / 100 + 1;
            playerAttackHandler.attackCooldown /= increaseAttackSpeedPerLevel / 100 + 1;
            playerAttackHandler.attackDuration /= increaseAttackDamagePerLevel / 100 + 1;
            playerHealth.SetMaxHealth((int)(playerHealth.maxHealth * (increaseMaxHealthPerLevel / 100 + 1)));
            playerHealth.healthRegenAmount *= increaseHealthRegenPerLevel / 100 + 1;
        }
    }
}