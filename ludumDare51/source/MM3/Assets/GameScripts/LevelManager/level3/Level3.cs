using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
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
        count = interTime;
        borad = new int[holes.Length];
    }
    public void EndLevel()
    {
        start = false;
    }

    public int timeLayer = 3;
    public GameObject[] holes;
    public int[] borad;
    public Sprite[] mouse1;
    public Sprite[] mouse2;
    public GameObject[] tokens;

    private float interTime = 1.5f;
    private float count;
    private float dt;
    private int moeNum = 2;
    private int waveCount;


    public void Init()
    {

    }

    public void SetMouse()
    {
        if (Input.GetMouseButtonDown(0)) {
            SoundManager.PlaySound("hit");
        }
        if (Input.GetMouseButton(0))
        {
            MouseManager.SetMouseSprite(mouse2);
        }
        else
        {
            MouseManager.SetMouseSprite(mouse1);
        }
    }

    public void updateMoe()
    {
        if (count > 0)
        {
            count -= dt;
            if (count <= 0)
            {
                waveCount++;
                if (waveCount > 3)
                {
                    moeNum = 3;
                    interTime = 1.3f;
                }
                if (waveCount > 6)
                {
                    moeNum = 4;
                }
                if (CanSet() < 6)
                {
                    Clean();
                }
                for (int i = 0; i < moeNum; i++)
                {
                    NewMoe();
                }
                count = interTime;
            }
        }
    }

    public void NewMoe()
    {
        int tem = CanSet();
        if(tem > 0){
            int ttem = Random.Range(1, tem + 1);
            SetNum(ttem);
        }
    }

    public int CanSet()
    {
        int res = 0;
        foreach(int i in borad)
        {
            if (i == 0)
            {
                res++;
            }
        }
        return res;
    }

    public void SetNum(int n)
    {
        int res = n;
        for(int i = 0;i<borad.Length;i++)
        {
            if (borad[i] == 0)
            {
                res--;
                if(res == 0)
                {
                    CreateToken(i, Random.Range(0, tokens.Length));
                }
            }
        }
    }

    public void Update()
    {
        if (start)
        {
            SetMouse();
            dt = TimeManager.DeltaTime(timeLayer);
            if (dt > 0)
            {
                updateMoe();
            }
        }
    }

    public void CreateToken(int index,int t, AnimationCallBack callback = null)
    {
        borad[index] = 1;
        GameObject tem = Instantiate(tokens[t], holes[index].transform);
        StageManager.AddItem(tem.transform.GetComponent<LeaveStage>());
        tem.transform.GetComponent<MoeCountroler>().Show();
    }

    public void Clean()
    {
        for(int i = 0; i < borad.Length; i++)
        {
            borad[i] = 0;
        }
    }
}
