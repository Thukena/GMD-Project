using UnityEngine;

public interface IFollow
{
    void FollowTarget(Transform target);
    void StopFollowTarget();
}
