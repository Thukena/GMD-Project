using UnityEngine;

namespace Shared.Collision
{
    public class WallChecker : MonoBehaviour
    {
        public bool isTouchingWall;
        [SerializeField] private Transform parentTransform;
        [SerializeField] private CollisionChecker collisionChecker;
        [SerializeField] private Flipper flipper;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private LayerMask groundLayer;
    
        private Vector2 collisionCheckerSize;
        private float _spriteSizeX;
        private void Start()
        {
            _spriteSizeX = sprite.bounds.size.x;
            collisionCheckerSize = collisionChecker.boxCollider.size;
        }
            
        private void Update()
        {
            var collider = collisionChecker.GetCollider();
            isTouchingWall = collider != null;
        
            if (isTouchingWall)
            {
                var position = parentTransform.position;
            
                var direction = (flipper.facingRight ? 1 : -1) * Vector2.right;
                
                var colliderSide = Physics2D.BoxCast(position, collisionCheckerSize, 0f, direction, _spriteSizeX / 2 + collisionCheckerSize.x, groundLayer);
                if (colliderSide.collider != null)
                {
                    isTouchingWall = true;
                    var parentPositionX = colliderSide.point.x +  -direction.x * (sprite.bounds.size.x / 2); // Set player positionX to collider edge +/- half player width since position is in the middle of the player
                    parentTransform.position = new Vector2(parentPositionX, position.y); 
                 return;   
                }
            }

            isTouchingWall = false;
        }
    }
}