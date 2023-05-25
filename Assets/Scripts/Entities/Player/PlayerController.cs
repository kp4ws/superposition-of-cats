using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isTouchingGround;
    [SerializeField] private bool isTouchingCeiling;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private float ceilingCheckDistance = 0.2f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private BoxCollider2D upperCollider;
    [SerializeField] private BoxCollider2D lowerCollider;

    private Vector2 originalColliderSize;
    private bool isFacingRight = true; // Add this variable to keep track of the player's facing direction
    private bool inputEnabled = true;

    private void Start()
    {
        upperCollider.enabled = true;
        lowerCollider.enabled = true;
    }

    private void Update()
    {
        if (!inputEnabled)
            return;

        // Check if the player is grounded
        isTouchingGround = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        // Check if the player is touching the ceiling
        isTouchingCeiling = Physics2D.Raycast(ceilingCheck.position, Vector2.up, ceilingCheckDistance, groundLayer);

        // Handle player movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip the player if moving in the opposite direction
        if ((moveInput > 0 && !isFacingRight) || (moveInput < 0 && isFacingRight))
        {
            FlipPlayer();
        }

        // Jumping
        if (isTouchingGround && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            anim.SetTrigger("Slide");
            upperCollider.enabled = false;
        }

        // Set parameters for the Animator
        anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isJumping", !isTouchingGround);
        anim.SetBool("topHitCheck", isTouchingCeiling);

        // Debug the ground raycast
        Debug.DrawRay(groundCheck.position, Vector2.down * groundCheckDistance, Color.red);

        // Debug the ceiling raycast
        if(isTouchingCeiling)
        {
            Debug.DrawRay(ceilingCheck.position, Vector2.up * ceilingCheckDistance, Color.red);
        }
        else
        {
            Debug.DrawRay(ceilingCheck.position, Vector2.up * ceilingCheckDistance, Color.blue);
        }
    }

    private void FlipPlayer()
    {
        // Switch the value of isFacingRight
        isFacingRight = !isFacingRight;

        // Get the current scale of the player's transform
        Vector3 scale = transform.localScale;

        // Flip the scale on the X-axis to mirror the sprite
        scale.x *= -1;

        // Apply the new scale to the player's transform
        transform.localScale = scale;
    }

    //Callback from animation event
    public void SlideFinished()
    {
        upperCollider.enabled = true;
    }
}