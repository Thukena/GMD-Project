using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Shared
{
    public class AnimationHandler : MonoBehaviour
    {
        public string currentAnimationState;
        [SerializeField] private Animator animator;
        [SerializeField] private List<string> animationStates;

        public void ChangeAnimationState(string animationName, float? animationDuration = null)
        {
            if (animationDuration != null)
            {
                SetAnimationDuration(animationName, animationDuration.Value);
            }

            animationStates.ForEach(x => animator.SetBool(x, x.Equals(animationName))); //Set animationName to true and all others to false
            currentAnimationState = animationName;
        }

        public void StartTriggerAnimation(string animationName, float? animationDuration = null)
        {
            if (animationDuration != null)
            {
                SetAnimationDuration(animationName, animationDuration.Value);
            } 
            
            animator.SetTrigger(animationName);
            currentAnimationState = animationName;
        }
        
        private void SetAnimationDuration(string animationName, float duration)
        {
            float newSpeed = GetAnimationDuration(animationName) / duration;
            animator.SetFloat("SpeedMultiplier", newSpeed); //Use SpeedMultiplier to adjust duration of animation to animationDuration
        }
        
        private float GetAnimationDuration(string animationName)
        {
            return animator.runtimeAnimatorController.animationClips.FirstOrDefault(x => x.name == animationName)?.length ?? 0f;
        }
    }
}
