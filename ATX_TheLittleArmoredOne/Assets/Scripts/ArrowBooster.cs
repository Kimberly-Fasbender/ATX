using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBooster : MonoBehaviour
{
    private float origSpeed;
    private float hyperSpeed = 30f;
    
    private Color white = new Color (1f, 1f, 1f, 1f);
    private Color green = new Color (0f, 1f, 0f, 1f);

    Player player;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        player = FindObjectOfType<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        origSpeed = player.rollSpeed;
        Debug.Log(spriteRenderer.color);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(SpeedTimer());
    }

    private IEnumerator SpeedTimer()
    {
        player.rollSpeed = hyperSpeed;
        spriteRenderer.color = green;

        yield return new WaitForSeconds(1f);

        player.rollSpeed = origSpeed;
        spriteRenderer.color = white;
    }
}
