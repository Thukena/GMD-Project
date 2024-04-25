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
    public Flipper flipper;
    public Dash dash;

    private float _movementX;
    private float _movementY;
    
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
            if (dash.isDashing)
            {
                _movementY = 0f;
            }
            else
            {
                _movementY += Physics2D.gravity.y * gravity * Time.deltaTime;
            }
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
        
        if (dash.isDashing)
        {
            newPositionX = flipper.facingRight ? 10f : -10f;
            newPositionY = 0f;
        }
        
        transform.Translate(new Vector3(newPositionX * speed, newPositionY, 0f) * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var vector = context.ReadValue<Vector2>();
        
        _movementX = vector.x;

        if (flipper.ShouldFlip(_movementX))
        {
            flipper.Flip();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("JUMP");
            if (groundChecker.isGrounded && dash.isDashing == false)
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
            dash.TryDash();
        }
    }
}
