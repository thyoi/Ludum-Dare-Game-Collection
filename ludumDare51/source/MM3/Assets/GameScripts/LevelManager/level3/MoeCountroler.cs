using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoeCountroler : MonoBehaviour
{
    public float stayTime = 2;
    public float showTime = 0.5f;
    public int timeLayer = 3;


    private MouseArea mouseArea;
    private LeaveStage leave;
    private PositionInPathWithCurveAnimation2D positionAnimation;
    private bool boomed;
    private bool start;
    private float counter;
    private float dt;
    private AnimationCallBack callback;

    public void Awake()
    {
        mouseArea = transform.GetComponent<MouseArea>();
        leave = transform.GetComponent<LeaveStage>();
        positionAnimation = transform.GetComponent<PositionInPathWithCurveAnimation2D>();
    }

    public void Start()
    {
        Show();
    }

    public void Boomed()
    {
        boomed = true;
    }
    public void Show(AnimationCallBack callback = null)
    {
        this.callback = callback;
        mouseArea.ClickCallBack = (int[] a) => { leave.boom.StartBoom(); mouseArea.active = false; if (callback != null) { callback(); }Boomed(); };
        positionAnimation.animationTime = showTime;
        positionAnimation.StartAnimation();
        start = true;
    }
    public void Unshow()
    {
        start = false;
        positionAnimation.initPosition = positionAnimation.endPosition;
        positionAnimation.endPosition = Vector2.zero;
        positionAnimation.StartAnimation(()=> { if (!boomed && callback != null) { callback(); }Destroy(gameObject); });
    }

    public void Update()
    {
        dt = TimeManager.DeltaTime(timeLayer);
        if (start && dt>0)
        {
            counter += dt;
            if (counter > stayTime)
            {
                Unshow();
            }
        }
    }
}
