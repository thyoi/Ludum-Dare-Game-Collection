using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageContainer : MonoBehaviour
{
    public ScaleWithCurveAnimation2D MaskAnimation;
    public SpriteRenderer BackGround;

    private Level curLevel;
    private List<LeaveStage> leaveItems = new List<LeaveStage>();
    private List<LeaveStage> scaleLeaveItem = new List<LeaveStage>();
    private List<LeaveStage> pushLeaveStages = new List<LeaveStage>();

    public void Clean()
    {
        if (curLevel != null)
        {
            Destroy(curLevel.gameObject);
        }
        
    }

    public void AddItem(LeaveStage l)
    {
        leaveItems.Add(l);
        switch (l.leave)
        {
            case (LeaveStage.leaveType.Scale):
                scaleLeaveItem.Add(l);
                break;
            case (LeaveStage.leaveType.pushed):
                pushLeaveStages.Add(l);
                break;
        }
    }
    public void Register(Level l)
    {
        l.InitLevel();
        curLevel = l;
        foreach(LeaveStage i in l.objectList)
        {
            switch (i.leave)
            {
                case (LeaveStage.leaveType.Scale):
                    scaleLeaveItem.Add(i);
                    break;
                case (LeaveStage.leaveType.pushed):
                    pushLeaveStages.Add(i);
                    break;
            }
            leaveItems.Add(i);
            i.ShowOutMask();
        }
        l.transform.parent = this.transform;
        BackGround.color = l.BackGroundColor;
    }


    public void ShowItems(Vector2 v , AnimationCallBack callback = null)
    {
        MaskAnimation.InitSize = 0;
        MaskAnimation.EndSize = 3;
        UF.SetPosition(MaskAnimation.transform, v);
        foreach(LeaveStage l in leaveItems)
        {
            l.ShowInMask();
        }
        BackGround.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        MaskAnimation.StartAnimation(callback);
    }

    public void HideItem(Vector2 v, AnimationCallBack callback = null)
    {
        if (curLevel != null)
        {
            curLevel.EndLevel();
        }
        MaskAnimation.InitSize = 0;
        MaskAnimation.EndSize = 3;
        UF.SetPosition(MaskAnimation.transform, v);
        foreach (LeaveStage l in leaveItems)
        {
            l.ShowOutMask();
        }
        BackGround.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        MaskAnimation.StartAnimation(Clean);
    }




}
