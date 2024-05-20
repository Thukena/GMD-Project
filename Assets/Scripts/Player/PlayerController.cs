using Shared;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private AttackController attackController;
    
        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed || context.canceled)
            {
                playerMovement.Move(context.ReadValue<Vector2>().x);
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                print("JUMP");
                playerMovement.Jump();
            }
        }
    
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (!currentAnimatorStateInfo.IsName("Attack"))
                {
                    print("ATTACK");
                    float attackAnimationDuration = 0.5f; // animation duration is currently 0.5 seconds
                    float newSpeed = attackAnimationDuration / attackController.attackDuration;
                    
                    animator.SetFloat("SpeedMultiplier", newSpeed); //Use SpeedMultiplier to adjust duration of animation to attackDuration
                    animator.SetTrigger("Attack");
                    attackController.Attack();
                }
            }
        }
    
        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                print("DASH");
                playerMovement.Dash();
            }
        }
    }
}

