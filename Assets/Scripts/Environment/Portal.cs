using System;
using GameManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Environment
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private float chargeTime;
        [SerializeField] private TextMeshProUGUI chargeTimeText; // Reference to TextMeshPro UI component
        [SerializeField] private Image portalFilling;
        
        
        private bool isCharging;
        private bool isOpen;
        private float currentChargeTime;

        private void Start()
        {
            chargeTimeText.text = Math.Floor(chargeTime).ToString();
            currentChargeTime = chargeTime;
        }

        private void Update()
        {
            if (isCharging)
            {
                currentChargeTime -= Time.deltaTime;
                chargeTimeText.text = Math.Ceiling(currentChargeTime).ToString();
                portalFilling.fillAmount = 1 - (currentChargeTime / chargeTime);
                
                if (currentChargeTime <= 0)
                {
                    isOpen = true;
                    isCharging = false;
                }
            }
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (isOpen)
                {
                    SceneController.Instance.StartNextStage();
                }
                else
                {
                    isCharging = true;
                }
            }
        }
        
    }
}
