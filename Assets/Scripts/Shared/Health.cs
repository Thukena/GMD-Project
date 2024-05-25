using UnityEngine;
using UnityEngine.UI;

namespace Shared
{
    public class Health : MonoBehaviour
    {
        public float knockBackResistance;
        public float stunResistence;
        public bool isStunned;
        public bool isDead = false;
        [SerializeField] private int maxHealth;
        [SerializeField] private Image greenHealthBar;
        private int currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
            greenHealthBar.fillAmount = 1f;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            greenHealthBar.fillAmount = (float)currentHealth / maxHealth;
            
            if (currentHealth <= 0)
            {
                isDead = true;
                print("DEAD");
                Destroy(gameObject);
            }
        }
    }
}