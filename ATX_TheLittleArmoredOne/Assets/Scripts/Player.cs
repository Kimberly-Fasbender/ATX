using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpHeight = 4.0f;

    Rigidbody2D rigidBody;
    Animator animator;
    Collider2D collider;
    CapsuleCollider2D capsuleCollider;
    Vector2 origCapsuleColliderOffset;
    Vector2 origCapsuleColliderSize;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        origCapsuleColliderOffset = new Vector2(capsuleCollider.offset.x, capsuleCollider.offset.y);
        origCapsuleColliderSize = new Vector2(capsuleCollider.size.x, capsuleCollider.size.y); 
    }

    void Update()
    {
        Run();
        MirrorSprite();
        Jump();
    }
     private void Run()
    {
        float moveInput = Input.GetAxis("Horizontal"); 
        Vector2 playerVelocity = new Vector2(moveInput * runSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        // animation transition 
        bool isRunning = Mathf.Abs(rigidBody.velocity.x) > 0;
        animator.SetBool("Running", isRunning);
    }

     private void MirrorSprite()
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
        bool isTouchingGround = collider.IsTouchingLayers(ground);

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            Vector2 jumpVelocity = new Vector2(0, 
                Mathf.Sqrt(-2.0f * Physics2D.gravity.y * jumpHeight));
            rigidBody.velocity = jumpVelocity;

            animator.SetTrigger("TakingOff");
        }

        // animation transition
        animator.SetBool("Jumping", !isTouchingGround); 

        UpdateCapsuleCollider();
    }

    private void UpdateCapsuleCollider() 
    {
         if (animator.GetBool("Jumping") == true)
        {
            capsuleCollider.offset = new Vector2(-0.005f, 0.005f);
            capsuleCollider.size = new Vector2(0.0001f, 0.45f);
        } 
        else 
        {
            capsuleCollider.offset = origCapsuleColliderOffset;
            capsuleCollider.size = origCapsuleColliderSize;
        }
    }
}
