using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }


    //Player infor to load new scene
    public Vector3 playerPosition;
    public float playerJumpSpeed;
    public Vector2 playerVelocity;

    private void Awake()
    {
        /*if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/
        Instance = this;

    }

    public void LoadNextScene(Vector3 currentPosition, Vector3 sceneSize)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if the next scene index is within the valid range
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            playerPosition = TransformPosition(currentPosition, sceneSize);
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void LoadBackScene(Vector3 currentPosition, Vector3 sceneSize)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int backSceneIndex = currentSceneIndex - 1;
        // Check if the next scene index is within the valid range
        if (backSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            playerPosition = TransformPosition(currentPosition, sceneSize);
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(backSceneIndex);
        }
    }

    public void LoadSpecificScene(int  sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private Vector3 TransformPosition(Vector3 currentPosition, Vector3 sceneSize)
    {
        // transformation: bottom-left to top-left
        return new Vector3(currentPosition.x, sceneSize.y - currentPosition.y, currentPosition.z);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = playerPosition;
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = playerVelocity;
            }
        }
    }
}
