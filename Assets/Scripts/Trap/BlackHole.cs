using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackHole : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public GrapplingHook grapplingGun;
    public CameraFollow cameraFollow;

    [Header("Object Ref:")]
    public GameObject whiteHole;

    [Header("BlackHole Setting:")]
    public float range;
    public float pullForce = 4f;

    private float distanceBtPlayer;
    private GameObject player;
    private GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //Calculate distance between player and white hole
        distanceBtPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (distanceBtPlayer <= range) //In area of influence
        {
            //Stop grappling if player is using grappling rope
            if (grapplingGun.grappleRope)
            {
                grapplingGun.StopGrappling();
            }
            //Pull Player
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            Vector2 direction = transform.position - player.transform.position;
            direction.Normalize();
            rb.AddForce(pullForce * direction, ForceMode2D.Force);
        }
    }

    // Teleport Player from black hole to white hole
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null && collision.CompareTag("Player"))
        {
            StartCoroutine(Teleport());
        }
    }

    //Lock camera in whitehole before teleport
    IEnumerator Teleport()
    {
        //Lock camera moving and set position in white hole
        cameraFollow.followPlayer = false;
        camera.transform.position = new Vector3(camera.transform.position.x, whiteHole.transform.position.y, camera.transform.position.z);

        yield return new WaitForSeconds(1); // lock camera for 1s
        //Teleport Player
        cameraFollow.followPlayer = true;
        player.transform.position = whiteHole.transform.position;

    }
}
