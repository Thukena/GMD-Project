
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    public Transform parentTransform;
    public CollisionChecker collisionChecker;
    public Flipper Flipper;
    public bool isTouchingWall;
    
    private Renderer parentRenderer;
    
    // Start is called before the first frame update
    private void Start()
    {
        parentRenderer = parentTransform.GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        var collider = collisionChecker.GetCollider();
        isTouchingWall = collider != null;
        
        if (isTouchingWall)
        {
            isTouchingWall = true;
            var position = parentTransform.position;
            
            var colliderEdgePositionX = Flipper.facingRight ? collider.bounds.min.x : collider.bounds.max.x;
            var parentPositionX = colliderEdgePositionX + (Flipper.facingRight ? -1 : 1) * parentRenderer.bounds.size.x / 2; // Set player positionX to collider edge +/- half player width since position is in the middle of the player
            parentTransform.position = new Vector3(parentPositionX, position.y, position.z); 
        }
    }
}