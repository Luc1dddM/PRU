using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateController : MonoBehaviour
{

    AudioManager audioManager;
    

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            audioManager.PlaySFX(audioManager.changemapgate);
            CoinController.instance.ResetCoin();
            SceneController.instance.LoadNextScene();
        }

    }
}
