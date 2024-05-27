using Shared;
using UnityEngine;

namespace Enemies
{
    
    public class MoodSwingerController : MonoBehaviour
    {
        [SerializeField] private MoodSwingerAIStateHandler aiStateHandler;
        [SerializeField] private DirectFollow directFollow;
        [SerializeField] private Transform playerTransform; //Should be in directFollow instead?

        
        private void Start()
        {
        }
        
        private void Update()
        {
            switch (aiStateHandler.currentState)
            {
                case MoodSwingerState.Following:
                    directFollow.FollowTarget(playerTransform);
                    break;
                case MoodSwingerState.Attacking:
                    break;
                case MoodSwingerState.Fleeing:
                    break;
                case MoodSwingerState.Stunned:
                    directFollow.StopFollowTarget();
                    break;
            }
        }
    }
}