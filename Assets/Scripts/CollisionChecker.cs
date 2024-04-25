using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    
    public LayerMask groundLayer;
    private BoxCollider2D _boxCollider;

    // Start is called before the first frame update
    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    public Collider2D GetCollider()
    {
        return Physics2D.OverlapBox(_boxCollider.bounds.center, _boxCollider.size, 0, groundLayer);
    }
}
