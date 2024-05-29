using System.Collections;
using UnityEngine;

namespace Shared
{
    public class KnockBackHandler : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Rigidbody2D rigidBody;
        private Coroutine _activeKnockBack;

        public void KnockBack(float knockBackSpeed, float knockBackDuration, float stunDuration, float attackerPosition)
        {
            //Check if the target already is being knockedBack
            if (_activeKnockBack != null)
            {
                StopCoroutine(_activeKnockBack);  
            }

            var newKnockBackCoroutine = StartCoroutine(KnockBackCoroutine(knockBackSpeed, knockBackDuration, stunDuration, attackerPosition));
            _activeKnockBack = newKnockBackCoroutine;
        }

        private IEnumerator KnockBackCoroutine(float knockBackSpeed, float knockBackDuration, float stunDuration, float attackerPosition)
        {
            var knockBackResistance = health.knockBackResistance;
            
            if (knockBackResistance >= 100) yield break;
            
            health.isStunned = true;
            
            var knockBackDirection = new Vector2(transform.position.x - attackerPosition, 0).normalized;

            var actualKnockBackDuration = ApplyResistance(knockBackDuration, knockBackResistance);
            
            float timer = 0;
            while (timer < actualKnockBackDuration)
            {
                var width = sprite.bounds.size.x;
                RaycastHit2D hit = Physics2D.Raycast(rigidBody.position, knockBackDirection, width / 2, LayerMask.GetMask("Ground"));
                if (hit.collider != null)
                {
                    rigidBody.position = hit.point - knockBackDirection * width / 2; //Move to edge of collision +- half target width
                    break;
                }
                
                rigidBody.position += knockBackDirection * (knockBackSpeed * Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(ApplyResistance(stunDuration, health.stunResistence));

            health.isStunned = false;
            _activeKnockBack = null;
        }
        
        private static float ApplyResistance(float duration, float resistance)
        {
            return duration * (1 - resistance / 100.0f);
        }
    }
}