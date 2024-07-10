using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialIceMap : MonoBehaviour
{
    [SerializeField] private GameObject tutorialImage; // Drag and drop your tutorial image GameObject here

    private bool tutorialActive = true;

    void Start()
    {
        // Hiển thị ảnh tutorial khi bắt đầu map
        tutorialImage.SetActive(true);
        // Dừng thời gian của game
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (tutorialActive && Input.anyKeyDown)
        {
            // Tắt ảnh tutorial và bắt đầu thời gian của game khi bấm bất kỳ phím nào
            tutorialImage.SetActive(false);
            Time.timeScale = 1f;
            tutorialActive = false;
        }
    }
}
