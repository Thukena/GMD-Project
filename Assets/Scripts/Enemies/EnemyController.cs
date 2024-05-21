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

        private IFollow _follow;

        private void Start()
        {
            _follow = GetComponent<IFollow>();
        }

        private void Update()
        {
            if (Vector2.Distance(transform.position, playerTransform.position) <= attackDistance)
            {
                if (_follow.IsFollowing)
                {
                    _follow.StopFollowTarget();
                }

                if (!attackHandler.isAttacking)
                {
                    animationHandler.StartAttackAnimation(attackHandler.attackDuration);
                    attackHandler.Attack();
                }
            }
            else
            { 
                _follow.FollowTarget(playerTransform);
            }
        }
    }
}
