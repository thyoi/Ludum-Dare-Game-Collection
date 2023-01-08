using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundBoom : MonoBehaviour
{
    public BackGroundSystem system;
    public float StepTime;
    public int MaxStep;
    public float StepTimeAcc;
    public float DownValueAcc;
    public float DownValue;
    public float DownTime;
    public float DownTimeAcc;
    public Vector2Int StartPoint;

    private float stepCount;
    private int curStep;
    // Start is called before the first frame update
    void Start()
    {
        curStep = -1;
        stepCount = 0;
    }
    public void Down()
    {
        if(curStep <= 0)
        {
            system.DownAt(StartPoint, DownValue, DownTime);
        }
        else
        {
            DownInLine(StartPoint + curStep * Vector2Int.up, new Vector2Int(1, -1),curStep);
            DownInLine(StartPoint + curStep * Vector2Int.right, new Vector2Int(-1, -1), curStep);
            DownInLine(StartPoint + curStep * Vector2Int.down, new Vector2Int(-1, 1), curStep);
            DownInLine(StartPoint + curStep * Vector2Int.left, new Vector2Int(1, 1), curStep);
        }
    }

    public void DownInLine(Vector2Int start, Vector2Int Dir,int n)
    {
        for(int i = 0; i < n; i++)
        {
            system.DownAt(start + Dir * i,DownValue,DownTime);
        }
    }


    // Update is called once per frame
    void Update()
    {
        stepCount += Time.deltaTime;
        if (stepCount > StepTime)
        {
            stepCount = 0;
            curStep++;
            if (curStep > MaxStep)
            {
                Destroy(gameObject);
            }
            Down();
            DownTime += DownTimeAcc;
            DownValue += DownValueAcc;
            StepTime += StepTimeAcc;



        }
    }
}
