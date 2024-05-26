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
        [SerializeField] private Dash dash;
        private float _currentMovementXInput;
        private bool _jumpAfterDash;
        
        private void Start()
        {
            dash.OnDashEnd += OnDashEnd;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed || context.canceled)
            {
                _currentMovementXInput = context.ReadValue<Vector2>().x;

                if (!dash.isDashing)
                {
                    playerMovement.Move(_currentMovementXInput);
                }
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (dash.isDashing)
                {
                    _jumpAfterDash = true;  
                }
                else
                {
                    print("JUMP");
                    playerMovement.Jump();
                }
            }
        }
    
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed && attackHandler.canAttack)
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
                dash.TryDash();
                if (dash.isDashing)
                {
                    playerMovement.isAffectedByGravity = false;
                    playerMovement.Move(0f);
                }
            }
        }

        private void OnDashEnd()
        {
            playerMovement.isAffectedByGravity = true;   
            playerMovement.Move(_currentMovementXInput);
            
            if (_jumpAfterDash)
            {
                playerMovement.Jump();
                _jumpAfterDash = false;
            }
        }
    }
}

