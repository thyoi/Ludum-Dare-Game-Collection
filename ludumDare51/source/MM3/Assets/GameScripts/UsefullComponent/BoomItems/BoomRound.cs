using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomRound : MonoBehaviour
{
    public ScaleWithCurveAnimation2D round;
    public ScaleWithCurveAnimation2D mask;


    public void StartBoom(float Size, AnimationCallBack callback = null)
    {
        round.endSize = Size;
        mask.endSize = Size + 0.1f;
        mask.delay *= Size;
        round.StartAnimation(callback);
        mask.StartAnimation();
    }

    public void SetColor(Color c)
    {
        round.transform.GetComponent<SpriteRenderer>().color = c;
    }


}
