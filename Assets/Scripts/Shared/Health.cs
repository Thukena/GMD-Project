
using UnityEngine;

namespace Shared
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        private int currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
        }
        
        public void TakeDamage(int damage)
        {
            print("DAMAGE TAKEN: " + damage);
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                print("DEAD");
                Destroy(gameObject);
            }
        }
    }
}