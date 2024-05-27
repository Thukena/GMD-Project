using System.Collections;
using System.Collections.Generic;
using Shared;
using UnityEngine;

namespace Player
{
    public class KnockBackHandler : MonoBehaviour
    {
        [SerializeField] private float knockBackSpeed;
        [SerializeField] private float knockBackDuration;
        [SerializeField] private float stunDurationAfterKnockBack; 

        private readonly Dictionary<Health, Coroutine> _activeKnockBacks = new Dictionary<Health, Coroutine>();

        public void KnockBack(Health targetHealth)
        {
            //Check if the target already is being knockedBack
            if (_activeKnockBacks.TryGetValue(targetHealth, out Coroutine existingCoroutine))
            {
                StopCoroutine(existingCoroutine);  
            }

            Coroutine newKnockBackCoroutine = StartCoroutine(KnockBackCoroutine(targetHealth));
            _activeKnockBacks[targetHealth] = newKnockBackCoroutine;

            targetHealth.OnDeath += () =>
            {
                if (_activeKnockBacks.TryGetValue(targetHealth, out var coroutine))
                {
                    StopCoroutine(coroutine);
                    _activeKnockBacks.Remove(targetHealth);
                }
            };
        }

        private IEnumerator KnockBackCoroutine(Health targetHealth)
        {
            var knockBackResistance = targetHealth.knockBackResistance;

            if (knockBackResistance >= 100) yield break;
            
            targetHealth.isStunned = true;  

            var knockBackDirection = new Vector2(targetHealth.transform.position.x - transform.position.x, 0).normalized;
            var targetRigidBody = targetHealth.GetComponent<Rigidbody2D>();

            var actualKnockBackDuration = ApplyResistance(knockBackDuration, knockBackResistance);
            
            float timer = 0;
            while (timer < actualKnockBackDuration)
            {
                var targetWidth = targetHealth.GetComponent<Renderer>().bounds.size.x;
                RaycastHit2D hit = Physics2D.Raycast(targetRigidBody.position, knockBackDirection, targetWidth / 2, LayerMask.GetMask("Ground"));
                if (hit.collider != null)
                {
                    targetRigidBody.position = hit.point - knockBackDirection * targetWidth / 2; //Move to edge of collision +- half target width
                    break;
                }
                targetRigidBody.position += knockBackDirection * (knockBackSpeed * Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(ApplyResistance(stunDurationAfterKnockBack, targetHealth.stunResistence));

            targetHealth.isStunned = false;
            _activeKnockBacks.Remove(targetHealth);
        }
        
        private static float ApplyResistance(float duration, float resistance)
        {
            return duration * (1 - resistance / 100.0f);
        }
        
    }
}