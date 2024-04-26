using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public GroundChecker groundChecker;
    public WallChecker wallChecker;
    public CeilingChecker ceilingChecker;
    public float gravity = 5;
    public Animator animator;
    public Flipper flipper;
    public Dash dash;

    private float _currentMovementXInput;
    private float _movementY;
    private bool _jumpAfterDash;
    
    // Start is called before the first frame update
    void Start()
    {
        dash.OnDashEnd += OnDashEnd;
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

        var newPositionX = _currentMovementXInput;
        var newPositionY = _movementY;
        
        if (dash.isDashing)
        {
            var dashSpeed = dash.dashSpeed;
            newPositionX = flipper.facingRight ? dashSpeed : -dashSpeed;
            newPositionY = 0f;
        }

        if (wallChecker.isTouchingWall)
        {
            newPositionX = 0f;
        }
        transform.Translate(new Vector3(newPositionX * speed, newPositionY, 0f) * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _currentMovementXInput = context.ReadValue<Vector2>().x;
        
        if (!dash.isDashing && flipper.ShouldFlip(_currentMovementXInput))
        {
            flipper.Flip();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("JUMP");
            Jump();
        }
    }

    private void Jump()
    {
        if (groundChecker.isGrounded)
        {
            if (dash.isDashing)
            {
                _jumpAfterDash = true;
                return;
            }

            _movementY = jumpHeight;
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
    
    private void OnDashEnd()
    {
        print("Dash ended!");
        if (flipper.ShouldFlip(_currentMovementXInput))
        {
            flipper.Flip();
        }

        if (_jumpAfterDash)
        {
            Jump();
            _jumpAfterDash = false;
        }
    }
    
}
