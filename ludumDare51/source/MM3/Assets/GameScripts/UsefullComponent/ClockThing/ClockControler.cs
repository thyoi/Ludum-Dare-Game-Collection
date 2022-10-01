using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockControler : MonoBehaviour
{
    public GameObject Mask;
    public AnimationCurve curve;
    public float initTime;
    public int maskNum = 60;
    public bool init;
    public bool start;
    public int timeLayer = 3;
    public float CountDownTime;
    public FrameAnimation num;
    public float Delay = 3;

    public AnimationCallBack callback;
    private Transform[] MaskList;
    private Vector3[] MaskPositions;
    private float initCount;
    private float startCount;
    private float dt;
    private int lastNum;

    public void initMask(int n)
    {
        MaskList = new Transform[n];
        MaskPositions = new Vector3[n];
        for(int i = 0; i < n; i++)
        {
            MaskList[i] = Instantiate(Mask, transform).transform;
            MaskList[i].Rotate(0, 0, -360f / n*i);
            MaskPositions[i] = new Vector3(Mathf.Sin(2 * Mathf.PI / n * i) * 0.35f, Mathf.Cos(2 * Mathf.PI / n * i) * 0.35f, 0);
            MaskList[i].localPosition = MaskPositions[i];
        }
    }

    public void SetNum(int n)
    {
        if (lastNum != n)
        {
            lastNum = n;
            num.SetSprites(CharaterManager.GetNumSprites(n));
        }
        
    }

    public void UpdateInit()
    {
        if (initCount < initTime)
        {
            initCount += dt;
            if (initCount >= initTime)
            {
                init = false;
                initCount = initTime;
                StartCountdown();
            }
            SetMasks(curve.Evaluate(initCount / initTime));
        }
    }

    public void UpdateStart()
    {
        if (startCount < CountDownTime)
        {
            startCount += dt;
            if (startCount >= CountDownTime)
            {
                startCount = CountDownTime;
                start = false;
                Destroy(num.transform.gameObject);
                callback();
            }
            SetNum(UF.ConvertFloatToInt(10 - 10*startCount / CountDownTime));
            SetMasks(1 - startCount / CountDownTime);
        }
    }

    private void setMask(int n,bool on)
    {
        if (on)
        {
            MaskList[n].localPosition = MaskPositions[n];
        }
        else
        {
            
            MaskList[n].localPosition = Vector3.zero;
        }
    }

    private void SetMasks(float f)
    {
        for (int i = 0; i < maskNum; i++)
        {
            if (maskNum - i > f * maskNum)
            {
                setMask(i, true);
            }
            else
            {
                setMask(i, false);
            }
        }
    }

    public void Init()
    {
        init = true;
    }

    public void StartCountdown()
    {
        start = true;
        num.transform.GetComponent<ScaleWithCurveAnimation2D>().StartAnimation();
    }

    public void Awake()
    {
        initMask(maskNum);
    }

    public void Update()
    {
        dt = TimeManager.DeltaTime(timeLayer);
        if (dt > 0)
        {
            if (Delay > 0)
            {
                Delay -= dt;
            }
            else
            {
                if (init)
                {
                    UpdateInit();
                }
                if (start)
                {
                    UpdateStart();
                }
            }
        }
    }
}
