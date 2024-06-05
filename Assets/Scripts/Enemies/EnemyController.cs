using Interfaces;
using Player;
using Shared;
using UnityEngine;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {
    
        [SerializeField] private float attackDistance;
        [SerializeField] private AnimationHandler animationHandler;
        [SerializeField] private Health health;
        private IAttack attack;

        private IMovementAI _movementAI;
        private Transform playerTransform;

        private void Start()
        {
            attack = GetComponent<IAttack>();
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
            
            if (Vector2.Distance(transform.position, playerTransform.position) <= attackDistance)
            {
                _movementAI.StopMovement();
                
                if (!attack.IsAttacking)
                {
                    animationHandler.StartTriggerAnimation("Attack", attack.AttackDuration);
                    attack.Attack();
                }
            }
            else
            {
                _movementAI.FollowTarget(playerTransform);
            }
        }
    }
}
