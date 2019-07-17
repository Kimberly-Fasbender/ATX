using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenPersistence : MonoBehaviour
{
    int startingSceneIndex;

    void Awake()
    {
        int numOfScenePersistence = FindObjectsOfType<ScreenPersistence>().Length;
        if (numOfScenePersistence > 1)
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
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (startingSceneIndex != currentSceneIndex)
        {
            Destroy(gameObject);
        }
    }
}
