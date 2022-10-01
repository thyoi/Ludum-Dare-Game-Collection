using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    static public StageManager mainManager;

    public StageContainer stage1;
    public StageContainer stage2;
    public Level[] levelList;
    public Vector2[] ShowPosition;

    private int levelCount = 0;


    public void NextLevel()
    {
        if (levelCount < levelList.Length)
        {
            stage1.Register(levelList[levelCount]);
            stage1.ShowItems(ShowPosition[levelCount]);
            stage2.HideItem(ShowPosition[levelCount]);
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
    }

    public void Start()
    {
        NextLevel();
    }
}
