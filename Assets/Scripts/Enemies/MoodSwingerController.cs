using Enemies.Interfaces;
using Player;
using Shared;
using UnityEngine;

namespace Enemies
{
    
    public class MoodSwingerController : MonoBehaviour
    {
        [SerializeField] private MoodSwingerAIStateHandler aiStateHandler;
        [SerializeField] private AnimationHandler animationHandler;
        private IMovementAI _movementAI;
        private IAttack _attack;
        private Transform playerTransform;

        private void Start()
        {
            _movementAI = GetComponent<IMovementAI>();
            _attack = GetComponent<IAttack>();
            playerTransform = PlayerManager.Instance.Player.transform;
        }

        private void Update()
        {
            if (aiStateHandler.currentState == MoodSwingerState.Stunned)
            {
                _movementAI.StopMovement();
                _attack.StopAttack();
                animationHandler.ChangeAnimationState("Stunned");
            }
            
            if (_attack.IsAttacking)
            {
                return;
            }
            
            switch (aiStateHandler.currentState)
            {
                case MoodSwingerState.Following:
                    _movementAI.FollowTarget(playerTransform);
                    animationHandler.ChangeAnimationState("Follow");
                    break;
                case MoodSwingerState.Attacking:
                    _attack.Attack();
                    animationHandler.ChangeAnimationState("Attack");
                    _movementAI.FollowTarget(playerTransform); //Make sure the enemy is facing the player
                    _movementAI.StopMovement();
                    break;
                case MoodSwingerState.Fleeing:
                    _movementAI.FleeTarget(playerTransform);
                    animationHandler.ChangeAnimationState("Flee");
                    break;
            }
        }
    }
}