using UnityEngine;

public interface IFollow
{
    bool IsFollowing { get; set; }
    void FollowTarget(Transform target);
    void StopFollowTarget();
}
