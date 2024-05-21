using UnityEngine;


namespace Shared
{
    public class AnimationHandler : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float attackAnimationDuration;
        
        public void StartAttackAnimation(float attackDuration)
        {
            float newSpeed = attackAnimationDuration / attackDuration;
            animator.SetFloat("SpeedMultiplier", newSpeed); //Use SpeedMultiplier to adjust duration of animation to attackDuration
            animator.SetTrigger("Attack");
        }
    }
}
