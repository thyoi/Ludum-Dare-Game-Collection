using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPositionAnimation2D : MonoBehaviour
{
    public bool DirectMode = false;
    public int timeLayer = 3;
    public float speed = 7;
    public float minSpeed = 0.001f;
    public float delay = 0;

    private Transform myTransform;
    private Vector3Countroler registerPosition;
    protected Vector2 curPosition;
    protected Vector2 endPosition;
    protected Vector2 velocity;
    protected float dt;
    private BoolCountroler stop;
    private BoolCountroler pause;
    protected Vector2 minVelocity;

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

    public Vector2 EndPosition
    {
        get { return endPosition; }
        set
        {
            Finish = false;
            if (minSpeed != 0) { minVelocity = (endPosition - curPosition).normalized * minSpeed; }
            endPosition = value;
        }
    }

    public Vector2 CurPosition
    {
        get { return curPosition; }
        set
        {
            Finish = false;
            if (minSpeed != 0) { minVelocity = (endPosition - curPosition).normalized * minSpeed; }
            curPosition = value;
            SetPosition(curPosition);
        }
    }


    void Awake()
    {
        stop = new BoolCountroler(false);
        pause = new BoolCountroler(false);
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
            }
            else
            {
                UpdatePosition();
            }
        }
    }

    protected virtual void UpdatePosition()
    {
        Vector2 distant = (endPosition - curPosition);
        Vector2 tem = distant * dt * speed + minVelocity;
        if(UF.LongerBlockDistance(distant,tem))
        {
            curPosition += tem;
        }
        else
        {
            curPosition = endPosition;
            Finish = true;
        }
        SetPosition(curPosition);
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

  
}
