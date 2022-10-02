using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionInPathWithCurveAnimation2D : MonoBehaviour
{
    public bool DirectMode = true;
    public int timeLayer = 3;
    public float animationTime = 1;
    public float delay;
    public AnimationCurve curve;
    public Vector2 initPosition;
    public Vector2 endPosition;
    public AnimationStatic.endType endType;
    public string initSound;

    private float counter;
    private BoolCountroler stop;
    private BoolCountroler pause;
    private Transform myTransform;
    private Vector3Countroler registerPosition;
    private float dt;
    private bool onBack;
    private AnimationCallBack callBack;

    public void SetCallBack(AnimationCallBack callback)
    {
        callBack = callback;
    }
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

    public Vector2 InitPosition 
    { 
        get { return initPosition; }
        set
        {
            initPosition = value;
            UpdatePosition();
        }
    }

    public Vector2 EndPosition
    {
        get { return endPosition; }
        set
        {
            endPosition = value;
            UpdatePosition();
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
            registerPosition = transform.GetComponent<TransformCountroler>().RegisterPosition();
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
                if (delay <= 0)
                {
                    SoundManager.PlaySound(initSound);
                }
            }
            else
            {
                UpdateCounter();
                UpdatePosition();
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
                    if (endType == AnimationStatic.endType.backLoop)
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
                    if (endType == AnimationStatic.endType.stop)
                    {
                        Finish = true;
                        CallBackOnce();
                    }
                    else if (endType == AnimationStatic.endType.loop)
                    {
                        counter = 0;
                    }
                    else if (endType == AnimationStatic.endType.back)
                    {
                        onBack = true;
                    }
                    else if (endType == AnimationStatic.endType.backLoop)
                    {
                        onBack = true;
                    }
                }
            }
        }
    }

    public void StartAnimation(AnimationCallBack callBack = null)
    {
        this.callBack = callBack;
        Finish = false;
        Pause = false;
        counter = 0;
        onBack = false;
    }

    private void UpdatePosition()
    {
        SetPosition(UF.Lerp(initPosition, endPosition, curve.Evaluate(counter / animationTime)));
    }

    protected void SetPosition(Vector2 p)
    {
        if (DirectMode)
        {
            myTransform.localPosition = new Vector3(p.x, p.y, myTransform.localPosition.z);
        }
        else
        {
            registerPosition.DefaultValue = new Vector3(p.x, p.y, registerPosition.DefaultValue.z);
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
