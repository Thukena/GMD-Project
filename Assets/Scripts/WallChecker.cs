
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    public Transform parentTransform;
    public CollisionChecker collisionChecker;
    public Flipper Flipper;
    
    private Renderer parentRenderer;
    
    // Start is called before the first frame update
    private void Start()
    {
        parentRenderer = parentTransform.GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if (collisionChecker.isColliding)
        {
            var collider = collisionChecker.collider;
            var position = parentTransform.position;
            
            var colliderEdgePositionX = Flipper.facingRight ? collider.bounds.min.x : collider.bounds.max.x;
            var parentPositionX = colliderEdgePositionX + (Flipper.facingRight ? -1 : 1) * parentRenderer.bounds.size.x / 2;
            parentTransform.position = new Vector3(parentPositionX, position.y, position.z);; // Set player position to collider top + half player height since position is in the middle of the player
        }
    }
}