using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngineInternal;


public class Player : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float horizontal = 0f;

        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            horizontal = -1f;
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            horizontal = 1f;

        body.linearVelocity = new Vector2(horizontal * speed, body.linearVelocity.y);
    }
    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            if (IsGrounded())
            {
                body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

        }
    }
    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        return hit.collider != null;
    }
}
