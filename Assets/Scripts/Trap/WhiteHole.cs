using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteHole : MonoBehaviour
{
  
    [Header("Game Object Ref:")]
    [SerializeField] private GameObject player;


    [Header("White Hole Setting:")]
    [SerializeField] private float intensity;
    [SerializeField] private int range;


    private float distanceBtPlayer;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
     

    }

    // Start is called before the first frame update
    private void Update()
    {

        //Calculate distance between player and white hole
        distanceBtPlayer = Vector2.Distance(player.transform.position, transform.position); ;
        if(distanceBtPlayer <= range) //In area of influence
        {
            //Push Player
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            rb.AddForce(direction * intensity, ForceMode2D.Force);
        }
        
    }
}
