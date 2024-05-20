using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }
    public string test = "";

    public Vector3 playerPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName, Vector3 currentPosition, Vector3 sceneSize)
    {
        playerPosition = TransformPosition(currentPosition, sceneSize);
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
    }

    public void LoadBackScene(Vector3 currentPosition, Vector3 sceneSize)
    {
        playerPosition = TransformPosition(currentPosition, sceneSize);
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(test);
    }

    private Vector3 TransformPosition(Vector3 currentPosition, Vector3 sceneSize)
    {
        // Example transformation: bottom-left to top-left
        return new Vector3(currentPosition.x, sceneSize.y - currentPosition.y, currentPosition.z);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = playerPosition;
        }
    }
}
