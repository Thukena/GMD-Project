using UnityEngine;

namespace Enemies
{
    
    public class MoodSwingerController : MonoBehaviour
    {
        [SerializeField] private MoodSwingerAIStateHandler aiStateHandler;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private MoodSwingerAnimationHandler moodSwingerAnimationHandler;
        private IFollow _follow;
        private IAttack _attack;

        private void Start()
        {
            _follow = GetComponent<IFollow>();
            _attack = GetComponent<IAttack>();
        }

        private void Update()
        {
            if (_attack.IsAttacking)
            {
                return;
            }
            
            switch (aiStateHandler.currentState)
            {
                case MoodSwingerState.Following:
                    _follow.FollowTarget(playerTransform);
                    moodSwingerAnimationHandler.StartFollowAnimation();
                    break;
                case MoodSwingerState.Attacking:
                    _attack.Attack();
                    moodSwingerAnimationHandler.StartAttackAnimation();
                    _follow.StopFollowTarget();
                    break;
                case MoodSwingerState.Fleeing:
                    break;
                case MoodSwingerState.Stunned:
                    _follow.StopFollowTarget();
                    break;
            }
        }
    }
}