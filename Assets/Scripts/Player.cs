using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    
    
    public Rigidbody2D _rigidBody;
    private float _movementX;
    private float _movementZ;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(_movementX, 0f, 0f) * (speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // var movement = new Vector3(_movementX, 00f, 00f);
        // _rigidBody.AddForce(movement * speed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        print(context.ReadValue<Vector2>());
        
        var vector = context.ReadValue<Vector2>();
        
        _movementX = vector.x;
    }
}
