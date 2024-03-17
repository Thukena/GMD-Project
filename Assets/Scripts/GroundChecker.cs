using System.Linq;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public LayerMask groundLayer;
    public Transform parent;
    public bool isGrounded;
    
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

   private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCollider.bounds.center, boxCollider.size,groundLayer);

        var ground = colliders.FirstOrDefault(x => x.gameObject.layer == 6);
        
        if (ground != null)
        {
            isGrounded = true;

            var position = parent.position;
            parent.position = new Vector3(position.x, ground.bounds.max.y + 1, position.z);; // Set player position to ground top + half player height since position is in the middle of the player
        }
        else
        {
            isGrounded = false;
        }
    }

}
