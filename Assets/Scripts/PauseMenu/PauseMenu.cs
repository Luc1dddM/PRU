using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    // Start is called before the first frame update
    private static bool isPaused = false;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                audioManager.PlaySFX(audioManager.menuopen);
                Resume();
            }
            else
            {
                audioManager.PlaySFX(audioManager.menuopen);
                Pause();
            }
        }
    }
    public void Pause()
    {
        
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Home()
    {
        audioManager.PlaySFX(audioManager.buttonclick);
        SceneController.instance.LoadMainMenu();
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        audioManager.PlaySFX(audioManager.buttonclick);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Restart()
    {
        audioManager.PlaySFX(audioManager.buttonclick);
        Time.timeScale = 1f;
        isPaused = false;
        SceneController.instance.LoadFirstScene();
    }
}
