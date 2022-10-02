using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level7 : MonoBehaviour
{
    public Level myLevel;
    private bool start;

    public void Awake()
    {
        myLevel.initFunction = InitLevel;
        myLevel.endFunction = EndLevel;
        Init();
    }
    public void InitLevel()
    {
        start = true;
    }
    public void EndLevel()
    {
        start = false;
    }

    public float delay;
    public float delay2;
    public int timeLayer = 3;
    public Transform sleep1;
    public Transform sleep2;
    public Vector2 sleepPosition;
    public Boomable b1;
    public Boomable b2;
    public Boomable b3;
    public Boomable b4;


    private float dt;

    public void Init()
    {

    }

    public void Update()
    {
        if (start)
        {
            dt = TimeManager.DeltaTime(timeLayer);
            if (dt > 0)
            {
                if (delay > 0)
                {
                    delay -= dt;
                    if (delay <= 0)
                    {
                        endAnime();
                    }
                }
                if (delay2 > 0)
                {
                    delay2 -= dt;
                    if (delay2 <= 0)
                    {
                        SoundManager.PlaySound("enddram");
                    }
                }
            }
        }
    }

    public void endAnime()
    {
        sleep1.position = new Vector3(10, 20, 0);
        UF.SetPosition(sleep2, sleepPosition);
        //SoundManager.PlaySound("enddram");
        b1.StartBoom(() => { b2.StartBoom(() => { b3.StartBoom(() => { b4.StartBoom(); }); }); });
    }
}
