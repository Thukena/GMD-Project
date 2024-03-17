using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public GroundChecker GroundChecker;
    public float _gravity = 5;

    private float _movementX;
    private float _movementY;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!GroundChecker.isGrounded) {
            _movementY += Physics2D.gravity.y * _gravity * Time.deltaTime;
        } else {
            if (_movementY < 0)
            {
                _movementY = 0f;
            }
        }
        
        transform.Translate(new Vector3(_movementX * speed, _movementY, 0f) * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var vector = context.ReadValue<Vector2>();
        
        _movementX = vector.x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        print("JUMP");
        if (GroundChecker.isGrounded)
        {
            _movementY += jumpHeight;
        }
    }
}
