using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class OrbitPath : MonoBehaviour
{
    public int vertexCount;    // adjust this for smoothness of circle
    public float lineWidth;
    public float radius;
    public Vector3 origin;
    public float angle1;
    public float angle2;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SetupCircle();
    }

    private void SetupCircle()
    {
        lineRenderer.widthMultiplier = lineWidth;

        float deltaTheta = (360 - angle2 + angle1) / vertexCount;
        float theta = angle2;

        lineRenderer.positionCount = vertexCount;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 pos = new Vector3(origin[0] + radius * Mathf.Cos(theta * Mathf.Deg2Rad),
                origin[1] + radius * Mathf.Sin(theta * Mathf.Deg2Rad), -1.0f);
            lineRenderer.SetPosition(i, pos);
            if (theta + deltaTheta > 360) // look at potentially making this more accurate
            {
                theta = 0;
            }
            theta += deltaTheta;
        }
    }

    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        float deltaTheta = (2f * Mathf.PI) / vertexCount;
        float theta = 0f;

        Vector3 oldPos = Vector3.zero;
        for (int i = 0; i < vertexCount + 1; i++)
        {
            Vector3 pos = new Vector3(origin[0] + radius * Mathf.Cos(theta),
                    origin[1] + radius * Mathf.Sin(theta), 0f);
            Gizmos.DrawLine(oldPos, pos);
            oldPos = pos;
            theta += deltaTheta;
        }
    }
#endif
}
