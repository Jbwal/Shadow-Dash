using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;

    [Header("Dash")]
    [SerializeField] private float dashForce = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float verticalDashMultiplier = 0.5f;

    private bool isDashing = false;
    private bool canDash = true;
    private float dashTimer = 0f;
    private float cooldownTimer = 0f;
    private Vector2 dashDirection = Vector2.right;
    private bool hasDashedInAir = false;

    private bool hasDoubleJump = false;
    private bool canDoubleJump = false;

    private bool isFrozen = false;

    private PlayerHealth playerHealth;
    private Animator animator;

    private void Start()
    {
        if (PlayerPrefs.HasKey("DoubleJump"))
            hasDoubleJump = true;
    }

    public void EnableDoubleJump()
    {
        hasDoubleJump = true;
    }

    public void UpgradeSpeed(float amount)
    {
        speed += amount;
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        animator = GetComponent<Animator>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        body.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        if (isDashing || isFrozen) return;

        float horizontal = 0f;

        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            horizontal = -1f;
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            horizontal = 1f;

        body.linearVelocity = new Vector2(horizontal * speed, body.linearVelocity.y);
        if (horizontal != 0)
        {
            spriteRenderer.flipX = horizontal < 0;
        }
    }

    private void Update()
    {
        if (isFrozen) return;

        HandleDashTimers();

        HandleMovementInput();

        HandleJump();

        HandleDash();

        UpdateAnimator();
    }

    // ---------------- GROUND ----------------
    private bool IsGrounded()
    {
        return Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            groundDistance,
            groundLayer
        );
    }

    // ---------------- INPUT ----------------
    private void HandleMovementInput()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            horizontal = -1f;
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            horizontal = 1f;

        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
            vertical = 1f;
        else if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
            vertical = -1f;

        if (horizontal != 0f)
            dashDirection = new Vector2(horizontal, vertical * verticalDashMultiplier).normalized;
    }

    // ---------------- JUMP ----------------
    private void HandleJump()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (IsGrounded())
            {
                body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                canDoubleJump = hasDoubleJump;
            }
            else if (canDoubleJump)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, 0f);
                body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                canDoubleJump = false;
            }
        }
    }

    // ---------------- DASH ----------------
    private void HandleDash()
    {
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame && canDash && !isDashing)
        {
            isDashing = true;
            canDash = false;

            dashTimer = dashDuration;
            cooldownTimer = dashCooldown;

            body.gravityScale = 0f;
            body.linearVelocity = dashDirection * dashForce;

            playerHealth?.SetInvincible(true);

            if (!IsGrounded())
                hasDashedInAir = true;
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                isDashing = false;
                body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
                playerHealth?.SetInvincible(false);
            }
        }

        if (!isDashing)
            body.gravityScale = 1f;
    }

    // ---------------- DASH COOLDOWN ----------------
    private void HandleDashTimers()
    {
        if (!canDash)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f && IsGrounded())
            {
                canDash = true;
                hasDashedInAir = false;
            }
        }

        if (IsGrounded() && hasDashedInAir)
        {
            canDash = true;
            hasDashedInAir = false;
            cooldownTimer = 0f;
        }
    }

    // ---------------- ANIMATOR ----------------
    private void UpdateAnimator()
    {
        if (animator == null) return;

        animator.SetFloat("speed", Mathf.Abs(body.linearVelocity.x));
        animator.SetBool("grounded", IsGrounded());
        animator.SetBool("dashing", isDashing);
        animator.SetFloat("verticalSpeed", body.linearVelocity.y);
    }

    // ---------------- FREEZE ----------------
    public void SetFrozen(bool state)
    {
        isFrozen = state;
        body.linearVelocity = Vector2.zero;
        body.gravityScale = state ? 0f : 1f;
    }
}