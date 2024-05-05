using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectFollow : MonoBehaviour, IFollow
{
    [SerializeField] private float speed;
    
    public void FollowTarget(Transform target)
    {
        var direction = new Vector3(target.position.x, transform.position.y, transform.position.z);
        
        transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
    }
}
