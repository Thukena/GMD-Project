using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    
    public LayerMask groundLayer;
    public bool isColliding;
    public new Collider2D collider;
    
    private BoxCollider2D _boxCollider;

    // Start is called before the first frame update
    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        collider = Physics2D.OverlapBox(_boxCollider.bounds.center, _boxCollider.size,0,groundLayer);
        isColliding = collider != null;
    }
}
