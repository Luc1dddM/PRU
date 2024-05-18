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
    private bool isGrappling = false; // Kiểm soát trạng thái đu dây

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
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(
                origin: mouseWorldPosition,
                direction: Vector2.zero,
                distance: Mathf.Infinity,
                layerMask: grappleLayer
            );

            if (hit.collider != null)
            {
                grapplePosition = hit.point;
                grapplePosition.z = 0f;

                // Bắt đầu Coroutine để dây xuất hiện dần
                StartCoroutine(ShootRope());
            }
        }

        if (rope.enabled)
        {
            rope.SetPosition(1, transform.position);
        }
    }

    private IEnumerator ShootRope()
    {
        isGrappling = true;
        float distance = Vector3.Distance(transform.position, grapplePosition);
        float time = distance / ropeShootSpeed;
        float elapsedTime = 0f;

        rope.SetPosition(0, grapplePosition);
        rope.SetPosition(1, transform.position);
        rope.enabled = true;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / time;

            // Từ từ di chuyển điểm đầu của dây từ vị trí của người chơi đến vị trí grapple
            Vector3 currentRopePosition = Vector3.Lerp(transform.position, grapplePosition, t);
            rope.SetPosition(1, transform.position); // Luôn đặt điểm 1 ở vị trí của nhân vật
            rope.SetPosition(0, currentRopePosition); // Di chuyển điểm 0 từ nhân vật đến vật thể

            yield return null;
        }

        // Đảm bảo dây được đặt đúng vị trí khi Coroutine kết thúc
        rope.SetPosition(0, grapplePosition);


        // Kích hoạt DistanceJoint2D để nhân vật có thể đu lên
        distanceJoint.enabled = true;
        distanceJoint.connectedAnchor = grapplePosition;
        distanceJoint.distance = grappleLength;

        isGrappling = false; // Hoàn tất đu dây
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        distanceJoint.enabled = false;
        rope.enabled = false;
    }
}
