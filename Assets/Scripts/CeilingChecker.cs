using UnityEngine;

public class CeilingChecker : MonoBehaviour
{
    public bool IsTouchingCeiling;
    public Transform parentTransform;
    public CollisionChecker collisionChecker;
    
    private Renderer parentRenderer;

    private void Start()
    {
        parentRenderer = parentTransform.GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        var collider = collisionChecker.GetCollider();
        
        if (collider != null)        {
            IsTouchingCeiling = true;
            var position = parentTransform.position;
            parentTransform.position = new Vector3(position.x, collider.bounds.min.y - parentRenderer.bounds.size.y / 2, position.z); // Set player position to collider bottom - half player height since position is in the middle of the player
        }
        else
        {
            IsTouchingCeiling = false;
        }
    }
}
