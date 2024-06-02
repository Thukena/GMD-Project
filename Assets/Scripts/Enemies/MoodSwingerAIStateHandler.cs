using GameManagement;
using Player;
using Shared;
using UnityEngine;

namespace Enemies
{
    public class MoodSwingerAIStateHandler : MonoBehaviour
    {
        public MoodSwingerState currentState;
        
        [SerializeField] private Health health;
        [SerializeField] private float attackCooldown; 
        [SerializeField] private float attackRangeX;    
        [SerializeField] private float attackRangeY;
        [SerializeField] private GravityHandler gravityHandler;
        private Transform playerTransform;
        private float lastAttackTime;

        private void Start()
        {
            lastAttackTime = -attackCooldown;
            playerTransform = GameManager.Instance.Player.transform;
        }

        void Update()
        {

            if (health.isDead)
            {
                currentState = MoodSwingerState.Dead;   
            }
            else if (health.isStunned)
            {
                currentState = MoodSwingerState.Stunned;
            }
            else if (ShouldAttack())
            {
                currentState = MoodSwingerState.Attacking;
                lastAttackTime = Time.time; 
            }
            else if (ShouldFlee())
            {
                currentState = MoodSwingerState.Fleeing;
            }
            else
            {
                currentState = MoodSwingerState.Following;
            }
        }

        private bool ShouldAttack()
        {
            if (CanAttack() && gravityHandler.verticalMovement.Equals(0f))
            {
                float distanceToPlayerX = Mathf.Abs(transform.position.x - playerTransform.position.x);
                float distanceToPlayerY = Mathf.Abs(transform.position.y - playerTransform.position.y);

                return distanceToPlayerX <= attackRangeX && distanceToPlayerY <= attackRangeY;
            }
            
            return false;
        }

        private bool CanAttack()
        {
            float timeSinceLastAttack = Time.time - lastAttackTime;
            return timeSinceLastAttack >= attackCooldown;
        }
        
        private bool ShouldFlee()
        {
            return !CanAttack();
        }
        
    }
}