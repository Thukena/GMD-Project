using UnityEngine;
using UnityEngine.UI;

namespace Shared
{
    public class Health : MonoBehaviour
    {
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
                print("DEAD");
                Destroy(gameObject);
            }
        }
    }
}