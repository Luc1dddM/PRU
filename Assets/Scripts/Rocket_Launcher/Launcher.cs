using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    // Variable declaration start-------------------------------------------------------------------
    public GameObject missile;
    public GameObject target;
    public Transform missileInitiatePos;

    private float timer; //control missile frequency spawn
    // Variable declaration end-------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);
        Debug.Log(distance);
        if (distance < 10)
        {
            timer += Time.deltaTime;
            if (timer > 2)//after 5 secs 
            {
                timer = 0;//reset time
                Shoot();
            }
        }
    }


    void Shoot()
    {
        Instantiate(missile, missileInitiatePos.position, Quaternion.identity);
    }
}
