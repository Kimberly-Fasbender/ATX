using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTourist : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Collider2D collider2d;
    Rigidbody2D rigidBody;
    Player player;
    EnemyTourist [] enemyTourists;

    bool isEnemy = false;
    
    void Start()
    {
        collider2d = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        enemyTourists = FindObjectsOfType<EnemyTourist>();
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
        EnemyDetection(collision);

        if (collision != player.GetComponent<CircleCollider2D>() && !isEnemy)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(rigidBody.velocity.x)), 1f);
        } 

        isEnemy = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyDetection(collision);

        if (isEnemy)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(rigidBody.velocity.x)), 1f);
        } 

        isEnemy = false;
    }

    private void EnemyDetection(Collider2D collision)
    {
        foreach (EnemyTourist enemy in enemyTourists)
        {
            if (collision == enemy.GetComponent<BoxCollider2D>())
            {
                isEnemy = true;
            }
        }
    }
}

