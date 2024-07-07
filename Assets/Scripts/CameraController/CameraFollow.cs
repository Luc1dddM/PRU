using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    public bool followPlayer = true;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (followPlayer)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp
            (transform.position.y, player.gameObject.transform.position.y + 5f, 0.02f), -10);
        }
    }
}
