using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : MonoBehaviour
{
    private Rigidbody2D knightRigidbody2D;
    private Animator knightAnimator;

    private bool knightIsFacingRight = true;
    private bool knightIsJumping = false;
    private bool knightIsGrounded = false;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask ground;
    public float moveInput;
    public float knightSpeed;
    public float knightJumpForse;

    // Start is called before the first frame update
    void Start()
    {
        knightRigidbody2D = GetComponent<Rigidbody2D>();
        knightAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        knightIsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);
        moveInput = Input.GetAxis("Horizontal");

        knightAnimator.SetBool("KnightIsRunning", !(moveInput == 0));

        if (Input.GetKeyDown(KeyCode.UpArrow) && knightIsGrounded)
        {
            knightIsJumping = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && knightIsGrounded)
        {
            knightAnimator.SetBool("KnightIsRolling", true);
        }

        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            knightAnimator.SetBool("KnightIsRolling", false);
        }
    }

    private void FixedUpdate()
    {
        knightRigidbody2D.velocity = new Vector2(moveInput * knightSpeed, knightRigidbody2D.velocity.y);

        if (knightIsFacingRight == false && moveInput > 0)
        {
            FlipKnight();
        }
        else if (knightIsFacingRight == true && moveInput < 0)
        {
            FlipKnight();
        }

        if (knightIsJumping)
        {
            knightRigidbody2D.AddForce(new Vector2(0f, knightJumpForse));
            knightIsJumping = false;
        }
    }

    private void FlipKnight()
    {
        knightIsFacingRight = !knightIsFacingRight;

        Vector3 knightScale = transform.localScale;
        knightScale.x *= -1;

        transform.localScale = knightScale;
    }
}
