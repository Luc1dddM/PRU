using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance { get; private set; }
    [SerializeField] Animator transAimt;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        instance = this;

    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadNewScene());
    } 


    public void LoadMainMenu() {
        SceneManager.LoadSceneAsync(0);    
    }

    public void LoadFirstScene()
    {
        SceneManager.LoadSceneAsync(1);

    }

    IEnumerator LoadNewScene()
    {

        transAimt.SetTrigger("End");
        yield return new WaitForSeconds(1);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        Debug.Log(nextSceneIndex < SceneManager.sceneCountInBuildSettings);
        // Check if the next scene index is within the valid range
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadSceneAsync(nextSceneIndex);
        }
        transAimt.SetTrigger("Start");

    }
}
