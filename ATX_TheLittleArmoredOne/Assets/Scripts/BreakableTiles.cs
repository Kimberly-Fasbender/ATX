using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTiles : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = FindObjectOfType<Player>();
        Collider2D playerCollider = player.GetComponent<Collider2D>();

        if (collision == playerCollider)
        {
            Destroy(gameObject);
        }
    }
}
