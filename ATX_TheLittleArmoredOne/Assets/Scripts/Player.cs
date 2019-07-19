using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // configuration params
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float rollSpeed = 8.5f;
    [SerializeField] float jumpHeight = 4.0f;
    [SerializeField] Vector2 deathJump = new Vector2 (0f, 18f);
    float originOffset = 0.09f;

    // cached component references
    private Rigidbody2D rigidBody;
    private Animator animator;
    private CapsuleCollider2D bodyCollider;
    private BoxCollider2D feetCollider;

    // state
    private bool isAlive = true;
    private Vector2 origCapsuleColliderOffset;
    private Vector2 origCapsuleColliderSize;
    private RaycastHit2D hit; 

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();

        origCapsuleColliderOffset = new Vector2(bodyCollider.offset.x, bodyCollider.offset.y);
        origCapsuleColliderSize = new Vector2(bodyCollider.size.x, bodyCollider.size.y); 
        origBoxColliderOffset = new Vector2(feetCollider.offset.x, feetCollider.offset.y);
        origBoxColliderSize = new Vector2(feetCollider.size.x, feetCollider.size.y);
    }

    void Update()
    {
        if (!isAlive) { return; }
        Run();
        MirrorPlayer();
        Jump();
        Roll();
        UpdateCapsuleCollider();
        Die();

        // need to fix (always to the right when standing still...)
        float thing = originOffset * Mathf.Sign(rigidBody.velocity.x);
        // print(thing);
        Vector2 thing2 = new Vector2 (transform.position.x + thing, transform.position.y);
        hit = Physics2D.Raycast(thing2, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(thing2, Vector2.down, Color.magenta, 0.1f);
    }

    private void Run()
    {
        // adjusting x velocity
        float moveInput = Input.GetAxis("Horizontal"); 
        Vector2 playerVelocity = new Vector2(moveInput * runSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        // animation transition
        bool isRunning = (Mathf.Abs(rigidBody.velocity.x) > 0) && (hit.normal.y > 0.9f);
        animator.SetBool("Running", isRunning);
    }

     private void MirrorPlayer()
    {
        bool isRunning = Mathf.Abs(rigidBody.velocity.x) > 0;

        if (isRunning) 
        {
            transform.localScale = new Vector2 (Mathf.Sign(rigidBody.velocity.x), 1f);
        }
    }

    private void Jump()
    {
        LayerMask ground = LayerMask.GetMask("Ground");
        bool isTouchingGround = feetCollider.IsTouchingLayers(ground);
        Vector2 jumpVelocity = new Vector2(0, 
                Mathf.Sqrt(-2.0f * Physics2D.gravity.y * jumpHeight));

        // full jump
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            rigidBody.velocity = jumpVelocity;

            animator.SetTrigger("TakingOff"); 
        }
        
        // early release jump
        else if (Input.GetButtonUp("Jump"))
        {
            if (rigidBody.velocity.y > 0)
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y * 0.333f);
            }
        }

        // animation transition
        animator.SetBool("Jumping", !isTouchingGround); 
    }

    private void UpdateCapsuleCollider() 
    {
        // updating shape of capsule collider depending on if ball shaped or not
        if (animator.GetBool("Jumping") || animator.GetBool("Rolling"))
        {
            bodyCollider.offset = new Vector2(-0.005f, 0.005f);
            bodyCollider.size = new Vector2(0.0001f, 0.45f);
        } 
        else 
        {
            bodyCollider.offset = origCapsuleColliderOffset;
            bodyCollider.size = origCapsuleColliderSize;
        }
    }

    private void Die() 
    {
        LayerMask enemy = LayerMask.GetMask("Enemy");
        LayerMask hazards = LayerMask.GetMask("Hazards");

        if (bodyCollider.IsTouchingLayers(enemy) || feetCollider.IsTouchingLayers(enemy) ||
            bodyCollider.IsTouchingLayers(hazards) || feetCollider.IsTouchingLayers(hazards))
        {
            isAlive = false;
        
            rigidBody.velocity = deathJump;
            animator.SetTrigger("Dying");

            FindObjectOfType<GameSession>().ArrangePlayerFuneral();
        }
    }

    private void Roll()
    { 
        bool isRolling = hit.normal.y < 0.9f;
        float rollAngle = Vector2.Angle(hit.point, hit.normal);

        float moveInput = Input.GetAxis("Horizontal"); 
        // Debug.Log(hit.normal.y);

        if (moveInput > 0)
        {
            if (isRolling && rollAngle > 130.0f)
            {
                IncreaseRollVelocity(moveInput);
            }
        }
        else if (moveInput < 0)
        {
            if (isRolling && (rollAngle > 50.0f && rollAngle < 80.0f))
            {
                IncreaseRollVelocity(moveInput);
            }
        }

        // animation transition
        animator.SetBool("Rolling", isRolling);
    }

    private void IncreaseRollVelocity(float moveInput)
    {
        Vector2 playVelocity = new Vector2(moveInput * rollSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playVelocity;
    }
}
