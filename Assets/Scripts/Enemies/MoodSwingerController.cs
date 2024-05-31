using Shared;
using UnityEngine;

namespace Enemies
{
    
    public class MoodSwingerController : MonoBehaviour
    {
        [SerializeField] private MoodSwingerAIStateHandler aiStateHandler;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private AnimationHandler animationHandler;
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
                    ChangeAnimationState("Follow");
                    break;
                case MoodSwingerState.Attacking:
                    _attack.Attack();
                    ChangeAnimationState("Attack");
                    _follow.StopFollowTarget();
                    break;
                case MoodSwingerState.Fleeing:
                    break;
                case MoodSwingerState.Stunned:
                    _follow.StopFollowTarget();
                    break;
            }
        }
        
        private void ChangeAnimationState(string animationName)
        {
            if (!animationHandler.currentAnimationState.Equals(animationName))
            {
                animationHandler.ChangeAnimationState(animationName);
            }
        }
    }
}