using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    
    public bool isDashing = false;
    public bool canDash = true;

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
        yield return new WaitForSeconds(0.1f);
        isDashing = false;
        yield return new WaitForSeconds(0.5f);
        canDash = true;
    }
}