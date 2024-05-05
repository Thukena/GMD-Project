using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class Dash : MonoBehaviour
    {
    
        public bool isDashing = false;
        public float dashSpeed;
        [SerializeField] private float dashDuration;
        [SerializeField] private float dashCooldown;
        private bool canDash = true;

        public event Action OnDashEnd;

        public void TryDash()
        {
            if (canDash)
            {
                StartCoroutine(PerformDash());
            }
        }
    
        private IEnumerator PerformDash()
        {
            canDash = false;
            isDashing = true;
            yield return new WaitForSeconds(dashDuration);
            isDashing = false;
            OnDashEnd?.Invoke();
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
        }
    }
}