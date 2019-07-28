using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] public AudioClip bounceSFX;
    AudioListener audioListener;
    Player player;

    void Start()
    {
        audioListener = FindObjectOfType<AudioListener>();
        player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision == player.GetComponent<BoxCollider2D>())
        {
            AudioSource.PlayClipAtPoint(bounceSFX, audioListener.transform.position, 0.3f);
        }
    }
}
