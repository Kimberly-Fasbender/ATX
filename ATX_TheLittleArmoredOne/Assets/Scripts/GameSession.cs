using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    int startScreenIndex = 0;

    void Awake()
    {
        int numOfSessions = FindObjectsOfType<GameSession>().Length;

        if (numOfSessions > 1)
        {
            Destroy(gameObject);
        }
        else 
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ArrangePlayerFuneral()
    {
        if (playerLives > 1)
        {
            RemoveLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void RemoveLife()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        playerLives -= 1;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(startScreenIndex);
        Destroy(gameObject);
    }
}
