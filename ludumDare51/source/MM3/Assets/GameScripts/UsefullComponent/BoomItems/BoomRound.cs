using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomRound : MonoBehaviour
{
    public ScaleWithCurveAnimation2D round;
    public ScaleWithCurveAnimation2D mask;


    public void StartBoom(float Size)
    {
        round.endSize = Size;
        mask.endSize = Size + 0.1f;
        round.StartAnimation();
        mask.StartAnimation();
    }


}
