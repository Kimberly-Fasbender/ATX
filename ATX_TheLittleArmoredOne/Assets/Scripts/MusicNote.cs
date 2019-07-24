using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNote : MonoBehaviour
{
    [SerializeField] AudioClip musicNoteSFX;

    AudioListener audioListener;

    int points = 1;
    bool isPickedUp = false;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPickedUp)
        {
            audioListener = FindObjectOfType<AudioListener>();

            FindObjectOfType<GameSession>().AddPoints(points);
            AudioSource.PlayClipAtPoint(musicNoteSFX, audioListener.transform.position);
            Destroy(gameObject);
            isPickedUp = true;
        }
    }
}
