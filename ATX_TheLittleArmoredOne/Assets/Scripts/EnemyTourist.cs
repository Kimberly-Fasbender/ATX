using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTourist : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D rigidBody;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    private void Move() 
    {
        if (IsFacingRight())
        {
            rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);
        }
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidBody.velocity.x)), 1f);
    }
}

