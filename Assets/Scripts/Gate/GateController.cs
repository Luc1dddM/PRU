using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateController : MonoBehaviour
{

    public Text doorText;


    private void Awake()
    {
        doorText.text = "";

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            SceneController.instance.LoadNextScene();
        }

        if (collision.CompareTag("Player"))
        {
            StartCoroutine(textTiming());
        }
    }

    private IEnumerator textTiming()
    {
        if (CoinController.instance.coinCout == 3)
        {
            doorText.text = "Press E to activate the Portal"; // appear text

        }
        else
        {
            doorText.text = "You do not have enough coin!!!"; // appear text

        }
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);
       
        doorText.text = "";
    }
}
