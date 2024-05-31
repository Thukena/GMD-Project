using Enemies.Interfaces;
using Shared;
using UnityEngine;

namespace Enemies
{
    
    public class MoodSwingerController : MonoBehaviour
    {
        [SerializeField] private MoodSwingerAIStateHandler aiStateHandler;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private AnimationHandler animationHandler;
        private MovementAI _movementAI;
        private IAttack _attack;

        private void Start()
        {
            _movementAI = GetComponent<MovementAI>();
            _attack = GetComponent<IAttack>();
        }

        private void Update()
        {
            if (aiStateHandler.currentState == MoodSwingerState.Stunned)
            {
                _movementAI.StopMovement();
                _attack.StopAttack();
                ChangeAnimationState("Stunned");
            }
            
            if (_attack.IsAttacking)
            {
                return;
            }
            
            switch (aiStateHandler.currentState)
            {
                case MoodSwingerState.Following:
                    _movementAI.FollowTarget(playerTransform);
                    ChangeAnimationState("Follow");
                    break;
                case MoodSwingerState.Attacking:
                    _attack.Attack();
                    ChangeAnimationState("Attack");
                    _movementAI.FollowTarget(playerTransform); //Make sure the enemy is facing the player
                    _movementAI.StopMovement();
                    break;
                case MoodSwingerState.Fleeing:
                    _movementAI.FleeTarget(playerTransform);
                    ChangeAnimationState("Flee");
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