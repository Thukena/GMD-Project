using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public GroundChecker groundChecker;
    public CollisionChecker wallChecker;
    public CeilingChecker ceilingChecker;
    public float gravity = 5;
    public Animator animator;

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
        if (ceilingChecker.IsTouchingCeiling)
        {
            if (_movementY > 0)
            {
                _movementY = 0f;
            }
        }
        
        if (!groundChecker.isGrounded)
        {
            _movementY += Physics2D.gravity.y * gravity * Time.deltaTime;
        }
        else
        {
            if (_movementY < 0)
            {
                _movementY = 0f;
            }
        }

        var newPosition = new Vector3(_movementX * speed, _movementY, 0f) * Time.deltaTime;
        
        if (wallChecker.isColliding)
        {
            newPosition.x = 0f;
        }
        transform.Translate(newPosition);
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
        if (context.performed)
        {
            print("JUMP");
            if (groundChecker.isGrounded)
            {
                _movementY += jumpHeight;
            }
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("ATTACK");
            animator.SetTrigger("Attack"); 
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
