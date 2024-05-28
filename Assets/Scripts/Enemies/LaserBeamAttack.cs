using System.Collections;
using Shared;
using UnityEngine;

namespace Enemies
{
    public class LaserBeamAttack : MonoBehaviour, IAttack
    {
        
        public bool IsAttacking { get; set; }
        [SerializeField] private float laserSpeed;
        [SerializeField] private SpriteRenderer laser;
        [SerializeField] private float scaleOffset; 
        [SerializeField] private Flipper flipper;
        [SerializeField] private float duration;

        private Transform _laserTransform;
        private Vector3 _originalScale;
        private Vector3 _originalPosition;
        private void Start()
        {
            _laserTransform = laser.transform;
            _originalScale = _laserTransform.localScale;
            _originalPosition = _laserTransform.localPosition;
        }

        public void Attack()
        {
            IsAttacking = true;
            laser.gameObject.SetActive(true);
            StartCoroutine(FireLaser());
        }

        private IEnumerator FireLaser()
        {
            float time = 0f;

            while (time < duration)
            {
                Vector3 scaleChange = Vector3.right * (laserSpeed * Time.deltaTime);
                float direction = flipper.facingRight ? 1f : -1f;
            
                _laserTransform.localScale += scaleChange;
                _laserTransform.position += scaleChange * (scaleOffset * direction);
                
                time += Time.deltaTime;
                yield return null;
            }
            
            _laserTransform.localScale = _originalScale;
            _laserTransform.localPosition = _originalPosition; 

            laser.gameObject.SetActive(false);
            IsAttacking = false;
        }
    }
}