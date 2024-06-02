using System;
using Enemies.Interfaces;
using Shared;
using Shared.Collision;
using UnityEngine;

namespace Enemies
{
    public class DirectMovementAI : MonoBehaviour, IMovementAI
    {
        [SerializeField] private BasicMovement basicMovement;
        [SerializeField] private WallChecker wallChecker;
        [SerializeField] private BasicJump basicJump;

        private bool _isMoving;

        public void FollowTarget(Transform target)
        {
            _isMoving = true;
            
            if (wallChecker.isTouchingWall)
            {
                basicJump.Jump();
            }

            var transformPositionX = target.position.x;
            
            //Prevent moving when standing directly below or above the player
            if (!(Math.Abs(transformPositionX - transform.position.x) < 0.01))
            {
                basicMovement.Move(transformPositionX > transform.position.x ? 1 : -1);
            }
            else if (_isMoving)
            {
               StopMovement();
            }
        }

        public void FleeTarget(Transform target)
        {
            _isMoving = true;
            
            if (wallChecker.isTouchingWall)
            {
                basicJump.Jump();
            }
            
            basicMovement.Move(target.position.x > transform.position.x ? -1 : 1);
        }

        public void StopMovement()
        {
            if (_isMoving)
            {
                basicMovement.Move(0);
                _isMoving = false;
            }
        }
    }
}
