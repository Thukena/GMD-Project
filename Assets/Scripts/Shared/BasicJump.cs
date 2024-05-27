using Shared.Collision;
using UnityEngine;

namespace Shared
{
    public class BasicJump : MonoBehaviour
    {
        [SerializeField] private GroundChecker groundChecker;
        [SerializeField] private GravityHandler gravityHandler;
        [SerializeField] private float jumpHeight;

        public void Jump()
        {
            if (groundChecker.isGrounded)
            {
                gravityHandler.verticalMovement = jumpHeight;
            }
        }
    }
}