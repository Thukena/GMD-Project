using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shared
{
    public class AttackHandler : MonoBehaviour
    {
        public float attackDuration;
        public bool canAttack = true;
        public float damage;
        public float attackCooldown;
        [SerializeField] private string targetTag;
        [SerializeField] BoxCollider2D boxCollider;
        [SerializeField] private float knockBackSpeed;
        [SerializeField] private float knockBackDuration;
        [SerializeField] private float stunDurationAfterKnockBack;

        private readonly HashSet<Collider2D> _hitTargets = new();

        public void Attack()
        {
            canAttack = false;
            _hitTargets.Clear(); 
            StartCoroutine(PerformAttack());
        }
        
        private IEnumerator PerformAttack()
        {
            var elapsedTime = 0f;
            const float checkInterval = 0.01f;

            while (elapsedTime < attackDuration)
            {
                Collider2D[] hits = Physics2D.OverlapBoxAll(boxCollider.bounds.center, boxCollider.size, boxCollider.transform.rotation.eulerAngles.z);
                foreach (var hit in hits)
                {
                    if (hit.CompareTag(targetTag) && !_hitTargets.Contains(hit))
                    {
                        _hitTargets.Add(hit);
                        var healthComponent = hit.GetComponent<Health>();
                        healthComponent.TakeDamage((int)damage);
                        if (healthComponent.isDead == false)
                        {
                            healthComponent.GetKnockedBack(knockBackSpeed, knockBackDuration, stunDurationAfterKnockBack, transform.position.x);
                        }
                    }
                }

                elapsedTime += checkInterval;
                yield return new WaitForSeconds(checkInterval);
            }

            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }
    }
}