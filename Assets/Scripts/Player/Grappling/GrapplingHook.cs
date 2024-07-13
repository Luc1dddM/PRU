using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public Tutorial_GrapplingRope grappleRope;
    public Move playerMoving;


    [Header("Object Ref:")]
    public GameObject Player;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref:")]
    public DistanceJoint2D m_DistanceJoint;
    public Rigidbody2D m_rigidbody;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistance = 20;

    [Header("Grappling State:")]
    public bool canGrappling = false;
    public bool activeGrappling;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;
    
    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        grappleRope.enabled = false;
        m_DistanceJoint.enabled = false;
    }

    private void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && activeGrappling)
            {
                SetGrapplePoint();
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0) && canGrappling)
            {
                StopGrappling();
            }
        }
    }

    void SetGrapplePoint()
    {
        Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            if (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
            {
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistance || !hasMaxDistance)
                {
                    audioManager.PlaySFX(audioManager.grappling);
                    grapplePoint = _hit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)firePoint.position;
                    canGrappling = true;
                    grappleRope.enabled = true;
                }
            }
        }
    }

    public void Grapple()
    {
        m_DistanceJoint.autoConfigureDistance = true;
        m_DistanceJoint.connectedAnchor = grapplePoint;
        m_DistanceJoint.enabled = true;
    }

    public void StopGrappling()
    {
        audioManager.PlaySFX(audioManager.grappling);
        grappleRope.enabled = false;
        m_DistanceJoint.enabled = false;
        canGrappling = false;
    }
    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistance);
        }
    }

}
