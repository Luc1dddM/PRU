﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject restartConfirm;

    // Start is called before the first frame update
    private static bool isPaused = false;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }




    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "MainMenu")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log(isPaused);
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
       
    }
    public void Pause()
    {
        audioManager.PlaySFX(audioManager.menuopen);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }


    public void Home()
    {
        audioManager.PlaySFX(audioManager.buttonclick);
        SceneController.instance.LoadMainMenu();
        pauseMenu.SetActive(false);
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


    public void ConfirmRestart()
    {
        audioManager.PlaySFX(audioManager.buttonclick);
        restartConfirm.SetActive(true);
    }

    public void CancelRestart()
    {
        audioManager.PlaySFX(audioManager.buttonclick);
        restartConfirm.SetActive(false);
    }

    public void Restart()
    {
        audioManager.PlaySFX(audioManager.buttonclick);
        restartConfirm.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        SceneController.instance.NewGame();
    }

    public void Settings()
    {
        audioManager.PlaySFX(audioManager.buttonclick);
        pauseMenu.SetActive(false);
        Time.timeScale = 0f; //pause game speed
        SettingsMenuManager.instance.OpenSettingsMenu();
    }
}
