using Interfaces;
using Shared;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }
        [SerializeField] private AnimationHandler animationHandler;
        [SerializeField] private BasicMovement basicMovement;
        private IAttack attack;
        [SerializeField] private Dash dash;
        [SerializeField] private BasicJump basicJump;
        [SerializeField] private GravityHandler gravityHandler;

        private float _currentMovementXInput;
        private bool _jumpAfterDash;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            attack = GetComponent<IAttack>();
            dash.OnDashEnd += OnDashEnd;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed || context.canceled)
            {
                _currentMovementXInput = context.ReadValue<Vector2>().x;

                if (!dash.isDashing)
                {
                    basicMovement.Move(_currentMovementXInput);
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
                    basicJump.Jump();
                }
            }
        }
    
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed && !attack.IsAttacking)
            {
                animationHandler.StartTriggerAnimation("Attack", attack.AttackDuration);
                attack.Attack();
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
                    gravityHandler.isAffectedByGravity = false;
                    basicMovement.Move(0f);
                }
            }
        }

        private void OnDashEnd()
        {
            gravityHandler.isAffectedByGravity = true;   
            basicMovement.Move(_currentMovementXInput);
            
            if (_jumpAfterDash)
            {
                basicJump.Jump();
                _jumpAfterDash = false;
            }
        }
    }
}

