﻿using System.Collections;
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
    SFXController SFXController;

    void Start()
    {
        player = FindObjectOfType<Player>();
        SFXController = FindObjectOfType<SFXController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        origSpeed = player.rollSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(SpeedTimer());
    }

    private IEnumerator SpeedTimer()
    {
        player.rollSpeed = hyperSpeed;
        spriteRenderer.color = green;
        SFXController.PlaySFX("speed", 0.5f);

        yield return new WaitForSeconds(1f);

        player.rollSpeed = origSpeed;
        spriteRenderer.color = white;
    }
}
