using UnityEngine;

namespace Shared.Collision
{
    public class CollisionChecker : MonoBehaviour
    {
    
        public BoxCollider2D boxCollider;
        [SerializeField] private LayerMask groundLayer;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        public Collider2D GetCollider()
        {
            return Physics2D.OverlapBox(boxCollider.bounds.center, boxCollider.size, 0, groundLayer);
        }
    }
}
