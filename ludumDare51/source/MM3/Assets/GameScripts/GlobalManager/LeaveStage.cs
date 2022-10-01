using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveStage : MonoBehaviour
{
    public enum leaveType
    {
        none,
        pushed,
        Scale

    }
    public leaveType leave;
    public  Character characterAnimation;
    public ScaleWithCurveAnimation2D scaleAnimation;
    public SpriteRenderer spriteRenderer;

    public void ShowInMask()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
    }
    public void ShowOutMask()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }
    }

    public void ScaleLeave(AnimationCurve curve, float time)
    {
        scaleAnimation.curve = curve;
        scaleAnimation.EndSize = 0;
        scaleAnimation.InitSize = 1;
        scaleAnimation.animationTime = time;
        scaleAnimation.StartAnimation();
    }

    public void Clean()
    {
        if (characterAnimation is null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            characterAnimation.ReturnToPool();
        }
    }
}
