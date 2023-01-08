using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLine : MonoBehaviour
{
    public TimeLineItem[] items;
    public int itemCount;
    public float finalTime;
    public UF.AnimeCallback FinalCallback;


    private float timeCount;


    public void Init()
    {
        items = new TimeLineItem[100];
        itemCount = 0;
        timeCount = 0;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (items != null)
        {
            timeCount += Time.deltaTime;
            for (int i = 0; i < itemCount; i++)
            {
                if (timeCount > items[i].Time)
                {
                    items[i].CallOnce();
                }
            }

            if (timeCount > finalTime)
            {
                if (FinalCallback != null)
                {
                    FinalCallback();
                }
                Destroy(gameObject);
            }
        }
    }

    public void AddCallBack(float time, UF.AnimeCallback c)
    {
        items[itemCount] = new TimeLineItem(c,time);
        itemCount++;

    }
}




public class TimeLineItem
{
    public UF.AnimeCallback CallBack;
    public float Time;

    public TimeLineItem(UF.AnimeCallback c,float t)
    {
        CallBack = c;
        Time = t;
    }

    public void CallOnce()
    {
        if (CallBack != null)
        {
            CallBack();
            CallBack = null;
        }
    }

}
