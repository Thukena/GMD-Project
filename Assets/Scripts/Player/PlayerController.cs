using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerMovement playerMovement;
    
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
                print("ATTACK");
                animator.SetTrigger("Attack"); 
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

