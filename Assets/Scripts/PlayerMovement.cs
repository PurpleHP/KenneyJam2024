using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float dashSpeed = 10f; // Adjust as needed
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private int jumpsRemaining;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = 1;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Jumping
        if (isGrounded)
        {
            jumpsRemaining = 1; // Reset jumps when grounded
        }

        if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsRemaining--;
        }
    }
}
