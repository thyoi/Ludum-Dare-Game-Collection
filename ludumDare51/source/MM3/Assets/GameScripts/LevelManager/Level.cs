using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public delegate void LevelFunction(Level l);
    public LeaveStage[] objectList;
    public MouseArea[] mouseAreaList;
    public LevelFunction initFunction;
    public LevelFunction endFunction;
    public ScaleWithCurveAnimation2D[] showItem;
    public Color BackGroundColor;
    public ClockControler clock;


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
        if(initFunction != null)
        {
            initFunction(this);
        }
        if(clock != null)
        {
            clock.Init();
            clock.callback = StageManager.mainManager.NextLevel;
            clock.transform.parent.parent = null;
        }
        
    }


    public void EndLevel()
    {
        foreach (MouseArea m in mouseAreaList)
        {
            m.active = true;
        }
        if (endFunction != null)
        {
            endFunction(this);
        }
    }

}
