using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateController : MonoBehaviour
{

    public Text doorText;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        doorText.text = "";
        doorText.enabled = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            CoinController.instance.coinCout = 0;
            SceneController.instance.LoadNextScene();
        }

        if (collision.CompareTag("Player"))
        {
            StartCoroutine(textTiming());
        }
    }

    private IEnumerator textTiming()
    {
        doorText.enabled = true;
        if (CoinController.instance.coinCout == 3)
        {
            doorText.text = "Press E to activate the Portal"; // appear text

        }
        else
        {
            doorText.text = "You do not have enough coin!!!"; // appear text

        }
        //yield on a new YieldInstruction that waits for 5 seconds.
        audioManager.PlaySFX(audioManager.changemapgate);
        yield return new WaitForSeconds(3);
        doorText.enabled = false;

        doorText.text = "";
    }
}
