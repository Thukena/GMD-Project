using JetBrains.Annotations;
using Shared;
using Shared.Collision;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {

        [SerializeField] private float speed;
        [SerializeField] private float jumpHeight;
        [SerializeField] private GroundChecker groundChecker;
        [SerializeField] private WallChecker wallChecker;
        [SerializeField] private CeilingChecker ceilingChecker;
        [SerializeField] private float gravity = 5;
        [SerializeField] private Flipper flipper;
        [SerializeField] [CanBeNull] private Dash dash;
    
        private float _currentMovementXInput;
        private float _movementY;
        private bool _jumpAfterDash;
        private bool _canDash = false;
        private void Start()
        {
            _canDash = dash != null;

            if (_canDash)
            {
                dash!.OnDashEnd += OnDashEnd;
            }
        }

        void Update()
        {
            if (ceilingChecker.isTouchingCeiling)
            {
                if (_movementY > 0)
                {
                    _movementY = 0f;
                }
            }
        
            if (!groundChecker.isGrounded)
            {
                if (_canDash && dash!.isDashing)
                {
                    _movementY = 0f;
                }
                else
                {
                    _movementY += Physics2D.gravity.y * gravity * Time.deltaTime;
                }
            }
            else
            {
                if (_movementY < 0)
                {
                    _movementY = 0f;
                }
            }

            var newPositionX = _currentMovementXInput;
            var newPositionY = _movementY;
        
            if (_canDash && dash!.isDashing)
            {
                var dashSpeed = dash.dashSpeed;
                newPositionX = flipper.facingRight ? dashSpeed : -dashSpeed;
                newPositionY = 0f;
            }

            if (wallChecker.isTouchingWall)
            {
                newPositionX = 0f;
            }
        
            transform.Translate(new Vector3(newPositionX * speed, newPositionY, 0f) * Time.deltaTime);
        }
    
        public void Move(float movementInput)
        {
            _currentMovementXInput = movementInput;
        
            if ((!_canDash || !dash!.isDashing) && flipper.ShouldFlip(_currentMovementXInput))
            {
                flipper.Flip();
            }
        }
    
        public void Jump()
        {
            if (groundChecker.isGrounded)
            {
                if (_canDash && dash!.isDashing)
                {
                    _jumpAfterDash = true;
                    return;
                }

                _movementY = jumpHeight;
            }    
        }

        public void Dash()
        {
            dash.TryDash();
        }
    
        private void OnDashEnd()
        {
            print("Dash ended!");
            if (flipper.ShouldFlip(_currentMovementXInput))
            {
                flipper.Flip();
            }

            if (_jumpAfterDash)
            {
                Jump();
                _jumpAfterDash = false;
            }
        }
    }
}
