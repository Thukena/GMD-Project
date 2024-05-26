using System;
using System.Collections;
using Shared;
using Shared.Collision;
using UnityEngine;

namespace Player
{
    public class Dash : MonoBehaviour
    {
    
        public bool isDashing = false;
        public float dashSpeed;
        [SerializeField] private float dashDuration;
        [SerializeField] private float dashCooldown;
        [SerializeField] private Flipper flipper; 
        [SerializeField] private WallChecker wallChecker; 
        private bool _canDash = true;

        public event Action OnDashEnd;

        public void TryDash()
        {
            if (_canDash)
            {
                StartCoroutine(PerformDash());
            }
        }

        private IEnumerator PerformDash()
        {
            _canDash = false;
            isDashing = true;

            float timeBeforeDashing = Time.time;
            while (Time.time < timeBeforeDashing + dashDuration)
            {
                if (!wallChecker.isTouchingWall)
                {
                    float newPositionX = flipper.facingRight ? dashSpeed : -dashSpeed;
                    transform.Translate(new Vector3(newPositionX, 0, 0) * Time.deltaTime);
                }

                yield return null;
            }

            isDashing = false;
            OnDashEnd?.Invoke();
            yield return new WaitForSeconds(dashCooldown);
            _canDash = true;
        }
    }
}