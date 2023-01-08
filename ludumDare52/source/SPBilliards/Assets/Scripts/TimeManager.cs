using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager globalManager;
    public static float DT()
    {
        return globalManager.deltaTime();
    }
    public static void SetScale(float a)
    {
        globalManager.SetTimeScale(a);
    }


    private float timeScale;


    public TimeManager()
    {
        timeScale = 1;
        TimeManager.globalManager = this;
    }

    public void SetTimeScale(float s)
    {
        Time.timeScale = s;
        timeScale = s;
    }

    public void ReSetTimeScale()
    {
        SetTimeScale(1);
    }

    public float deltaTime()
    {
        return Time.deltaTime / timeScale;
    }


}

