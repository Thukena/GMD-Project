using Player;
using Shared.Collision;
using UnityEngine;

namespace Enemies
{
    public class DirectFollow : MonoBehaviour, IFollow
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private WallChecker wallChecker;

        public bool IsFollowing { get; set; }

        public void FollowTarget(Transform target)
        {
            IsFollowing = true;
            
            if (wallChecker.isTouchingWall)
            {
                playerMovement.Jump();
            }
            
            playerMovement.Move(target.position.x > transform.position.x ? 1 : -1);
        }

        public void StopFollowTarget()
        {
            playerMovement.Move(0);
            IsFollowing = false;
        }
    }
}
