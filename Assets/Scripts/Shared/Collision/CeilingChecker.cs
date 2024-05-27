using UnityEngine;

namespace Shared.Collision
{
    public class CeilingChecker : MonoBehaviour
    {
        public bool isTouchingCeiling;
        [SerializeField] private Transform parentTransform;
        [SerializeField] private CollisionChecker collisionChecker;
        [SerializeField] private GravityHandler gravityHandler;
    
        private Renderer _parentRenderer;

        private void Start()
        {
            _parentRenderer = parentTransform.GetComponent<Renderer>();
        }

        private void Update()
        {
            var collider = collisionChecker.GetCollider();
        
            if (collider != null && gravityHandler.verticalMovement >= 0)        
            {
                isTouchingCeiling = true;
                var position = parentTransform.position;
                parentTransform.position = new Vector3(position.x, collider.bounds.min.y - _parentRenderer.bounds.size.y / 2, position.z); // Set player position to collider bottom - half player height since position is in the middle of the player
                Physics2D.SyncTransforms(); // update the position of the player immediately to move WallChecker colliderChecker
            }
            else
            {
                isTouchingCeiling = false;
            }
        }
    }
}
