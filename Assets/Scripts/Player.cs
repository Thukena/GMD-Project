using System.Collections;
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
    private bool _canDash = true;
    private bool _isDashing = false;
    
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

        var newPositionX = _movementX;
        var newPositionY = _movementY;
        
        if (_isDashing)
        {
            newPositionX = _facingRight ? 10f : -10f;
            newPositionY = 0f;
        }
        
        if (wallChecker.isColliding)
        {
            newPositionX = 0f;
        }
        
        transform.Translate(new Vector3(newPositionX * speed, newPositionY, 0f) * Time.deltaTime);
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
            if (groundChecker.isGrounded && _isDashing == false)
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
    
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("DASH");
            if (_canDash)
            {
                StartCoroutine(Dash());
            }
        }
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        var oldGravityValue = gravity;
        gravity = 0f;
        yield return new WaitForSeconds(0.1f);
        gravity = oldGravityValue;
        _isDashing = false;
        yield return new WaitForSeconds(0.5f);
        _canDash = true;
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
