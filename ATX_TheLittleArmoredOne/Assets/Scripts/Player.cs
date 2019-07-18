using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // RaycastHit.point and RaycastHit.normal

    [SerializeField] float runSpeed = 5f;
    [SerializeField] float rollSpeed = 20f;
    [SerializeField] float jumpHeight = 4.0f;
    [SerializeField] Vector2 deathJump = new Vector2 (0f, 18f);

    Rigidbody2D rigidBody;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;

    Vector2 origCapsuleColliderOffset;
    Vector2 origCapsuleColliderSize;

    bool isAlive = true;
    private RaycastHit2D hit; 

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();

        origCapsuleColliderOffset = new Vector2(bodyCollider.offset.x, bodyCollider.offset.y);
        origCapsuleColliderSize = new Vector2(bodyCollider.size.x, bodyCollider.size.y); 
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

        hit = Physics2D.Raycast(transform.position, Vector2.down, 5f, LayerMask.GetMask("Ground"));
    }

    private void Run()
    {
        

        float moveInput = Input.GetAxis("Horizontal"); 
        Vector2 playerVelocity = new Vector2(moveInput * runSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        // animation transitions
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

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            Vector2 jumpVelocity = new Vector2(0, 
                Mathf.Sqrt(-2.0f * Physics2D.gravity.y * jumpHeight));
            rigidBody.velocity = jumpVelocity;

            animator.SetTrigger("TakingOff");
        }

        // animation transition
        animator.SetBool("Jumping", !isTouchingGround); 

        // UpdateCapsuleCollider();
    }

    private void UpdateCapsuleCollider() 
    {
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
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) || 
            feetCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
        
            rigidBody.velocity = deathJump;
            animator.SetTrigger("Dying");

            FindObjectOfType<GameSession>().ArrangePlayerFuneral();
        }
    }

    private void Roll()
    { 
        // up is negative, down is positive
        bool isRolling = hit.normal.y < 0.9f;
        float rollAngle = Vector2.Angle(hit.point, hit.normal);

        float moveInput = Input.GetAxis("Horizontal"); 
        Debug.Log(rollAngle);

        if (moveInput > 0)
        {
            if (isRolling && rollAngle > 130.0f)
            {
                Vector2 playVelocity = new Vector2(moveInput * rollSpeed, rigidBody.velocity.y);
                rigidBody.velocity = playVelocity;
            }
        }
        else if (moveInput < 0)
        {
            if (isRolling && (rollAngle > 50.0f && rollAngle < 80.0f))
            {
                Vector2 playVelocity = new Vector2(moveInput * rollSpeed, rigidBody.velocity.y);
                rigidBody.velocity = playVelocity;
            }
        }

        animator.SetBool("Rolling", isRolling);
    }
}
