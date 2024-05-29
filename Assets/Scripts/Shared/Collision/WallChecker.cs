using UnityEngine;

namespace Shared.Collision
{
    public class WallChecker : MonoBehaviour
    {
        [SerializeField] private Transform parentTransform;
        [SerializeField] private CollisionChecker collisionChecker;
        [SerializeField] private Flipper flipper;
        [SerializeField] private SpriteRenderer sprite;
        public bool isTouchingWall;
    
        private void Update()
        {
            var collider = collisionChecker.GetCollider();
            isTouchingWall = collider != null;
        
            if (isTouchingWall)
            {
                isTouchingWall = true;
                var position = parentTransform.position;
            
                var colliderEdgePositionX = flipper.facingRight ? collider.bounds.min.x : collider.bounds.max.x;
                var parentPositionX = colliderEdgePositionX + (flipper.facingRight ? -1 : 1) * sprite.bounds.size.x / 2; // Set player positionX to collider edge +/- half player width since position is in the middle of the player
                parentTransform.position = new Vector2(parentPositionX, position.y); 
            }
        }
    }
}