using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    static private float[] timeResultList;

    static TimeManager()
    {
        timeResultList = new float[32];
        for(int i = 0; i < 32; i++)
        {
            timeResultList[i] = 0;
        }
    }

    static public float DeltaTime(int layer)
    {
        return timeResultList[layer];
    }


    public string[] LayerName;
    private int LayerNum;
    private FloatCountroler[] timeScaleList;
    private float[] lagCountDown;
    private float dt;

    void Awake()
    {
        LayerNum = LayerName.Length;
        timeScaleList = new FloatCountroler[LayerNum];
        lagCountDown = new float[LayerNum];
        FloatCountroler tem;
        for (int i = 0; i < LayerNum; i++)
        {
            timeScaleList[i] = new FloatCountroler(1);
            tem = new FloatCountroler();
            tem.OperationIndex = ValueCountrolerManager.OprationName.any_ignore;
            timeScaleList[i].AddFactor(tem);
            tem = new FloatCountroler();
            tem.OperationIndex = ValueCountrolerManager.OprationName.any_ignore;
            timeScaleList[i].AddFactor(tem);
            lagCountDown[i] = 0;
        }

    }

    private void Reset()
    {
        for(int i = 0; i < LayerNum; i++)
        {
            timeScaleList[i].FactorWithIndex(0).OperationIndex = ValueCountrolerManager.OprationName.any_ignore;
            timeScaleList[i].FactorWithIndex(1).OperationIndex = ValueCountrolerManager.OprationName.any_ignore;
            timeScaleList[i].DefaultValue = 1;
        }
    }

    private bool LayerIsPause(int i)
    {
        if(i < LayerNum)
        {
            return timeScaleList[i].FactorWithIndex(1).OperationIndex == ValueCountrolerManager.OprationName.any_overwrite;
        }
        else
        {
            return true;
        }
    }

    public void Pause(uint layerMask)
    {
        for(int i = 0; i < LayerNum; i++)
        {
            if ((layerMask & 1) != 0)
            {
                timeScaleList[i].FactorWithIndex(1).OperationIndex = ValueCountrolerManager.OprationName.any_overwrite;
            }
            layerMask >>= 1;
        }
    }

    public void DePause(uint layerMask)
    {
        for (int i = 0; i < LayerNum; i++)
        {
            if ((layerMask & 1) != 0)
            {
                timeScaleList[i].FactorWithIndex(1).OperationIndex = ValueCountrolerManager.OprationName.any_ignore;
            }
            layerMask >>= 1;
        }
    }

    public void DePause()
    {
        for (int i = 0; i < LayerNum; i++)
        {
            timeScaleList[i].FactorWithIndex(1).OperationIndex = ValueCountrolerManager.OprationName.any_ignore;
        }
    }

    public void Lag(float time, uint layerMask)
    {
        for (int i = 0; i < LayerNum; i++)
        {
            if ((layerMask & 1) != 0)
            {
                timeScaleList[i].FactorWithIndex(0).OperationIndex = ValueCountrolerManager.OprationName.any_overwrite;
                lagCountDown[i] = time;
            }
            layerMask >>= 1;
        }
    }

    public void DeLag(uint layerMask)
    {
        for (int i = 0; i < LayerNum; i++)
        {
            if ((layerMask & 1) != 0)
            {
                timeScaleList[i].FactorWithIndex(0).OperationIndex = ValueCountrolerManager.OprationName.any_ignore;
                lagCountDown[i] = 0;
            }
            layerMask >>= 1;
        }
    }

    public void DeLag()
    {
        for (int i = 0; i < LayerNum; i++)
        {
            timeScaleList[i].FactorWithIndex(0).OperationIndex = ValueCountrolerManager.OprationName.any_ignore;
            lagCountDown[i] = 0;
        }
    }

    public float DeltaTimeWithLayer(int layer)
    {
        return TimeManager.DeltaTime(layer);
    }

    void Update()
    {
        dt = Time.deltaTime;
        for(int i = 0; i < LayerNum; i++)
        {
            if(lagCountDown[i]>0 && !LayerIsPause(i))
            {
                lagCountDown[i] -= dt;
                if (lagCountDown[i] <= 0)
                {
                    timeScaleList[i].FactorWithIndex(0).OperationIndex = ValueCountrolerManager.OprationName.any_ignore;
                }
            }
            timeResultList[i] = dt * timeScaleList[i].Value;
        }
    }
}
