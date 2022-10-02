using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level9 : MonoBehaviour
{
    public Level myLevel;
    private bool start;
    public  float inter1;
    public float inter2;
    public float inter3;
    public int timeLayer = 3;

    private float count1;
    private float count2;
    private float count3;
    private float dt;


    public void Awake()
    {
        myLevel.initFunction = InitLevel;
        myLevel.endFunction = EndLevel;
        Init();
        count1 = inter1;
        count2 = inter2;
        count3 = inter3;
    }
    public void InitLevel()
    {
        start = true;
    }
    public void EndLevel()
    {
        start = false;
    }

    public void Init()
    {

    }

    public GameObject[] bullets;
    public float[] bulletsSpeed;
    public Transform player;
    public float Speed;
    private Vector2 playerPosition;


    public void CreateBullet(int t,Vector2 init, Vector2 end,float zRotate)
    {
        GameObject tem = Instantiate(bullets[t], transform);
        tem.transform.GetComponent<PositionInPathWithCurveAnimation2D>().InitPosition = init;
        tem.transform.GetComponent<PositionInPathWithCurveAnimation2D>().endPosition = end;
        tem.transform.GetComponent<PositionInPathWithCurveAnimation2D>().animationTime = UF.NomLen(init - end)/bulletsSpeed[t];
        tem.transform.GetComponent<PositionInPathWithCurveAnimation2D>().StartAnimation();
        tem.transform.rotation = Quaternion.AngleAxis(zRotate * 180 / Mathf.PI, new Vector3(0, 0, 1));
        StageManager.AddItem(tem.transform.GetComponent<LeaveStage>());
    }


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

    public void RandomButtle()
    {
        
        float x = Random.Range(-9, 9);
        float y = Random.Range(-5, 5);
        float r = Random.Range(0, 2 * Mathf.PI);
        Vector2 init = new Vector2(x, y);
        Vector2 dis = new Vector2(Mathf.Sin(r), Mathf.Cos(r)) * 15;
        int t = Random.Range(0, 10);
        if (t < 6)
        {
            t = 2;
        }
        else if (t < 9)
        {
            t = 1;
        }
        else
        {
            t = 0;
        }
        CreateBullet(t, init-dis, init + dis,-r);
    }

    public void RandomRound()
    {
        SoundManager.PlaySound("round");
        int t = Random.Range(0, 10);
        if (t < 6)
        {
            t = 1;
        }
        else if (t < 9)
        {
            t = 2;
        }
        else
        {
            t = 0;
        }
        int n = Random.Range(12, 40);
        float d = 1.5f + Random.Range(0f, 6f);
        float r = Random.Range(0, Mathf.PI * 2);
        Vector2 init = new Vector2(Mathf.Sin(r), Mathf.Cos(r)) * d;
        RoundButtle(init, n, t);

    }

    public void RoundButtle(Vector2 init, int n, int type)
    {
        for(int i = 0; i < n; i++)
        {
            float r = Mathf.PI*2/n*i;
            Vector2 dis = new Vector2(Mathf.Sin(r), Mathf.Cos(r)) * 15;
            CreateBullet(type, init, init + dis, -r);
        }
    }
    

    

    public void trigger1()
    {
        RandomButtle();
    }
    public void trigger2()
    {
        RandomRound();
    }
    public void trigger3()
    {
        RandomRound();
    }

    public void UpdatePlayer()
    {
        Vector2 tem = Vector2.zero;
        tem.x = Input.GetKey(KeyCode.A) ? (Input.GetKey(KeyCode.D) ? 0 : -1) : (Input.GetKey(KeyCode.D) ? 1 : 0);
        tem.y = Input.GetKey(KeyCode.S) ? (Input.GetKey(KeyCode.W) ? 0 : -1) : (Input.GetKey(KeyCode.W) ? 1 : 0);
        playerPosition += tem * Speed * dt;
        player.position = playerPosition;
    }

}
