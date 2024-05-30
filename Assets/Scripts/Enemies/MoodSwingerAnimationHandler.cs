using UnityEngine;

namespace Enemies
{
    public class MoodSwingerAnimationHandler : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        public void StartFollowAnimation()
        {
            animator.SetBool("Follow", true);
            animator.SetBool("Attack", false);
        }  
        
        public void StartAttackAnimation()
        {
            animator.SetBool("Follow", false);
            animator.SetBool("Attack", true);
        }  
    }
}