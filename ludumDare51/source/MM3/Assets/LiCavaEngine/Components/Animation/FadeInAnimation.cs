using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInAnimation : MonoBehaviour
{
    public bool outMode = false;
    public bool destoryWhenOut = false;
    public int timeLayer = 3;
    public float animationTime = 1;
    public float delay = 0;

    private BoolCountroler stop;
    private BoolCountroler pause;
    private float apha;
    private float dt;
    private SpriteRenderer spriteRenderer;
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



    // Start is called before the first frame update
    void Awake()
    {
        stop = new BoolCountroler(false);
        pause = new BoolCountroler(true);
        pause.OperationIndex = ValueCountrolerManager.OprationName.bool_or;
        stop.AddFactor(pause);
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
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
                UpdateApha();
            }
        }
    }

    void UpdateApha()
    {
        if (outMode)
        {
            if (apha > 0)
            {
                apha -= dt/animationTime;
                if (apha <= 0)
                {
                    apha = 0;
                    Finish = true;
                    CallBackOnce();
                    if (destoryWhenOut)
                    {
                        Destroy(this.gameObject);
                    }
                    
                }
            }
        }
        else
        {
            if (apha < 1)
            {
                apha += dt / animationTime;
                if (apha >= 1)
                {
                    apha = 1;
                    Finish = true;
                    CallBackOnce();
                }
            }
        }

        SetApha(apha);
    }

    void SetApha(float a)
    {
        Color tem = spriteRenderer.color;
        tem.a = a;
        spriteRenderer.color = tem;
    }

    public void StartAnimation(AnimationCallBack callBack = null)
    {
        this.callBack = callBack;
        Finish = false;
        Pause = false;
        if (outMode)
        {
            apha = 1;
        }
        else
        {
            apha = 0;
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
