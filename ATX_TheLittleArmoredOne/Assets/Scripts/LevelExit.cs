using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] public AudioClip levelExitSFX;
    float levelLoadDelay = 1.5f;
    float slowMotionExit = 0.2f;
    float origVolume;

    AudioSource audioSource;
    AudioListener audioListener;
    Player player;

    
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        audioListener = FindObjectOfType<AudioListener>();
        player = FindObjectOfType<Player>();

        origVolume = audioSource.volume;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision != player.GetComponent<CircleCollider2D>())
        {
            ExitSFX();
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        Time.timeScale = slowMotionExit;
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        Time.timeScale = 1f;

        SceneManager.LoadScene(currentSceneIndex + 1);
        audioSource.volume = origVolume;
    }

    private void ExitSFX()
    {
        audioSource.volume = 0f;
        AudioSource.PlayClipAtPoint(levelExitSFX, audioListener.transform.position, 0.1f);
    }
}
