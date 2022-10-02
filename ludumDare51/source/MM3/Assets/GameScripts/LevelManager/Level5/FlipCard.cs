using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCard : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform c1;
    public Transform c2;
    public float curDrgee;
    public float endDrgee;
    public float Speed;
    public int timeLayer = 3;
    public float delay;
    public AnimationCallBack callBack;
    public Boomable boom;
    public int cardType;
    public string initSound;
    public string endSound;

    private float dt;
    private bool start;




    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            dt = TimeManager.DeltaTime(timeLayer);
            if (dt > 0)
            {
                if (delay > 0)
                {
                    delay -= dt;
                    if (delay <= 0)
                    {
                        SoundManager.PlaySound(initSound);
                    }
                }
                else
                {
                    if (curDrgee > endDrgee)
                    {
                        curDrgee -= Speed * dt;
                        if (curDrgee <= endDrgee)
                        {
                            curDrgee = endDrgee;
                            start = false;
                            CallBackOnce();
                        }
                    }
                    else
                    {
                        curDrgee += Speed * dt;
                        if (curDrgee >= endDrgee)
                        {
                            curDrgee = endDrgee;
                            start = false;
                            CallBackOnce();
                        }
                    }
                    SetDergee(curDrgee);
                }
            }
        }
    }

    public void StartRotate()
    {
        start = true;
    }
    void SetDergee(float d)
    {
        if (d >= 0 && d < 90)
        {
            SetCardDrgee(c1, d);
            SetCardDrgee(c2, 90);
        }
        else if(d>90 && d < 180)
        {
            SetCardDrgee(c1, 90);
            SetCardDrgee(c2, 180-d);
        }
    }

    void SetCardDrgee(Transform c, float d)
    {
        if (c != null)
        {
            c.rotation = Quaternion.AngleAxis(d, new Vector3(0, 1, 0));
        }
    }

    public void CallBackOnce()
    {
        SoundManager.PlaySound(endSound);
        if (callBack != null)
        {
            callBack();
            callBack = null;
        }
    }

}
