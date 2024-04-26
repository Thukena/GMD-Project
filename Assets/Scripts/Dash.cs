using System;
using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    
    public bool isDashing = false;
    public bool canDash = true;
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;

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
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}