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

        private float _spriteSizeY;
        private void Start()
        {
            _spriteSizeY = spriteRenderer.bounds.size.y;
        }

        private void Update()
        {
            var collider = collisionChecker.GetCollider();

            if (collider != null && gravityHandler.verticalMovement <= 0)
            {
                isGrounded = true;

                var position = parentTransform.position;
                parentTransform.position = new Vector2(position.x, collider.bounds.max.y + _spriteSizeY / 2); // Set player position to collider top + half player height since position is in the middle of the player
                Physics2D.SyncTransforms(); // update the position of the player immediately to move WallChecker colliderChecker
            }
            else
            {
                isGrounded = false;
            }
        }
    }
}