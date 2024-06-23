using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject restartConfirm;

    // Start is called before the first frame update
    private static bool isPaused = false;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
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
        SceneController.instance.LoadMainMenu();
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void Resume()
    {

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ConfirmRestart()
    {
        restartConfirm.SetActive(true);
    }

    public void CancelRestart()
    {
        restartConfirm.SetActive(false);
    }

    public void Restart()
    {
        restartConfirm.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        SceneController.instance.LoadFirstScene();
    }

    public void Settings()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 0f; //pause game speed
        SettingsMenuManager.instance.OpenSettingsMenu();
    }
}
