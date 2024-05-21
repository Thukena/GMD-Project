using Shared;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private AnimationHandler animationHandler;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private AttackHandler attackHandler;
    
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
            if (context.performed && !attackHandler.isAttacking)
            {
                animationHandler.StartAttackAnimation(attackHandler.attackDuration);
                attackHandler.Attack();
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

