using Enemies.Interfaces;
using Shared;
using UnityEngine;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {
    
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float attackDistance;
        [SerializeField] private AnimationHandler animationHandler;
        [SerializeField] private AttackHandler attackHandler;
        [SerializeField] private Health health;

        private MovementAI _movementAI;

        private void Start()
        {
            _movementAI = GetComponent<MovementAI>();
        }

        private void Update()
        {
            if (health.isStunned)
            {
                _movementAI.StopMovement();
                return;
            }
            
            var canAttack = attackHandler.canAttack;
            
            if (Vector2.Distance(transform.position, playerTransform.position) <= attackDistance)
            {
                _movementAI.StopMovement();
                
                if (canAttack)
                {
                    animationHandler.StartTriggerAnimation("Attack", attackHandler.attackDuration);
                    attackHandler.Attack();
                }
            }
            else
            {
                _movementAI.FollowTarget(playerTransform);
            }
        }
    }
}
