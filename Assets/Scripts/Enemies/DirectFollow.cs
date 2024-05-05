using Player;
using UnityEngine;

namespace Enemies
{
    public class DirectFollow : MonoBehaviour, IFollow
    {
        [SerializeField] private PlayerMovement playerMovement;

        public bool IsFollowing { get; set; }

        public void FollowTarget(Transform target)
        {
            IsFollowing = true;
            playerMovement.Move(target.position.x > transform.position.x ? 1 : -1);
        }

        public void StopFollowTarget()
        {
            playerMovement.Move(0);
            IsFollowing = false;
        }
    }
}
