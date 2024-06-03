using Enemies.Interfaces;
using Player;
using Shared;
using UnityEngine;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {
    
        [SerializeField] private float attackDistance;
        [SerializeField] private AnimationHandler animationHandler;
        [SerializeField] private AttackHandler attackHandler;
        [SerializeField] private Health health;

        private IMovementAI _movementAI;
        private Transform playerTransform;

        private void Start()
        {
            _movementAI = GetComponent<IMovementAI>();
            playerTransform = PlayerController.Instance.transform;
            health.OnDeath += () => Destroy(gameObject);
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
