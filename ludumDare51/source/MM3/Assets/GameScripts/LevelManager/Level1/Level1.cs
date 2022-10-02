using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public Level myLevel;
    private bool start;

    public void Awake()
    {
        myLevel.initFunction = InitLevel;
        myLevel.endFunction = EndLevel;
    }
    public void InitLevel()
    {
        start = true;
        DisableButtons();
        Init();
    }
    public void EndLevel()
    {
        start = false;
    }



    public MouseArea[] buttons;
    public int[,] borad;
    public Boomable[,] boomBoards;
    public GameObject[] tokens;

    private bool PlayerTurn;
    private int autoSetCount = 0;

    public void Init()
    {
        borad = new int[5,5];
        boomBoards = new Boomable[5,5];
        foreach(MouseArea m in buttons)
        {
            m.ClickCallBack = ButtonClick;
        }
        PlayerPlay();
    }

    public void PlayerPlay()
    {
        PlayerTurn = true;
    }

    public void DisableButtons()
    {
        foreach(MouseArea m in buttons)
        {
            if(m.data[0]==2 || m.data[0] == -2| m.data[1] == 2|| m.data[1] == -2)
            {
                m.active = false;
            }
        }
    }

    public void activeButton()
    {
        foreach (MouseArea m in buttons)
        {
            m.active = true;
        }
    }

    public Boomable CreateToken(Vector2 p,int t, AnimationCallBack callback = null)
    {
        GameObject tem = Instantiate(tokens[t-1], transform);
        UF.SetLocalPosition(tem.transform, p);
        StageManager.AddItem(tem.transform.GetComponent<LeaveStage>());
        tem.transform.GetComponent<ScaleWithCurveAnimation2D>().StartAnimation(callback);
        return tem.transform.GetComponent<LeaveStage>().boom;
    }

    public Boomable CreateToken(int x,int y, int t,AnimationCallBack callback = null)
    {
        return CreateToken(new Vector2(1.05f * (x-2), 1.05f * (y-2) - 0.35f), t, callback);
    }

    public void ButtonClick(int[] data)
    {
        if (PlayerTurn && borad[data[0]+2,data[1]+2] == 0 && start)
        {
            SetToken(data[0]+2, data[1]+2);
        }
    }

    private void SetToken(int x,int y)
    {
        PlayerTurn = false;
        Set(x, y, 1,AutoSet);
    }

    public void AutoSet()
    {
        if (Setable()>0)
        {
            autoSetCount++;
            if (autoSetCount > 3)
            {
                activeButton();
            }
            int r = Random.Range(1, Setable()+1);
            SetNum(r, PlayerPlay);
        }
        
    }

    public int Setable()
    {
        int res = 0;
        foreach(MouseArea m in buttons)
        {
            if(m.active && borad[m.data[0]+2,m.data[1]+2] == 0)
            {
                res++;
            }
        }
        return res;
    }
    public void SetNum(int a, AnimationCallBack callback)
    {
        int res = a;
        foreach (MouseArea m in buttons)
        {
            if (m.active && borad[m.data[0] + 2, m.data[1] + 2] == 0)
                    {
                    res--;
                    if(res == 0)
                    {
                        Set(m.data[0] + 2, m.data[1] + 2, 2, callback);
                    }
                }
            
        }
    }
    private AnimationCallBack temCall;
    private void Set(int x,int y,int t,AnimationCallBack callback)
    {
        switch (t)
        {
            case (1):
                SoundManager.PlaySound("show");
                break;
            case (2):
                SoundManager.PlaySound("deshow");
                break;
        }
        borad[x, y] = t;
        boomBoards[x, y] = CreateToken(x, y,t,()=> { Judge(callback); });
    }

    private void Judge(AnimationCallBack callback)
    {
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                if(i<3 &&  UF.Equile(borad[i,j], borad[i+1,j], borad[i + 2,j],0))
                {
                    TokenBoom(i, j, callback);
                    TokenBoom(i + 1, j);
                    TokenBoom(i + 2, j);
                    SoundManager.PlaySound("boom1");
                    return;
                }
                if (j< 3 && UF.Equile(borad[i, j], borad[i, j+1], borad[i , j+2],0))
                {
                    TokenBoom(i, j, callback);
                    TokenBoom(i , j+1);
                    TokenBoom(i , j+2);
                    SoundManager.PlaySound("boom1");
                    return;
                }
                if (i < 3 && j<3 && UF.Equile(borad[i, j], borad[i + 1, j+1], borad[i + 2, j+2],0))
                {
                    TokenBoom(i, j, callback);
                    TokenBoom(i + 1, j+1);
                    TokenBoom(i + 2, j+2);
                    SoundManager.PlaySound("boom1");
                    return;
                }
                if (i < 3 && j>1 && UF.Equile(borad[i, j], borad[i + 1, j-1], borad[i + 2, j-2],0))
                {
                    TokenBoom(i, j, callback);
                    TokenBoom(i + 1, j-1);
                    TokenBoom(i + 2, j-2);
                    SoundManager.PlaySound("boom1");
                    return;
                }
            }
        }
        callback();
    }
    private void TokenBoom(int x,int y,AnimationCallBack callback = null)
    {
        borad[x,y] = 0;
        boomBoards[x, y].StartBoom(callback);
    }

}
