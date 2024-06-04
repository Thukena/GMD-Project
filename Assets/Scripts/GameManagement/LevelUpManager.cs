using System;
using Shared;
using UnityEngine;

namespace GameManagement
{
    public class LevelUpManager : MonoBehaviour
    {
        public int currentXp;
        public int xpToNextLevel;
        public int level;
        public event Action OnXpGain;

        private IAttack attack;
        [SerializeField] private Health health;
        [SerializeField] private float increaseAttackDamagePerLevel;
        [SerializeField] private float increaseMaxHealthPerLevel;
        [SerializeField] private float increaseHealthRegenPerLevel;
        [SerializeField] private float increaseAttackCooldownPerLevel;
        [SerializeField] private float increaseAttackDurationPerLevel;
        
        private void Start()
        {
            attack = GetComponent<IAttack>();
            level = 1;
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
            level++;
            currentXp %= xpToNextLevel;
            xpToNextLevel *= 2;
            attack.Damage *= increaseAttackDamagePerLevel / 100 + 1;
            attack.AttackCooldown /= increaseAttackCooldownPerLevel / 100 + 1;
            attack.AttackDuration /= increaseAttackDurationPerLevel / 100 + 1;
            health.SetMaxHealth((int)(health.maxHealth * (increaseMaxHealthPerLevel / 100 + 1)));
            health.healthRegenAmount *= increaseHealthRegenPerLevel / 100 + 1;
        }
    }
}