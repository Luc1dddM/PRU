using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Lerp
            (transform.position.y, player.gameObject.transform.position.y + 5, 0.005f), -10);
    }
}
