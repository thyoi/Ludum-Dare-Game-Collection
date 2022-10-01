using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction : MonoBehaviour
{
    public MouseArea startButtion;
    public Boomable boomButton;
    public void Start()
    {
        startButtion.ClickCallBack = (int[] data) => { 
            StageManager.mainManager.NextLevel();
            boomButton.StartBoom();
        };
    }
}
