using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public delegate void LevelFunction();
    public LeaveStage[] objectList;
    public MouseArea[] mouseAreaList;
    public LevelFunction initFunction;
    public LevelFunction endFunction;
    public ScaleWithCurveAnimation2D[] showItem;
    public PositionInPathWithCurveAnimation2D[] moveItem;
    public FadeInAnimation[] fadeItem;
    public Color BackGroundColor;
    public ClockControler clock;
    public ItemPage[] pages;


    public void InitLevel()
    {
        transform.position = Vector3.zero;
        foreach(MouseArea m in mouseAreaList)
        {
            m.active = true;
        }
        foreach(ScaleWithCurveAnimation2D i in showItem)
        {
            i.StartAnimation();
        }
        foreach(PositionInPathWithCurveAnimation2D i in moveItem)
        {
            i.StartAnimation();
        }
        foreach(FadeInAnimation i in fadeItem)
        {
            i.StartAnimation();
        }
        if(initFunction != null)
        {
            initFunction();
        }
        if(clock != null)
        {
            clock.Init();
            clock.callback = StageManager.mainManager.NextLevel;
            clock.transform.parent.parent = null;
        }
        foreach(ItemPage i in pages)
        {
            i.Init();
        }
        
    }


    public void EndLevel()
    {
        foreach (MouseArea m in mouseAreaList)
        {
            m.active = false;
        }
        if (endFunction != null)
        {
            endFunction();
        }
    }

}
