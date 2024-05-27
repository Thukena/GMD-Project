using Shared.Collision;
using UnityEngine;

namespace Shared
{
    public class GravityHandler : MonoBehaviour
    {
        [SerializeField] private float gravityStrength = 5.0f;
        [SerializeField] private CeilingChecker ceilingChecker;
        [SerializeField] private GroundChecker groundChecker;

        public float verticalMovement;

        public bool isAffectedByGravity = true;

        private void Update()
        {
            if (!isAffectedByGravity || (verticalMovement > 0 && ceilingChecker.isTouchingCeiling) || (verticalMovement <= 0 && groundChecker.isGrounded)) 
            {
                verticalMovement = 0;
            }
            else
            {
                verticalMovement += Physics2D.gravity.y * gravityStrength * Time.deltaTime;
            }
            
            transform.Translate(Vector2.up * (verticalMovement * Time.deltaTime));
        }
    }
}
