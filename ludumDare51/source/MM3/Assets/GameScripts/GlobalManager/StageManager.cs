using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    static public StageManager mainManager;
    static public void  AddItem(LeaveStage l)
    {
        mainManager.stage2.AddItem(l);
    }
    static public void InitItem(LeaveStage l)
    {
        mainManager.stage1.AddItem(l);
    }

    public StageContainer stage1;
    public StageContainer stage2;
    public Level[] levelList;
    public Vector2[] ShowPosition;
    public int levelCount = 0;


    public void NextLevel()
    {
        if (levelCount < levelList.Length)
        {
            SoundManager.PlaySound("next");
            stage1.Register(levelList[levelCount]);
            stage1.ShowItems(ShowPosition[levelCount]);
            stage2.HideItem(ShowPosition[levelCount]);
            if(levelCount == 2)
            {
                SoundManager.PlaySound("background");
            }
            levelCount++;

            SwitchStage();
        }
    }

    public void SwitchStage()
    {
        StageContainer tem = stage2;
        stage2 = stage1;
        stage1 = tem;
    }

    public void Awake()
    {
        mainManager = this;
        Random.InitState(41);
    }

    public void Start()
    {
        NextLevel();
    }
}
