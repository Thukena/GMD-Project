using System.Collections;
using Shared;
using UnityEngine;

namespace Player
{
    public class KnockBackHandler : MonoBehaviour
    {
        [SerializeField] private float knockBackSpeed;
        [SerializeField] private float knockBackDuration;
        [SerializeField] private float stunDurationAfterKnockBack; 


        public void KnockBack(Health targetHealth)
        {
            StartCoroutine(KnockBackCoroutine(targetHealth));
        }

        private IEnumerator KnockBackCoroutine(Health targetHealth)
        {
            var knockBackResistance = targetHealth.knockBackResistance;

            if (knockBackResistance >= 100) yield break;
            
            targetHealth.isStunned = true;  

            var knockBackDirection = (targetHealth.transform.position - transform.position).normalized;
            var targetRigidBody = targetHealth.GetComponent<Rigidbody2D>();

            var actualKnockBackDuration = ApplyResistance(knockBackDuration, knockBackResistance);
            
            float timer = 0;
            while (timer < actualKnockBackDuration)
            {
                targetRigidBody.velocity = knockBackDirection * knockBackSpeed;
                timer += Time.deltaTime;
                yield return null;
            }

            targetRigidBody.velocity = Vector2.zero;

            yield return new WaitForSeconds(ApplyResistance(stunDurationAfterKnockBack, targetHealth.stunResistence));

            targetHealth.isStunned = false;
        }
        
        private static float ApplyResistance(float duration, float resistance)
        {
            return duration * (1 - resistance / 100.0f);
        }
        
    }
}