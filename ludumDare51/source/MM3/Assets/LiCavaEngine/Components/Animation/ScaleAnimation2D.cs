using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimation2D : MonoBehaviour
{
    public bool DirectMode = false;
    public int timeLayer = 3;
    public float speed = 7;
    public float minSpeed = 0.001f;
    public float delay = 0;

    private BoolCountroler stop;
    private BoolCountroler pause;
    private Transform myTransform;
    private Vector3Countroler registerScale;
    private float curSize = 1;
    private float endSize;
    private float dt;

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

    public float CurSize
    {
        get { return curSize; }
        set
        {
            Finish = false;
            curSize = value;
            SetScale(curSize);
        }
    }

    public float EndSize
    {
        get { return endSize; }
        set
        {
            Finish = false;
            endSize = value;
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
                UpdateScale();
            }
        }
    }

    private void UpdateScale()
    {
        float distant = endSize - curSize;
        float tem = distant * dt * speed + ((distant>0)?minSpeed:-minSpeed);
        if (Mathf.Abs(distant)>Mathf.Abs(tem))
        {
            curSize += tem;
        }
        else
        {
            curSize = endSize;
            Finish = true;
        }
        SetScale(curSize);
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


}
