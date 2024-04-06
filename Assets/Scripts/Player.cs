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
    private bool _facingRight = true;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!GroundChecker.isGrounded)
        {
            _movementY += Physics2D.gravity.y * _gravity * Time.deltaTime;
        }
        else
        {
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

        if (ShouldFlip())
        {
            Flip();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        print("JUMP");
        if (GroundChecker.isGrounded)
        {
            _movementY += jumpHeight;
        }
    }

    private bool ShouldFlip()
    {
        return (_movementX > 0 && !_facingRight) || (_movementX < 0 && _facingRight);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; 
        transform.localScale = localScale;
    }
}
