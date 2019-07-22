using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0; //TODO: REMOVE - for debugging only
    [SerializeField] public Text livesText;
    [SerializeField] public Text scoreText;

    float levelResetDelay = 1f;
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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu")
        {
            Destroy(gameObject);
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
        playerLives -= 1;
        
        StartCoroutine(RestartLevel());
        livesText.text = playerLives.ToString();
    }

    IEnumerator RestartLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        yield return new WaitForSeconds(levelResetDelay);
        SceneManager.LoadScene(currentSceneIndex);
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
