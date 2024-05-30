using System.Collections;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer rope;
    [SerializeField] private float ropeShootSpeed = 5f; // Tốc độ để dây xuất hiện

    private Vector3 grapplePosition;
    private DistanceJoint2D distanceJoint;
    public bool isGrappling = false;
    private bool isRenderLine = false;

    void Start()
    {
        distanceJoint = gameObject.GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
        rope.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGrappling)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);   //Get position of mouse click
            //Setup RayCast 
            RaycastHit2D hit = Physics2D.Raycast(
                origin: mouseWorldPosition,
                direction: Vector2.zero,
                distance: Mathf.Infinity,
                layerMask: grappleLayer
            );

            grapplePosition = hit.point;
            grapplePosition.z = 0f;

            //Call Shoot Rope func
            StartCoroutine(ShootRope(hit));
        }

        //Stop grappling
        if (Input.GetMouseButtonUp(0) && isGrappling)
        {
            distanceJoint.enabled = false;
            rope.enabled = false;
            isGrappling = false;
        }

        if (rope.enabled)
        {
            rope.SetPosition(1, transform.position);
        }
    }

    //Function to render rope and grapple
    private IEnumerator ShootRope(RaycastHit2D hit)
    {
        //Start render line
        isRenderLine = true;
        float distance = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        float time = distance / ropeShootSpeed;
        float elapsedTime = 0f;

        if(distance > grappleLength)
        {
            grapplePosition = CalculatePointC(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position , grappleLength);
        }
        else
        {
            grapplePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        while (elapsedTime < time)
        {
            rope.enabled = true;

            elapsedTime += Time.deltaTime;
            float t = elapsedTime / time;

            // Từ từ di chuyển điểm đầu của dây từ vị trí của người chơi đến vị trí grapple
            Vector3 currentRopePosition = Vector3.Lerp(transform.position, grapplePosition, t);
            rope.SetPosition(1, transform.position); // Luôn đặt điểm 1 ở vị trí của nhân vật
            rope.SetPosition(0, currentRopePosition); // Di chuyển điểm 0 từ nhân vật đến vật thể

            yield return null;
        }
        isRenderLine = false;

        if(hit.collider != null && (grappleLength >= distance))
        {
            isGrappling = true;

            //Make sure position of rope (where rope stick to object) is right after render
            rope.SetPosition(0, grapplePosition);

            // Enable Distance joint to grapple
            distanceJoint.enabled = true;
            distanceJoint.connectedAnchor = grapplePosition;
            distanceJoint.distance = grappleLength;
        }
        else
        {
            rope.enabled = false;
        }
    }


    //Stop grapple when hit an object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        distanceJoint.enabled = false;
        rope.enabled = false;
    }

    Vector3 CalculatePointC(Vector3 A, Vector3 B, float distance)
    {
        // Bước 1: Tính toán vector hướng từ B đến A
        Vector3 direction = A - B;

        // Bước 2: Chuẩn hóa vector hướng để có vector đơn vị
        Vector3 unitDirection = direction.normalized;

        // Bước 3: Nhân vector đơn vị với khoảng cách mong muốn
        Vector3 offset = unitDirection * distance;

        // Bước 4: Trừ vector này từ B để lấy điểm C
        Vector3 C = B + offset;

        return C;
    }

}
