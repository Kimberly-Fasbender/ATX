using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thwomp : MonoBehaviour
{
    [SerializeField] AudioClip thwompSFX;
    private int player;
    private int ground;
    private float resetSpeed = 5.0f;
    private Vector2 origPos;

    private RaycastHit2D hit;
    private Rigidbody2D rigidBody;
    private Collider2D collider2d;
    private AudioListener audioListener;
    

    void Start()
    {
        player = LayerMask.GetMask("Player");
        ground = LayerMask.GetMask("Ground");
        rigidBody = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<PolygonCollider2D>();
        audioListener = FindObjectOfType<AudioListener>();
        origPos = new Vector2 (transform.position.x, transform.position.y);
    }

    void Update()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, 30.0f, player);
        // Debug.DrawRay(transform.position, Vector2.down, Color.magenta);

        Fall();  
    }

    private void Fall()
    {
        if (hit.collider != null && gameObject.transform.position.y == origPos.y)
        {
            rigidBody.bodyType = RigidbodyType2D.Dynamic;
        }
        else if (collider2d.IsTouchingLayers(ground))
        {
            AudioSource.PlayClipAtPoint(thwompSFX, audioListener.transform.position);
            rigidBody.bodyType = RigidbodyType2D.Kinematic;
        }
        else if (gameObject.transform.position.y < origPos.y)
        {
            float step = resetSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, origPos, step);
        }
    }
}
