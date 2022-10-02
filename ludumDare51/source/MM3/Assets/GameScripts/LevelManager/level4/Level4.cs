using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : MonoBehaviour
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
        title1.SetCallBack(() => { title2.StartBoom(); });
        player.GetComponent<ColChecker>().callBack = ()=> { player.GetComponent<LeaveStage>().boom.StartBoom(); ResetPlayer(); } ;
    }
    public void EndLevel()
    {
        start = false;
    }


    public PositionInPathWithCurveAnimation2D title1;
    public Boomable title2;
    public GameObject[] tokens;
    public float interTime = 0.1f;
    public int timeLayer = 3;
    public Transform player;
    public Vector2 playerPosition;
    public float Speed = 1;
    public GameObject playerPrefab;

    private float count;
    private float dt;
    private int waveCount;

    public void Init()
    {
        count = 3.2f;
    }


    public void CreateToken(float y1,float y2, int t, AnimationCallBack callback = null)
    {
        GameObject tem = Instantiate(tokens[t], transform);
        tem.transform.GetComponent<PositionInPathWithCurveAnimation2D>().initPosition.y = y1;
        tem.transform.GetComponent<PositionInPathWithCurveAnimation2D>().endPosition.y = y2;
        tem.transform.GetComponent<PositionInPathWithCurveAnimation2D>().StartAnimation();
        tem.transform.GetComponent<ColChecker>().callBack = ()=> { tem.transform.GetComponent<LeaveStage>().boom.StartBoom(); };
        StageManager.AddItem(tem.transform.GetComponent<LeaveStage>());
    }


    public void Update()
    {
        if (start)
        {
            dt = TimeManager.DeltaTime(timeLayer);
            if (dt > 0)
            {
                if (count > 0)
                {
                    count -= dt;
                    if (count <= 0)
                    {
                        count = interTime;
                        CreateCar();
                        if (waveCount > 50)
                        {
                            CreateRC();
                        }
                        else
                        {
                            CreateCar();
                        }
                    }
                }
            }
            UpdatePlayer();
        }
    }

    public void UpdatePlayer()
    {
        Vector2 tem = Vector2.zero;
        tem.x = Input.GetKey(KeyCode.A)? (Input.GetKey(KeyCode.D)? 0 : -1) : (Input.GetKey(KeyCode.D)? 1 : 0);
        tem.y = Input.GetKey(KeyCode.S)? (Input.GetKey(KeyCode.W)? 0 : -1) : (Input.GetKey(KeyCode.W)? 1 : 0);
        playerPosition += tem * Speed * dt;
        player.position = playerPosition;
    }

    public void CreateCar()
    {
        waveCount++;
        int line = Random.Range(0, 6);
        int type = Random.Range(0, 4);

        float y = line * 0.83f - 3.2f;
        CreateToken(y, y, type);
    }
    public void CreateRC()
    {
        waveCount++;
        int line1 = Random.Range(0, 6);
        int line2 = Random.Range(0, 6);
        int type = Random.Range(0, 4);

        float y1 = line1 * 0.83f - 3.2f;
        float y2 = line2 * 0.83f - 3.2f;
        CreateToken(y1, y2, type);
    }

    public void ResetPlayer()
    {
        player = Instantiate(playerPrefab, transform).transform;
        playerPosition = new Vector2(0, -4.4f);
        player.GetComponent<ColChecker>().callBack = () => { player.GetComponent<LeaveStage>().boom.StartBoom(); ResetPlayer(); };
    }
}
