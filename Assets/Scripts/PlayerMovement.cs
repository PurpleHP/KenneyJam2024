using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float dashSpeed = 20f; // Adjust as needed
    [SerializeField] float dashDuration = 0.2f; // Adjust as needed
    [SerializeField] float dashCooldown = 1f; // Adjust as needed
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private int jumpsRemaining;
    private bool isDashing;
    private float dashTime;
    private float lastDashTime;
    private float horizontalInput;
    private float lastDirection = 1f; // 1 for right, -1 for left

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = 1;
        isDashing = false;
        dashTime = 0f;
        lastDashTime = -dashCooldown; // So that the player can dash immediately at start
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (!isDashing)
        {
            horizontalInput = Input.GetAxis("Horizontal");

            // Update facing direction based on input
            if (horizontalInput > 0)
            {
                lastDirection = 1f;
                transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z); // Face right
            }
            else if (horizontalInput < 0)
            {
                lastDirection = -1f;
                transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z); // Face left
            }

            // Update horizontal velocity while preserving the vertical velocity
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

            // Jumping
            if (isGrounded)
            {
                jumpsRemaining = 1; // Reset jumps when grounded
                dashCooldown = 0f;
            }

            if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpsRemaining--;
            }

            // Dashing
            if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDashTime + dashCooldown)
            {
                StartDash();
            }
        }
        else
        {
            ContinueDash();
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTime = Time.time;
        lastDashTime = Time.time;

        rb.velocity = new Vector2(lastDirection * dashSpeed, rb.velocity.y); // Dashing in the direction the player is facing
    }

    private void ContinueDash()
    {
        if (Time.time >= dashTime + dashDuration)
        {
            EndDash();
        }
    }

    private void EndDash()
    {
        isDashing = false;
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            rb.velocity = new Vector2(lastDirection * dashSpeed, rb.velocity.y); // Maintain dash speed during the dash
        }
    }
}
