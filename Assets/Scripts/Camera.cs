using Player;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 offset;
    private Transform _playerTransform;
    private void Start()
    {
        _playerTransform = PlayerController.Instance.transform;
        offset = transform.position - _playerTransform.position;
    }

    private void LateUpdate()
    {
        if (_playerTransform != null)
        {
            transform.position = _playerTransform.position + offset;
        }
    }
}
