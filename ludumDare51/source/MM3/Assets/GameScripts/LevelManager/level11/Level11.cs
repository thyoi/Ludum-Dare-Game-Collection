using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level11 : MonoBehaviour
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
        count1 = inter1;
        count2 = inter2;
        count3 = inter3;
    }
    public void EndLevel()
    {
        start = false;
    }

    public void Init()
    {

    }

    public float inter1;
    public float inter2;
    public float inter3;
    public int timeLayer = 3;
    public Transform Player;
    public GameObject dropPrefab;
    public Sprite[] sprites;
    public Color c;

    private float count1;
    private float count2;
    private float count3;
    private float dt;


    public void Update()
    {
        if (start)
        {
            dt = TimeManager.DeltaTime(timeLayer);
            if (dt > 0)
            {
                UpdatePlayer();
                if (count1 > 0)
                {
                    count1 -= dt;
                    if (count1 <= 0)
                    {
                        count1 = inter1;
                        trigger1();
                    }
                }
                if (count2 > 0)
                {
                    count2 -= dt;
                    if (count2 <= 0)
                    {
                        count2 = inter2;
                        trigger2();
                    }
                }
                if (count3 > 0)
                {
                    count3 -= dt;
                    if (count3 <= 0)
                    {
                        count3 = inter3;
                        trigger3();
                    }
                }
            }
        }
    }


    public void UpdatePlayer()
    {
        Vector3 tem = Player.transform.position;
        tem.x = MouseManager.MousePosition().x;
        tem.y = -3.89f;
        Player.transform.position = tem;
    }

    public void trigger1()
    {
        Drop();
    }
    public void trigger2()
    {
        Drop();
    }
    public void trigger3()
    {
        Drop();
    }

    public void Drop()
    {

        GameObject tem = Instantiate(dropPrefab, transform);
        float x = Random.Range(-7, 7);
        tem.transform.GetComponent<PositionInPathWithCurveAnimation2D>().animationTime = Random.Range(2, 3);
        tem.transform.GetComponent<PositionInPathWithCurveAnimation2D>().initPosition.x = x;
        tem.transform.GetComponent<PositionInPathWithCurveAnimation2D>().endPosition.x = x;
        tem.transform.GetComponent<PositionInPathWithCurveAnimation2D>().StartAnimation();
        tem.transform.GetComponent<DropItem>().rspeed = Random.Range(150, 300);
        tem.transform.GetComponent<ColChecker>().callBack = ()=>{tem.transform.GetComponent<LeaveStage>().boom.StartBoom(); };
        tem.transform.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        tem.transform.GetComponent<SpriteRenderer>().color = c;
        StageManager.AddItem(tem.transform.GetComponent<LeaveStage>());
    }
}
