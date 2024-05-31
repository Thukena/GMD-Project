using UnityEngine;

namespace Enemies.Interfaces
{
    public interface MovementAI
    {
        void FollowTarget(Transform target);
        void FleeTarget(Transform target);
        void StopMovement();
    }
}
