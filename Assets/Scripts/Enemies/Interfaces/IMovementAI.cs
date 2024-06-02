using UnityEngine;

namespace Enemies.Interfaces
{
    public interface IMovementAI
    {
        void FollowTarget(Transform target);
        void FleeTarget(Transform target);
        void StopMovement();
    }
}
