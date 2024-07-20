using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float dashSpeed = 20f; 
    [SerializeField] float dashDuration = 0.2f; 
    [SerializeField] float dashCooldown = 1f; 
    
    [SerializeField] Transform groundCheck1;
    [SerializeField] Transform groundCheck2;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private GameObject SpawnPoint;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool wasGrounded;
    private int jumpsRemaining;
    private bool isDashing;
    private float dashTime;
    private float lastDashTime;
    private float horizontalInput;
    private float lastDirection = 1f; // 1 for right, -1 for left
    
    // Sound
    public AudioSource src;
    public List<AudioClip> sfx = new List<AudioClip>(3); //0 -> Walk, 1 -> Dash, 2 -> Land, 3 -> Jump

    // Animation
    private Animator anim;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsSprintJumping = Animator.StringToHash("isSprintJumping");

    private void Awake()
    {
        transform.position = SpawnPoint.transform.position;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = 1;
        isDashing = false;
        dashTime = 0f;
        lastDashTime = -dashCooldown; // So that the player can dash immediately at start
        wasGrounded = true;
    }
private void Update()
{
    isGrounded = Physics2D.OverlapCircle(groundCheck1.position, 0.15f, groundLayer) || Physics2D.OverlapCircle(groundCheck2.position, 0.15f, groundLayer);

    if (isGrounded && !wasGrounded) // Landing sound
    {
        src.PlayOneShot(sfx[2]);
    }

    wasGrounded = isGrounded;

    if (!isDashing)
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        // Update facing direction based on input
        if (horizontalInput > 0)
        {
            lastDirection = 1f;
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y); // Face right
        }
        else if (horizontalInput < 0)
        {
            lastDirection = -1f;
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y); // Face left
        }

        // Update horizontal velocity while preserving the vertical velocity
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
       
        if (isGrounded)
        {
            jumpsRemaining = 1; // Reset jumps when grounded
            dashCooldown = 0f;
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsRemaining--;
            src.PlayOneShot(sfx[3]);
        }

        // Dashing
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDashTime + dashCooldown)
        {
            StartDash();
        }

       

        SetAnimation();
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
        src.PlayOneShot(sfx[1]);

        
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

    private void SetAnimation()
    {
        if (isGrounded)
        {
            anim.SetBool(IsJumping, false);
            anim.SetBool(IsSprintJumping, false);
        }
        else
        {
            if (Mathf.Abs(horizontalInput) > 0.1f) // If moving horizontally in the air
            {
                anim.SetBool(IsJumping, false);
                anim.SetBool(IsSprintJumping, true);
            }
            else // If not moving horizontally in the air
            {
                anim.SetBool(IsJumping, true);
                anim.SetBool(IsSprintJumping, false);
            }
        }

        if (isGrounded && Mathf.Abs(horizontalInput) > 0.1f) // If grounded and moving horizontally
        {
            anim.SetBool(IsWalking, true);
        
        }
        else
        {
            anim.SetBool(IsWalking, false);
        }
    }
}
