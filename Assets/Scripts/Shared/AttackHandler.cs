using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Shared
{
    public class AttackHandler : MonoBehaviour
    {
        public float attackDuration;
        public bool canAttack = true;
        [SerializeField] private int damage;
        [SerializeField] private float attackCooldown;
        [SerializeField] private string targetTag;
        [SerializeField] BoxCollider2D boxCollider;
        [SerializeField] private KnockBackHandler knockBackHandler;

        private bool _canKnockBack;
        private readonly HashSet<Collider2D> _hitTargets = new();

        private void Start()
        {
            _canKnockBack = knockBackHandler != null;
        }

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
                        Health healthComponent = hit.GetComponent<Health>();
                        healthComponent.TakeDamage(damage);
                        if (_canKnockBack && healthComponent.isDead == false)
                        {
                            knockBackHandler.KnockBack(healthComponent);
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