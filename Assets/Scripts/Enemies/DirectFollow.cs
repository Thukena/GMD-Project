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

        public bool IsFollowing { get; set; }

        public void FollowTarget(Transform target)
        {
            IsFollowing = true;
            
            if (wallChecker.isTouchingWall)
            {
                basicJump.Jump();
            }
            
            if (!(Math.Abs(target.position.x - transform.position.x) < 0.01))
            {
                basicMovement.Move(target.position.x > transform.position.x ? 1 : -1);
            }
            else if (IsFollowing)
            {
               StopFollowTarget();
            }
        }

        public void StopFollowTarget()
        {
            if (IsFollowing)
            {
                basicMovement.Move(0);
                IsFollowing = false;
            }
        }
    }
}
