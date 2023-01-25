using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessBar : MonoBehaviour
{
    public Transform Mask;
    public float[] yList;
    public int cur;
    public Transform[] ParticalPosition;
    public Color[] ParticalColor;
    // Start is called before the first frame update

    public void NextStage()
    {
        cur++;
        if (cur > yList.Length - 1)
        {
            cur = yList.Length - 1;
        }

        Vector2 tem = Mask.localPosition;
        tem.y = yList[cur];
        Mask.localPosition = tem;
        particalManager.GlobalManager.BoomParticalBust(20, ParticalPosition[cur - 1].position, ParticalColor[cur - 1], 1, true);
        particalManager.GlobalManager.CreateRoundPartical(ParticalPosition[cur - 1].position, 1, ParticalColor[cur - 1], true);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
