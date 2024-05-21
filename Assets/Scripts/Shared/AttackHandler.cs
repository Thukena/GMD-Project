
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shared
{
    public class AttackHandler : MonoBehaviour
    {
        public float attackDuration;
        public bool isAttacking = false;
        [SerializeField] private int damage;
        [SerializeField] private string targetTag;
        [SerializeField] BoxCollider2D boxCollider;
        
        private readonly HashSet<Collider2D> _hitTargets = new();
        
        public void Attack()
        {
            isAttacking = true;
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
                        print(hit.name + " WAS ATTACKED");
                        hit.GetComponent<Health>().TakeDamage(damage);
                    }
                }

                elapsedTime += checkInterval;
                yield return new WaitForSeconds(checkInterval);
            }

            print("DONE ATTACKING");

            isAttacking = false;
        }
    }
}