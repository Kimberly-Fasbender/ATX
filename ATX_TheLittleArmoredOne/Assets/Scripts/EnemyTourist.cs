using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTourist : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Collider2D collider2d;
    Rigidbody2D rigidBody;
    Player player;
    
    void Start()
    {
        collider2d = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
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
        if (collision != player.GetComponent<CircleCollider2D>())
        {
            transform.localScale = new Vector2(-(Mathf.Sign(rigidBody.velocity.x)), 1f);
        }
    }

    // private void Freeze()
    // {
    //     if (collision == player.GetComponent<CapsuleCollider2D>() || collision == player.GetComponent<BoxCollider2D>())
    //     {
    //         Debug.Log("HERE WE GO");
    //         gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
    //     }
    // }
}

