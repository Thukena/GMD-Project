using System;
using Shared;
using Shared.Collision;
using UnityEngine;

namespace Enemies
{
    public class DirectFollow : MonoBehaviour, IFollow
    {
        [SerializeField] private BasicMovement basicMovement;
        [SerializeField] private WallChecker wallChecker;
        [SerializeField] private BasicJump basicJump;

        private bool _isFollowing;

        public void FollowTarget(Transform target)
        {
            _isFollowing = true;
            
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
            else if (_isFollowing)
            {
               StopFollowTarget();
            }
        }

        public void StopFollowTarget()
        {
            if (_isFollowing)
            {
                basicMovement.Move(0);
                _isFollowing = false;
            }
        }
    }
}
