using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0; //TODO: REMOVE - for debugging only
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

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
        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
    }

    public void ArrangePlayerFuneral()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        playerLives -= 1;
        SceneManager.LoadScene(currentSceneIndex);

        livesText.text = playerLives.ToString();
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(startScreenIndex);
        Destroy(gameObject);
    }

    public void AddPoints(int points)
    {
        playerScore += points;
        scoreText.text = playerScore.ToString();
    }
}
