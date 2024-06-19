using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingController : MonoBehaviour, IItemCollection
{

    [Header("Scripts Ref:")]
    public GrapplingHook grapplingGunScript;

    [Header("Component Ref:")]
    public GameObject grapplingGun;

    // Start is called before the first frame update
    void Start()
    {
        grapplingGunScript.activeGrappling = false;
    }
    public void activeItem()
    {
        grapplingGunScript.activeGrappling = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (grapplingGunScript.grappleRope)
            grapplingGunScript.StopGrappling();
    }

    
}
