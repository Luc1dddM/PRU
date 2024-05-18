using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{

    public float revealSpeed = 1f; // Tốc độ để đường kẻ xuất hiện hoàn toàn
    private LineRenderer lineRenderer;
    private Vector3[] points;
    private float revealProgress = 0f; // Tiến trình hiện tại của việc xuất hiện

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);
        lineRenderer.positionCount = 0; // Bắt đầu với đường kẻ ẩn
    }

    void Update()
    {
        if (revealProgress < 1f)
        {
            revealProgress += revealSpeed * Time.deltaTime; // Tăng tiến trình dựa trên thời gian và tốc độ

            int pointCount = Mathf.FloorToInt(revealProgress * points.Length);
            lineRenderer.positionCount = pointCount;

            for (int i = 0; i < pointCount; i++)
            {
                lineRenderer.SetPosition(i, points[i]);
            }

            if (pointCount == points.Length)
            {
                // Đảm bảo toàn bộ đường kẻ được vẽ
                lineRenderer.positionCount = points.Length;
                for (int i = 0; i < points.Length; i++)
                {
                    lineRenderer.SetPosition(i, points[i]);
                }
            }
        }
    }
}
