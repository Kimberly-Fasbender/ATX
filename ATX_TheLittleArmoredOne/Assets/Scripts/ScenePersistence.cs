using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersistence : MonoBehaviour
{
    int startingSceneIndex;
    int activeScene;

    void Awake()
    {

        ScenePersistence[] ScenePersistences = FindObjectsOfType<ScenePersistence>();
        activeScene = SceneManager.GetActiveScene().buildIndex;
        if (ScenePersistences.Length > 1)
        {
            if (ScenePersistences[0].activeScene == ScenePersistences[1].activeScene)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    
        // int numOfScenePersistence = FindObjectsOfType<ScenePersistence>().Length;

        
        // if (numOfScenePersistence > 1)
        // {
        //     Destroy(gameObject);
        // }
        // else
        // {
        //     DontDestroyOnLoad(gameObject);
        // }

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
