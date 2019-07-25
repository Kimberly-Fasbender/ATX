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

    float levelResetDelay = 1.5f;
    int startScreenIndex = 0;

    void Awake()
    {
        int numOfSessions = FindObjectsOfType<GameSession>().Length;

        if (numOfSessions > 1)
        {
            Debug.Log($"I'm destroying a Game Object with livesText {livesText.text}");
            Destroy(gameObject);
        }
        else 
        {
            Debug.Log($"I am SAVING a gameSession with livesText {livesText.text}");
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        Debug.Log($"Lives: {livesText.text}");
        Debug.Log($"Score: {scoreText.text}");
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
            Debug.Log("WOAH NELLY, WHY ARE WE IN HERE?!");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
        Debug.Log("I've set the test for the lives score!");
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
        
        StartCoroutine(SceneLoadDelay(currentSceneIndex));
        livesText.text = playerLives.ToString();
    }

    private void ResetGameSession()
    {
        StartCoroutine(SceneLoadDelay(startScreenIndex));
    }

    IEnumerator SceneLoadDelay(int scene)
    {
        yield return new WaitForSeconds(levelResetDelay);
        SceneManager.LoadScene(scene);

        if (scene == 0)
        {
            Debug.Log("AGAIN, DON'T BE IN HERE, BETTER NOT!!");
            Destroy(gameObject);
        }
    }

    public void AddPoints(int points)
    {
        Debug.Log("Text in GameSession");
        Debug.Log(scoreText.text);
        playerScore += points;
        scoreText.text = playerScore.ToString();
    }
}
