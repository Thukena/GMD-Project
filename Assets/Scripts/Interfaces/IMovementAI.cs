using UnityEngine;

namespace Interfaces
{
    public interface IMovementAI
    {
        void FollowTarget(Transform target);
        void FleeTarget(Transform target);
        void StopMovement();
    }
}
