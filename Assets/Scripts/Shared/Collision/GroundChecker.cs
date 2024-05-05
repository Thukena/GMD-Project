using UnityEngine;

namespace Shared.Collision
{
    public class GroundChecker : MonoBehaviour
    {
        
        public bool isGrounded;
        [SerializeField] private Transform parentTransform;
        [SerializeField] private CollisionChecker collisionChecker;
        private Renderer _parentRenderer;

        // Start is called before the first frame update
        private void Start()
        {
            _parentRenderer = parentTransform.GetComponent<Renderer>();
        }

        private void FixedUpdate()
        {
            var collider = collisionChecker.GetCollider();
        
            if (collider != null)
            {
                isGrounded = true;

                var position = parentTransform.position;
                parentTransform.position = new Vector3(position.x, collider.bounds.max.y + _parentRenderer.bounds.size.y / 2, position.z);; // Set player position to collider top + half player height since position is in the middle of the player
                Physics2D.SyncTransforms(); // update the position of the player immediately to move WallChecker colliderChecker
            }
            else
            {
                isGrounded = false;
            }
        }

    }
}
