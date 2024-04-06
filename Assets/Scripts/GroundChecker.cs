using System.Linq;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public LayerMask groundLayer;
    public Transform parentTransform;
    public bool isGrounded;
    
    private BoxCollider2D boxCollider;
    private Renderer parentRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        parentRenderer = parentTransform.GetComponent<Renderer>();
    }

   private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCollider.bounds.center, boxCollider.size,0,groundLayer);

        var ground = colliders.FirstOrDefault(x => x.gameObject.layer == LayerMask.NameToLayer("Ground"));
        
        if (ground != null)
        {
            isGrounded = true;

            var position = parentTransform.position;
            parentTransform.position = new Vector3(position.x, ground.bounds.max.y + parentRenderer.bounds.size.y / 2, position.z);; // Set player position to ground top + half player height since position is in the middle of the player
        }
        else
        {
            isGrounded = false;
        }
    }

}
