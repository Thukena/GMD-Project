using Shared;
using UnityEngine;

namespace Enemies
{
    public class MoodSwingerAIStateHandler : MonoBehaviour
    {
        public MoodSwingerState currentState;
        
        [SerializeField] private Health health;
        [SerializeField] private Transform playerTransform;
        
        void Update()
        {
            if (health.isStunned)
            {
                currentState = MoodSwingerState.Stunned;
            }
            else if (ShouldAttack())
            {
                currentState = MoodSwingerState.Attacking;
            }
            else if (ShouldFlee())
            {
                currentState = MoodSwingerState.Fleeing;
            }
            else
            {
                currentState = MoodSwingerState.Following;
            }
        }
        
        private bool ShouldAttack()
        {
            return false;
        }
        
        private bool ShouldFlee()
        {
            return false;
        }
        
    }
}