using System.Collections;
using System.Collections.Generic;
using Shared;
using UnityEngine;

namespace Enemies
{
    public class LaserBeamAttack : MonoBehaviour, IAttack
    {
        
        public bool IsAttacking { get; set; }
        [SerializeField] private float laserSpeed;
        [SerializeField] private GameObject laser;
        [SerializeField] private float scaleOffset; 
        [SerializeField] private Flipper flipper;
        [SerializeField] private float duration;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private string targetTag;
        [SerializeField] private int damage;
        [SerializeField] private float damageInterval;

        private readonly HashSet<Collider2D> _hitTargets = new();

        private Transform _laserTransform;
        private Vector3 _originalScale;
        private Vector3 _originalPosition;
        private Vector3 _originalBoxColliderSize;
        private Coroutine _currentAttack;
        
        private void Start()
        {
            _laserTransform = laser.transform;
            _originalScale = _laserTransform.localScale;
            _originalPosition = _laserTransform.localPosition;
            _originalBoxColliderSize = boxCollider.size * scaleOffset;
        }

        public void Attack()
        {
            IsAttacking = true;
            laser.SetActive(true);
            _currentAttack = StartCoroutine(FireLaser());
        }

        public void StopAttack()
        {
            if (_currentAttack != null)
            {
                StopCoroutine(_currentAttack);
                StopLaser();
            }
        }

        private IEnumerator FireLaser()
        {
            float time = 0f;
            StartCoroutine(ReapplyDamageCoroutine());
            while (time < duration)
            {
                Vector3 scaleChange = Vector3.right * (laserSpeed * Time.deltaTime);
                float direction = flipper.facingRight ? 1f : -1f;
            
                _laserTransform.localScale += scaleChange;
                _laserTransform.position += scaleChange * (scaleOffset * direction);
                
                boxCollider.size = _laserTransform.localScale * _originalBoxColliderSize.x;

                Collider2D[] hits = Physics2D.OverlapBoxAll(boxCollider.bounds.center, boxCollider.size, boxCollider.transform.rotation.eulerAngles.z);
                foreach (var hit in hits)
                {
                    if (hit.CompareTag(targetTag) && !_hitTargets.Contains(hit))
                    {
                        _hitTargets.Add(hit);
                        Health healthComponent = hit.GetComponent<Health>();
                        healthComponent.TakeDamage(damage);
                    }
                }
                
                time += Time.deltaTime;
                yield return null;
            }
            StopLaser();
        }

        private void StopLaser()
        {
            _laserTransform.localScale = _originalScale;
            _laserTransform.localPosition = _originalPosition; 

            laser.SetActive(false);
            IsAttacking = false;
        }

        private IEnumerator ReapplyDamageCoroutine()
        {
            while (IsAttacking)
            {
                _hitTargets.Clear();
                yield return new WaitForSeconds(damageInterval);
            }
        }
    }
}