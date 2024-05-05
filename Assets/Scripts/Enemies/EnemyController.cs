using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float attackDistance;
    
    private IFollow _follow;

    private void Start()
    {
        _follow = GetComponent<IFollow>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackDistance)
        {
            print("ATTACKING!");
        }
        else
        {
            _follow.FollowTarget(playerTransform);
        }
    }
}
