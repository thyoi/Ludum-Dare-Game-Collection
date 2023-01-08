using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullteTimeManager : MonoBehaviour
{
    public float speed;
    public float minStep;


    private float curScale;
    private float endScale;
    private bool active;

    private void UpdateScale()
    {
        curScale = UF.Lerp(curScale, endScale, speed, TimeManager.DT(), minStep);
        TimeManager.SetScale(curScale);
        if(curScale == endScale)
        {
            active = false;
        }

    }

    public void BulletTimeScale(float s)
    {
        endScale = s;
        active = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        curScale = 1;
        endScale = 1;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            UpdateScale();
        }
    }
}
