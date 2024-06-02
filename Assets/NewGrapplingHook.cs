using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGrapplingHook : MonoBehaviour
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
    public DistanceJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)][SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistance = 20;

    [Header("Grappling State:")]
    public bool canGrappling = false;


    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    /* [Header("Launching:")]
     [SerializeField] private bool launchToPoint = true;
     [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
     [SerializeField] private float launchSpeed = 1;*/

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    private void Start()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetGrapplePoint();
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            if (grappleRope.enabled)
            {
                RotateGun(grapplePoint, false);
            }
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopGrappling();
        }
        else
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            RotateGun(mousePos, true);
        }
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        if (!playerMoving.facingRight)
        {
            distanceVector = -distanceVector;
        }

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;

        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetGrapplePoint()
    {
        Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            if (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
            {
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistance || !hasMaxDistance)
                {
                    grapplePoint = _hit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    canGrappling = true;
                    grappleRope.enabled = true;
                }
                else
                {
                    grapplePoint = (Vector2)gunPivot.position + distanceVector.normalized * maxDistance;
                    grappleRope.enabled = true;
                    canGrappling = false;
                }
            }
        }
        else
        {
            grapplePoint = (Vector2)gunPivot.position + distanceVector.normalized * maxDistance;
            grappleRope.enabled = true;
            canGrappling = false;
        }
    }

    public void Grapple()
    {
        m_springJoint2D.autoConfigureDistance = false;

        if (autoConfigureDistance)
        {
            m_springJoint2D.autoConfigureDistance = true;
        }

        m_springJoint2D.connectedAnchor = grapplePoint;
        m_springJoint2D.enabled = true;
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name); // Debug log to check collision
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        m_rigidbody.gravityScale = 1;
    }*/

    public void StopGrappling()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
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
