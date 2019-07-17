using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNote : MonoBehaviour
{
    int points = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // play one off sound on camera //
        FindObjectOfType<GameSession>().AddPoints(points);
        Destroy(gameObject);
    }
}
