using UnityEngine;

namespace Shared
{
    public class Flipper : MonoBehaviour
    {
        public bool facingRight;
    
        public bool ShouldFlip(float direction)
        {
            return (direction > 0 && !facingRight) || (direction < 0 && facingRight);
        }
        public void Flip()
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1; 
            transform.localScale = localScale;
            Physics2D.SyncTransforms(); // update the position of the player immediately to move WallChecker colliderChecker
        }
    }
}
