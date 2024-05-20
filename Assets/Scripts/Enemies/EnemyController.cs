using UnityEngine;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {
    
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float attackDistance;
        [SerializeField] private Animator animator;

        private IFollow _follow;

        private void Start()
        {
            _follow = GetComponent<IFollow>();
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, playerTransform.position) <= attackDistance)
            {
                if (_follow.IsFollowing)
                {
                    _follow.StopFollowTarget();
                }

                AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (!currentAnimatorStateInfo.IsName("Attack"))
                {
                    animator.SetTrigger("Attack");
                }          
            }
            else
            { 
                _follow.FollowTarget(playerTransform);
            }
        }
    }
}
