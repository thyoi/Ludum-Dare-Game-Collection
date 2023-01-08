using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wiggle
{
    public AnimationCurve Curve;
    public float Time;
    public float Range;

    public SingleWiggle[] Wiggles;
    private int wiggleCount = 0;

    public void Update(float dt)
    {
        for(int i = 0; i < 10; i++)
        {
            Wiggles[i].Update(dt);

        }
    }
    public Vector2 GetValue()
    {
        Vector2 res = Vector2.zero;
        for(int i= 0; i < 10; i++)
        {
            res += Wiggles[i].GetValue(Curve);
        }
        return res;
    }

    public void RandomWiggle()
    {
        float angle = Random.Range(0, Mathf.PI * 2);

        Wiggles[wiggleCount].Dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
        Wiggles[wiggleCount].StartWiggle();
        wiggleCount++;
        if (wiggleCount >= 10)
        {
            wiggleCount = 0;
        }
    }

    public void Init()
    {
        for (int i = 0; i < 10; i++)
        {
            Wiggles[i] = new SingleWiggle();
            Wiggles[i].Time = Time;
            Wiggles[i].Range = Range;
        }
    }


}

[System.Serializable]
public class SingleWiggle
{

    public float Time;
    public float Range;
    public Vector2 Dir;

    private float count = 0;
    private bool active = false;


    public void StartWiggle()
    {
        count = 0;
        active = true;
    }
    public void Update(float dt)
    {
        if (active)
        {
            count += dt;
            if (count >= Time)
            {
                active = false;
                count = Time;
            }
        }
    }
    public Vector2 GetValue(AnimationCurve Curve)
    {
        return Curve.Evaluate(count / Time) * Range * Dir;
    }
}
