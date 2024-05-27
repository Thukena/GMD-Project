using Shared.Collision;
using UnityEngine;

namespace Shared
{
    public class BasicMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private WallChecker wallChecker;
        [SerializeField] private Flipper flipper;
    
        private float _currentMovementXInput;

        void Update()
        {
            var newPositionX = _currentMovementXInput;
            
            if (wallChecker.isTouchingWall)
            {
                newPositionX = 0f;
            }
        
            transform.Translate(Vector2.right * (newPositionX * speed * Time.deltaTime));
        }
    
        public void Move(float movementInput)
        {
            _currentMovementXInput = movementInput;
        
            if (flipper.ShouldFlip(_currentMovementXInput))
            {
                flipper.Flip();
            }
        }
    }
}
