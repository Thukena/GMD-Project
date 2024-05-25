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

        private IFollow _follow;

        private void Start()
        {
            _follow = GetComponent<IFollow>();
        }

        private void Update()
        {
            if (health.isStunned)
            {
                _follow.StopFollowTarget();
                return;
            }
            
            var canAttack = attackHandler.canAttack;
            
            if (Vector2.Distance(transform.position, playerTransform.position) <= attackDistance)
            {
                _follow.StopFollowTarget();
                
                if (canAttack)
                {
                    animationHandler.StartAttackAnimation(attackHandler.attackDuration);
                    attackHandler.Attack();
                }
            }
            else
            {
                if (canAttack)
                {
                    _follow.FollowTarget(playerTransform);
                }
            }
        }
    }
}
