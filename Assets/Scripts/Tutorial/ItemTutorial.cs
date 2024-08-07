using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTutorial : MonoBehaviour
{
    [SerializeField] GameObject askButton;
    [SerializeField] GameObject tutorialPanel;

    private bool isTutorialOn;

    private void Start()
    {
        isTutorialOn = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isTutorialOn)
        {
            Time.timeScale = 0f;
            if (Input.anyKeyDown)
            {
                Time.timeScale = 1f;
                tutorialPanel.SetActive(false);
                isTutorialOn = false;
            }
        }
    }

    public void handleAskButtonClick()
    {

        if (!isTutorialOn)
        {
            tutorialPanel.SetActive(true);
            isTutorialOn = true;
        }
    }
}
