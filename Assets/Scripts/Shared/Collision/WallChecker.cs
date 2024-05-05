using UnityEngine;

namespace Shared.Collision
{
    public class WallChecker : MonoBehaviour
    {
        [SerializeField] private Transform parentTransform;
        [SerializeField] private CollisionChecker collisionChecker;
        [SerializeField] private Flipper flipper;
        public bool isTouchingWall;
    
        private Renderer _parentRenderer;
    
        // Start is called before the first frame update
        private void Start()
        {
            _parentRenderer = parentTransform.GetComponent<Renderer>();
        }

        private void FixedUpdate()
        {
            var collider = collisionChecker.GetCollider();
            isTouchingWall = collider != null;
        
            if (isTouchingWall)
            {
                isTouchingWall = true;
                var position = parentTransform.position;
            
                var colliderEdgePositionX = flipper.facingRight ? collider.bounds.min.x : collider.bounds.max.x;
                var parentPositionX = colliderEdgePositionX + (flipper.facingRight ? -1 : 1) * _parentRenderer.bounds.size.x / 2; // Set player positionX to collider edge +/- half player width since position is in the middle of the player
                parentTransform.position = new Vector3(parentPositionX, position.y, position.z); 
            }
        }
    }
}