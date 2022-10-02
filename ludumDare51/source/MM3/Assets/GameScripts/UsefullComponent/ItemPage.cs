using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPage : MonoBehaviour
{
    public FadeInAnimation[] items;
    public PositionInPathWithCurveAnimation2D positionAnimation;
    public int timeLayer = 3;
    public float delay;
    public float display = 2;

    private bool start;
    private float dt;
    public void Init()
    {
        foreach(FadeInAnimation i in items)
        {
            i.SetApha(0);
        }
        transform.position = new Vector3(50, 50, 0);
        start = true;
    }

    public void InPage()
    {
        positionAnimation.endPosition = Vector3.zero;
        Vector3 tem = new Vector3(2, 0, 0);
        positionAnimation.initPosition = tem;
        foreach(FadeInAnimation f in items)
        {
            f.outMode = false;
            f.StartAnimation();
        }
        positionAnimation.StartAnimation();
        SoundManager.PlaySound("tip");
    }

    public void OutPage()
    {
        positionAnimation.initPosition= Vector3.zero;
        Vector3 tem = new Vector3(-2, 0, 0);
        positionAnimation.endPosition = tem;
        foreach (FadeInAnimation f in items)
        {
            f.outMode = true;
            f.StartAnimation();
        }
        positionAnimation.StartAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        dt = TimeManager.DeltaTime(timeLayer);
        if (dt > 0 && start)
        {
            if (delay > 0)
            {
                delay -= dt;
                if (delay <= 0)
                {
                    InPage();
                }
            }
            else if (display > 0)
            {
                display -= dt;
                if (display < 0)
                {
                    OutPage();
                }
            }
        }
    }
}
