using Shared.Collision;
using UnityEngine;

namespace Shared
{
    public class BasicMovement : MonoBehaviour
    {

        public bool isAffectedByGravity = true; 
        [SerializeField] private float speed;
        [SerializeField] private float jumpHeight;
        [SerializeField] private GroundChecker groundChecker;
        [SerializeField] private WallChecker wallChecker;
        [SerializeField] private CeilingChecker ceilingChecker;
        [SerializeField] private float gravity = 5;
        [SerializeField] private Flipper flipper;
    
        private float _currentMovementXInput;
        private float _movementY;
        private bool _jumpAfterDash;

        //public void UpdateMovement()
        void Update()
        {
            if ((ceilingChecker.isTouchingCeiling && _movementY > 0) || 
                (groundChecker.isGrounded && _movementY < 0))
            {
                _movementY = 0f;
            }
            else if (!groundChecker.isGrounded)
            {
                if (isAffectedByGravity)
                {
                    _movementY += Physics2D.gravity.y * gravity * Time.deltaTime;
                }
                else
                {
                    _movementY = 0f;
                }
            }

            var newPositionX = _currentMovementXInput;
            var newPositionY = _movementY;
            
            if (wallChecker.isTouchingWall)
            {
                newPositionX = 0f;
            }
        
            transform.Translate(new Vector3(newPositionX * speed, newPositionY, 0f) * Time.deltaTime);
        }
    
        public void Move(float movementInput)
        {
            _currentMovementXInput = movementInput;
        
            if (flipper.ShouldFlip(_currentMovementXInput))
            {
                flipper.Flip();
            }
        }
    
        public void Jump()
        {
            if (groundChecker.isGrounded)
            {
                _movementY = jumpHeight;
            }    
        }
    }
}
