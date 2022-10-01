using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithCurve : MonoBehaviour
{
    public float time = 2;
    public int timeLayer = 3;
    public AnimationCurve curve;
    public float speed = 0.1f;

    private Transform myTransform;
    private float counte = 0;
    private float dt;
    public void Awake()
    {
        myTransform = transform;
    }

    void Update()
    {
        dt = TimeManager.DeltaTime(timeLayer);
        if (dt > 0)
        {
            if (counte < time)
            {
                counte += dt;
                if (counte > time)
                {
                    counte = time;
                }
            }
            rotateWithSpeed(curve.Evaluate(counte / time));
        }
    }
     private void rotateWithSpeed(float v)
    {
        myTransform.Rotate(0,0,v*dt*1000);
    }
}
