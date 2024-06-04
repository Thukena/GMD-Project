using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shared
{
    public class MeleeAttack : MonoBehaviour, IAttack
    {

        public bool IsAttacking { get; set; }
        
        [SerializeField] private float damage;
        public float Damage
        {
            get => damage;
            set => damage = value;
        }

        [SerializeField] private float attackCooldown;
        public float AttackCooldown
        {
            get => attackCooldown;
            set => attackCooldown = value;
        }

        [SerializeField] private float attackDuration;
        public float AttackDuration
        {
            get => attackDuration;
            set => attackDuration = value;
        }
        
        [SerializeField] private string targetTag;
        [SerializeField] BoxCollider2D boxCollider;
        [SerializeField] private float knockBackSpeed;
        [SerializeField] private float knockBackDuration;
        [SerializeField] private float stunDurationAfterKnockBack;

        private readonly HashSet<Collider2D> _hitTargets = new();

        public void Attack()
        {
            IsAttacking = true;
            _hitTargets.Clear(); 
            StartCoroutine(PerformAttack());
        }

        public void StopAttack()
        {
            return; //TODO stop coroutine
        }

        private IEnumerator PerformAttack()
        {
            var elapsedTime = 0f;
            const float checkInterval = 0.01f;

            while (elapsedTime < AttackDuration)
            {
                Collider2D[] hits = Physics2D.OverlapBoxAll(boxCollider.bounds.center, boxCollider.size, boxCollider.transform.rotation.eulerAngles.z);
                foreach (var hit in hits)
                {
                    if (hit.CompareTag(targetTag) && !_hitTargets.Contains(hit))
                    {
                        _hitTargets.Add(hit);
                        var healthComponent = hit.GetComponent<Health>();
                        healthComponent.TakeDamage((int)Damage);
                        if (healthComponent.isDead == false)
                        {
                            healthComponent.GetKnockedBack(knockBackSpeed, knockBackDuration, stunDurationAfterKnockBack, transform.position.x);
                        }
                    }
                }

                elapsedTime += checkInterval;
                yield return new WaitForSeconds(checkInterval);
            }

            yield return new WaitForSeconds(AttackCooldown);
            IsAttacking = false;
        }
    }
}