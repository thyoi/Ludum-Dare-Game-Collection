using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6 : MonoBehaviour
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
        mainBall.active = true;
    }
    public void EndLevel()
    {
        start = false;
    }

    public Transform mainBrick;
    public BallCountroler mainBall;
    public GameObject ball;
    public GameObject bigBall;
    public float interTime = 5;
    public int timeLayer = 3;

    private float count;
    private float dt;
    private int waveCount;


    public void CreateBall(bool big = false)
    {
        GameObject tem;
        if (big)
        {
            tem = Instantiate(bigBall, transform);
        }
        else
        {
            tem = Instantiate(ball, transform);
        }
        StageManager.AddItem(tem.transform.GetComponent<LeaveStage>());
        tem.transform.GetComponent<BallCountroler>().bricks = mainBall.bricks;
        tem.transform.GetComponent<BallCountroler>().Speed = new Vector2(0, 5);
        tem.transform.position = new Vector3(MouseManager.MousePosition().x, -3.8f);
    }
    public void Init()
    {
        count = interTime;
    }

    public void Update()
    {
        if (start)
        {
            Vector3 tem = mainBrick.position;
            tem.x = MouseManager.MousePosition().x;
            mainBrick.position = tem;

            dt = TimeManager.DeltaTime(timeLayer);
            if (dt > 0)
            {
                if (count > 0)
                {
                    count -= dt;
                    if (count <= 0)
                    {
                        waveCount++;
                        count = interTime;
                        CreateBall(waveCount %3==2) ;
                        if(waveCount == 2)
                        {
                            interTime = 2;
                        }
                    }
                }
            }
        }
    }
}
