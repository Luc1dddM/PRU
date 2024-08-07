using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] private Button continueButton;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        if (!DataActionManager.instance.HasSavedGame())
        {
            continueButton.gameObject.SetActive(false);
        }
    }
    public void PlayGame()
    {
        audioManager.PlaySFX(audioManager.buttonclick);
        SceneController.instance.LoadSavedScene();
    }

    public void NewGame()
    {
        audioManager.PlaySFX(audioManager.buttonclick);
        DataActionManager.instance.NewGame();
        SceneController.instance.LoadFirstScene();
    }

    public void Settings()
    {
        audioManager.PlaySFX(audioManager.buttonclick);
        Time.timeScale = 0f; //pause game speed
        SettingsMenuManager.instance.OpenSettingsMenu();
    }

    public void VideoGame()
    {
        audioManager.PlaySFX(audioManager.buttonclick);
        SceneManager.LoadScene("IntroGame");
    }

    public void QuitGame()
    {
        audioManager.PlaySFX(audioManager.buttonclick);
        Application.Quit();
    }
}

  
