using UnityEngine;

namespace Shared.Collision
{
    public class GroundChecker : MonoBehaviour
    {
        
        public bool isGrounded;
        [SerializeField] private Transform parentTransform;
        [SerializeField] private CollisionChecker collisionChecker;
        [SerializeField] private GravityHandler gravityHandler;
        [SerializeField] private Renderer spriteRenderer;
        [SerializeField] private LayerMask groundLayer;

        private Vector2 collisionCheckerSize;
        private float _spriteSizeY;
        private void Start()
        {
            _spriteSizeY = spriteRenderer.bounds.size.y;
            collisionCheckerSize = collisionChecker.boxCollider.size;
        }

        private void Update()
        {
            var collider = collisionChecker.GetCollider();

            if (collider != null && gravityHandler.verticalMovement <= 0)
            {
                var position = parentTransform.position;
                var colliderTop = Physics2D.BoxCast(position, collisionCheckerSize, 0f, Vector2.down, _spriteSizeY / 2 + collisionCheckerSize.y, groundLayer);

                if (colliderTop.collider != null)
                {
                    isGrounded = true;
                    parentTransform.position = new Vector2(position.x, colliderTop.point.y + _spriteSizeY / 2); // Set player position to collider top + half player height since position is in the middle of the player
                    Physics2D.SyncTransforms(); // update the position of the player immediately to move WallChecker colliderChecker
                    return;
                }
            }
            isGrounded = false;
        }
    }
}