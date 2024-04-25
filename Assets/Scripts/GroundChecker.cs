using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public Transform parentTransform;
    public bool isGrounded;
    public CollisionChecker collisionChecker;
    private Renderer parentRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        parentRenderer = parentTransform.GetComponent<Renderer>();
    }

   private void FixedUpdate()
    {
        var collider = collisionChecker.GetCollider();
        
        if (collider != null)
        {
            isGrounded = true;

            var position = parentTransform.position;
            parentTransform.position = new Vector3(position.x, collider.bounds.max.y + parentRenderer.bounds.size.y / 2, position.z);; // Set player position to collider top + half player height since position is in the middle of the player
        }
        else
        {
            isGrounded = false;
        }
    }

}
