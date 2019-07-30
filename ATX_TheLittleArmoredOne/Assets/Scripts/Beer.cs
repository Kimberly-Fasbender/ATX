using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : MonoBehaviour
{
    [SerializeField] public AudioClip extraLifeSFX;

    int lives = 1;
    bool isPickedUp = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = FindObjectOfType<Player>();
        AudioListener audioListener = FindObjectOfType<AudioListener>();

        if (!isPickedUp && 
            player.isAlive && 
            collision != player.GetComponent<CircleCollider2D>())
        {
            GameSession gameSession = FindObjectOfType<GameSession>();
            gameSession.AddLives(lives);
            AudioSource.PlayClipAtPoint(extraLifeSFX, audioListener.transform.position);
            Destroy(gameObject);
            isPickedUp = true;
        }
    }
}
