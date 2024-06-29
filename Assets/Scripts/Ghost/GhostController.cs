using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public Transform[] patrolPoint;
    public float moveSpeed = 4f;
    public int patroDestination;
    public EdgeCollider2D stand;
    public BoxCollider2D trigger;

    private SpriteRenderer spriteRenderer;
    private bool isStand;

    // Start is called before the first frame update
    void Start()
    {
        stand = GetComponent<EdgeCollider2D>();
        trigger = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stand.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (patroDestination == 0 && patrolPoint.Count() > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoint[0].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoint[0].position) < .2f)
            {
                patroDestination = 1;
            }
        }
        if (patroDestination == 1 && patrolPoint.Count() > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoint[1].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoint[1].position) < .2f)
            {
                patroDestination = 0;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(StandTiming());


        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Standing");
            StartCoroutine(GhostTiming());

        }
    }

    private IEnumerator StandTiming()
    {
        isStand = false;
        stand.enabled = true; // appear a edge collider to stand
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
        if (!isStand)
        {
            stand.enabled = false;// disappear a edge collider to stand
            Debug.Log("Stand Appear");

        }
    }

    private IEnumerator GhostTiming()
    {
        var tmp = spriteRenderer.color;
        isStand = true;
        //Execute when the function is first called.
        stand.enabled = true; // appear a edge collider to stand
        trigger.enabled = false;//disappear the trigger 
        Debug.Log(stand.enabled);
        //Use loop to fadeout the ghost for 5 seconds.
        for (float f = 5; f >=-0.05f; f -= 0.05f)
        {
            Color c = spriteRenderer.color;
            c.a = f;
            spriteRenderer.color = c;
            yield return new WaitForSeconds(0.05f);

        }

        //After we have waited 5 seconds execute this.
        stand.enabled = false;// disappear a edge collider to stand
        spriteRenderer.color = Color.clear;// make the ghost become clear
        isStand = false;

        Debug.Log(stand.enabled);

        //After we have waited 5 seconds execute this.
        yield return new WaitForSeconds(5);
        spriteRenderer.color = tmp;// make the ghost become the old color
        trigger.enabled = true;//appear the trigger 
    }
}
