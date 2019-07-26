using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    float levelLoadDelay = 1.5f;
    float slowMotionExit = 0.2f;

    Player player;

    
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision != player.GetComponent<CircleCollider2D>())
        {
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
    }
}
