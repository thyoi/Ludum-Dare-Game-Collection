using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimeArrFloat
{
    public float cur;
    public float end;
    public float speed;
    public float delay;
    public bool start;
    public void update(float dt)
    {
        if (start)
        {
            if (delay <= 0)
            {
                nextCur(dt);
                if (cur == end)
                {
                    start = false;
                }
            }
            else
            {
                delay -= dt;
            }
        }
    }

    public void nextCur(float dt)
    {
        cur = Mathf.Lerp(cur, end, dt * speed);
    }

}


[System.Serializable]
public class AnimeArrVector2
{
    public Vector2 cur = new Vector2(0,0);
    public Vector2 end = new Vector2(0, 0);
    public float speed;
    public float delay = 0;
    public bool start = false;
    public void update(float dt)
    {
        if (start)
        {
            if (delay <= 0)
            {
                nextCur(dt);
                if(cur == end)
                {
                    start = false;
                }
            }
            else
            {
                delay -= dt;
            }
        }
    }

    public void nextCur(float dt)
    {
        cur = Vector2.Lerp(cur, end, dt * speed);
        if (UF.isCloseEnough(cur, end))
        {
            cur = end;
        }
    }

}
