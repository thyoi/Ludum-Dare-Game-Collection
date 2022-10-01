using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithCurveAnimation2D : MonoBehaviour
{
    public bool DirectMode = true;
    public int timeLayer = 3;
    public float animationTime = 1;
    public float delay;
    public AnimationCurve curve;
    public float initSize = 1;
    public float endSize;
    public AnimationStatic.endType endType;

    private float counter;
    private BoolCountroler stop;
    private BoolCountroler pause;
    private Transform myTransform;
    private Vector3Countroler registerScale;
    private float dt;
    private bool onBack;
    private AnimationCallBack callBack;
    public bool Pause
    {
        get { return pause.DefaultValue; }
        set { pause.DefaultValue = value; }
    }

    public bool Finish
    {
        get { return stop.DefaultValue; }
        set { stop.DefaultValue = value; }
    }

    public float InitSize
    {
        get { return initSize; }
        set
        {
            initSize = value;
            UpdateScale();
        }
    }

    public float EndSize
    {
        get { return endSize; }
        set
        {
            endSize = value;
            UpdateScale();
        }
    }
    void Awake()
    {
        stop = new BoolCountroler(false);
        pause = new BoolCountroler(true);
        pause.OperationIndex = ValueCountrolerManager.OprationName.bool_or;
        stop.AddFactor(pause);
        if (DirectMode)
        {
            myTransform = transform;

        }
        else
        {
            registerScale = transform.GetComponent<TransformCountroler>().RegisterScale();
        }
    }

    void Update()
    {
        dt = TimeManager.DeltaTime(timeLayer);
        if (dt > 0 && !stop.Value)
        {
            if (delay > 0)
            {
                delay -= dt;
            }
            else
            {
                UpdateCounter();
                UpdateScale();
            }
        }
    }


    public void UpdateCounter()
    {
        if (onBack)
        {
            if (counter > 0)
            {
                counter -= dt;
                if (counter <= 0)
                {
                    counter = 0;
                    if(endType == AnimationStatic.endType.backLoop)
                    {
                        onBack = false;
                    }
                    else
                    {
                        onBack = false;
                        Finish = true;
                        CallBackOnce();
                    }
                }
            }
        }
        else
        {
            if (counter < animationTime)
            {
                counter += dt;
                if (counter >= animationTime)
                {
                    counter = animationTime;
                    if(endType == AnimationStatic.endType.stop)
                    {
                        Finish = true;
                        CallBackOnce();
                    }
                    else if(endType == AnimationStatic.endType.loop)
                    {
                        counter = 0;
                    }
                    else if(endType == AnimationStatic.endType.back)
                    {
                        onBack = true;
                    }
                    else if(endType == AnimationStatic.endType.backLoop)
                    {
                        onBack = true;
                    }
                }
            }
        }
    }

    public void StartAnimation(AnimationCallBack callBack =null)
    {
        this.callBack = callBack;
        Finish = false;
        Pause = false;
        counter = 0;
        onBack = false;
    }

    private void UpdateScale()
    {
        SetScale(UF.Lerp(initSize, endSize, curve.Evaluate(counter / animationTime)));
    }

    private void SetScale(float size)
    {
        if (DirectMode)
        {
            myTransform.localScale = new Vector3(size, size, 1);
        }
        else
        {
            registerScale.DefaultValue = new Vector3(size, size, 1);
        }
    }

    private void CallBackOnce()
    {
        if (callBack != null)
        {
            callBack();
            callBack = null;
        }
    }
}
