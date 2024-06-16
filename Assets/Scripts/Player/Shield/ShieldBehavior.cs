using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : MonoBehaviour
{


    //variable to reference to the shiled in game
    [SerializeField] private GameObject shield;
    //check if character is shielded or not
    private bool isShielded;
    private bool canActivateShield;



    void Start()
    {
        isShielded = false;
        shield.SetActive(false);
        canActivateShield = false; //cannot active shield when player does not collect item
    }

    void Update()
    {
        if (canActivateShield)
        {
            CheckShield();
        }

    }

    public void CheckShield() //check if shield is actived
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isShielded)
        {
            shield.SetActive(true);
            isShielded = true;
            Debug.Log("Shield Actived");
        }
    }

    void NoShield()
    {
        shield.SetActive(false);
        isShielded = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rocket"))
        {
            Debug.Log("missile collision detect");
            //turning of the shield
            Invoke("NoShield", 0f);
        }
    }

    // Method to enable shield functionality
    public void EnableShieldActivation()
    {
        canActivateShield = true;
    }
}
