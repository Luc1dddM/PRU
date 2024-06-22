using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneController.instance.LoadSavedScene();
    }

    public void NewGame()
    {
        DataActionManager.instance.NewGame();
        SceneController.instance.LoadFirstScene();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
