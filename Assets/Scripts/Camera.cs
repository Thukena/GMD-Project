using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject Player;
    
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - Player.transform.position;
    }
    
    private void LateUpdate()
    {
        transform.position = Player.transform.position + offset;
    }
}
