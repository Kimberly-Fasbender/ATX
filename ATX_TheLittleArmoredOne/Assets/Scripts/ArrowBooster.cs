using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBooster : MonoBehaviour
{
    [SerializeField] AudioClip speedSFX;
    private float origSpeed;
    private float hyperSpeed = 30f;

    private Color white = new Color (1f, 1f, 1f, 1f);
    private Color green = new Color (0f, 1f, 0f, 1f);

    Player player;
    SpriteRenderer spriteRenderer;
    AudioListener audioListener;

    void Start()
    {
        player = FindObjectOfType<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioListener = FindObjectOfType<AudioListener>();
        
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
        AudioSource.PlayClipAtPoint(speedSFX, audioListener.transform.position, 0.3f);
        // SFXController = FindObjectOfType<SFXController>();
        // SFXController.PlaySFX("speed", 0.5f);

        yield return new WaitForSeconds(1f);

        player.rollSpeed = origSpeed;
        spriteRenderer.color = white;
    }
}
