using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void PlayGame()
    {
        audioManager.PlaySFX(audioManager.buttonclick);
        SceneController.instance.LoadSavedScene();
    }

    public void NewGame()
    {
        DataActionManager.instance.NewGame();
        SceneController.instance.LoadFirstScene();
    }
    public void QuitGame()
    {
        audioManager.PlaySFX(audioManager.buttonclick);
        Application.Quit();
    }
}
