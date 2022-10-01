using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLineData : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int id;

    public bool Loop 
    {
        get
        {
            return lineRenderer.loop;
        }
        set
        {
            lineRenderer.loop = value;
        }
    }

    public void SetPositions(Vector3[] positions)
    {
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

}
