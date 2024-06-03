using Shared;
using UnityEngine;

namespace Player
{
    public class LevelUpManager : MonoBehaviour
    {
        [SerializeField] private AttackHandler playerAttackHandler;
        [SerializeField] private Health playerHealth;
        [SerializeField] private float increaseAttackDamagePerLevel;
        [SerializeField] private float increaseMaxHealthPerLevel;
        [SerializeField] private float increaseHealthRegenPerLevel;
        [SerializeField] private float increaseAttackSpeedPerLevel;

        private int _playerLevel;
        private int _currentXp;
        private int _xpToNextLevel;
        
        private void Start()
        {
            _playerLevel = 1;
            _xpToNextLevel = 100;
        }
        
        public void AddXp(int xp)
        {
            _currentXp += xp;
            if (_currentXp >= _xpToNextLevel)
            {
                LevelUp();
            }
        }
        
        private void LevelUp()
        {
            _playerLevel++;
            _currentXp %= _xpToNextLevel;
            _xpToNextLevel *= 2;
            print("LEVEL UP! Current level: " + _playerLevel);

            playerAttackHandler.damage *= increaseAttackDamagePerLevel / 100 + 1;
            playerAttackHandler.attackCooldown /= increaseAttackSpeedPerLevel / 100 + 1;
            playerAttackHandler.attackDuration /= increaseAttackDamagePerLevel / 100 + 1;
            playerHealth.SetMaxHealth((int)(playerHealth.maxHealth * (increaseMaxHealthPerLevel / 100 + 1)));
            playerHealth.healthRegenAmount *= increaseHealthRegenPerLevel / 100 + 1;

            print("new attack damage: " + playerAttackHandler.damage);
            print("new attack speed: " + playerAttackHandler.attackCooldown);
            print("new attack duration: " + playerAttackHandler.attackDuration);
            print("new max health: " + playerHealth.maxHealth);
            print("new health regen: " + playerHealth.healthRegenAmount);
        }
    }
}