using UnityEngine;

namespace Shared.Collision
{
    public class CeilingChecker : MonoBehaviour
    {
        public bool isTouchingCeiling;
        [SerializeField] private Transform parentTransform;
        [SerializeField] private CollisionChecker collisionChecker;
        [SerializeField] private GravityHandler gravityHandler;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private LayerMask groundLayer;

        private float _spriteSizeY;
        private Vector2 collisionCheckerSize;

        private void Start()
        {
            _spriteSizeY = sprite.bounds.size.y;
            collisionCheckerSize = collisionChecker.boxCollider.size;
        }
        
        private void Update()
        {
            var collider = collisionChecker.GetCollider();
        
            if (collider != null && gravityHandler.verticalMovement >= 0)        
            {
                var position = parentTransform.position;
                var colliderBottom = Physics2D.BoxCast(position, collisionCheckerSize, 0f, Vector2.up, _spriteSizeY / 2 + collisionCheckerSize.y, groundLayer);

                if (colliderBottom.collider != null)
                {
                    isTouchingCeiling = true;
                    parentTransform.position = new Vector2(position.x, colliderBottom.point.y - sprite.bounds.size.y / 2); // Set player position to collider bottom - half player height since position is in the middle of the player
                    Physics2D.SyncTransforms(); // update the position of the player immediately to move WallChecker colliderChecker
                    return;
                }
            }
            isTouchingCeiling = false;
        }
    }
}
