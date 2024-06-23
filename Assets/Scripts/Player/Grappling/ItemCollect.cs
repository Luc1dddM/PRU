using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    public IItemCollection actionable;
    public MonoBehaviour actionScript;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void Start()
    {
        actionable = actionScript as IItemCollection;
        if (actionable == null)
        {
            Debug.LogError("The assigned script does not implement IActionable");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            /*collision.GetComponent<GrapplingController>().ActiveGrapplingGun();
            Destroy(gameObject); */
            audioManager.PlaySFX(audioManager.collectitem);
            actionable.activeItem();
            Destroy(gameObject);
        }
    }
}
