using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level10 : MonoBehaviour
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
        Set();
        PlayerTurn();
        b1.StartAnimation();
    }
    public void EndLevel()
    {
        start = false;
    }

    public void Init()
    {

    }

    public MonsterControler[] monsters;
    public Vector2[] position;
    public  Sprite[] mouseSprite;
    public PositionInPathWithCurveAnimation2D delayCallBack;
    public PositionInPathWithCurveAnimation2D delayCallBack2;
    public Color color0;
    public GameObject monster3;
    public PositionInPathWithCurveAnimation2D b1;
    public PositionInPathWithCurveAnimation2D b2;
    public PositionInPathWithCurveAnimation2D b3;

    private int State;
    private bool playerTurn;

    public void PlayerTurn()
    {
        playerTurn = true;
    }

    public void Set()
    {
        for(int i = 0; i < 8; i++)
        {
            if (monsters[i] != null)
            {
                monsters[i].SetPosition(position[i]);
            }
        }
    }
    public int MouseOn()
    {
        Vector2 tem = MouseManager.MousePosition();
        if(tem.y>0 || tem.y < -2.5f)
        {
            return -1;
        }
        else
        {
            return (int)((tem.x + 4.5f * 1.8f) / 1.8f);
        }
    }

    public int SelectN(int n,int t)
    {
        OffAll();
        if (t >= 0)
        {
            int l = n / 2;
            int r = n - 1 - l;
            int init = 0;
            if (t > l)
            {
                init = t - l;
            }
            if (t > 7 - r)
            {
                init = 8 - n;
            }
            for (int i = 0; i < n; i++)
            {
                Select(init + i, true);
            }
            return init;
        }
        return -1;
    }

    public void Select(int n,bool on)
    {
        if(n>=0 && n < 8 && monsters[n] != null)
        {
            if (on) { monsters[n].Onbord(); }
            else
            {
                monsters[n].OffBord();
            }
        }
    }

    public void OffAll()
    {
        for(int i = 0; i < 8; i++)
        {
            Select(i, false);
        }
    }
    public void OnAll()
    {
        for (int i = 0; i < 8; i++)
        {
            Select(i, true);
        }
    }

    public void Update()
    {
        if (start)
        {
            MouseManager.SetMouseSprite(mouseSprite);
            //OnAll();
            if((State == 0||State == 3) && playerTurn)
            {
                int tem = SelectN(3, MouseOn());
                if(Input.GetMouseButtonDown(0) && tem >= 0)
                {
                    ColorMagic(tem);
                    Judge();
                }
            }
            else if (State == 1 && playerTurn)
            {
                int tem = SelectN(2, MouseOn());
                if (Input.GetMouseButtonDown(0) && tem >= 0)
                {
                    SwitchMagic(tem);
                    //Judge();
                }
            }
            else if(State == 2 && playerTurn)
            {

                int tem = SelectN(1, MouseOn());
                if (Input.GetMouseButtonDown(0) && tem >= 0)
                {
                    CloneMagic(tem);
                    //Judge();
                }
            }


        }
    }

    public void ColorMagic(int init)
    {
        for(int i = 0; i < 3; i++)
        {
            SetColor0(init + i);
        }
    }

    public void SetColor0(int n)
    {
        if (n >= 0 && n < 8 && monsters[n] != null)
        {
            monsters[n].SetColor0(color0);
        }
    }

    public void NextState()
    {
        State++;
        PlayerTurn();
        if(State == 1)
        {
            b1.endPosition.y = -2.5f;
            b1.initPosition.y = 0;
            b1.StartAnimation();
            b2.StartAnimation();
            monsters[4] = monsters[2];
            monsters[3] = monsters[1];
            monsters[2] = monsters[0];
            monsters[0] = null;
            monsters[1] = null;
            monsters[5] = monsters[6];
            monsters[6] = monsters[7];
            monsters[7] = null;
            Set();
        }
        else if(State == 2)
        {
            b2.endPosition.y = -2.5f;
            b2.initPosition.y = 0;
            b2.StartAnimation();
            b3.StartAnimation();
            UF.print(monsterN(1));
            UF.print(monsterN(2));
            MonsterControler t1 = monsters[monsterN(1)];
            MonsterControler t2 = monsters[monsterN(2)];
            cleanMonster();
            monsters[3] = t1;
            monsters[4] = t2;
            Set();
        }
        else if(State == 3)
        {
            b3.endPosition.y = -2.5f;
            b3.initPosition.y = 0;
            b3.StartAnimation();
            b1.endPosition.y = 0;
            b1.initPosition.y = -2.5f;
            b1.StartAnimation();
        }
    }

    public MonsterControler FirstMonster()
    {
        for(int i = 0; i < 8; i++)
        {
            if (monsters[i] != null)
            {
                MonsterControler tem = monsters[i];
                monsters[i] = null;
                return tem;
            }
        }
        return null;
    }

    public void cleanMonster()
    {
        for (int i = 0; i < 8; i++)
        {
            monsters[i] = null;
        }
    }
    public int MonsterNum()
    {
        int res = 0;
        for (int i = 0; i < 8; i++)
        {
            if (monsters[i] != null)
            {
                res++;
            }
        }
        return res;
    }

    public int monsterN(int n)
    {
        int res = n;
        for (int i = 0; i < 8; i++)
        {
            if (monsters[i] != null)
            {
                res--;
                if(res == 0)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public void SetAndMove()
    {
        MonsterControler[] tem = new MonsterControler[8];
        int n = MonsterNum();
        int t = n / 2;
        for(int i = 0; i < n; i++)
        {
            tem[t + n] = FirstMonster();
        }
        monsters = tem;
        Set();
    }

    public void Judge()
    {
        for(int i = 0; i < 6;i++)
        {
            if(equile(monsters[i], monsters[i+1], monsters[i + 2]))
            {
                monsters[i].StartBoom();
                monsters[i+1].StartBoom();
                monsters[i+2].StartBoom();
                monsters[i] = null;
                monsters[i+1] = null;
                monsters[i+2] = null;
                playerTurn = false;
                if (State == 0)
                {
                    delayCallBack.StartAnimation(NextState);
                }
                else
                {
                    delayCallBack2.StartAnimation(NextState);
                }
                break;
            }
        }
        if(State == 2)
        {
            NextState();
        }
    }

    public bool equile(MonsterControler m1, MonsterControler m2, MonsterControler m3)
    {
        if(m1 == null || m2 == null | m3 == null)
        {
            return false;
        }
        if(m1.color == m2.color && m1.color == m3.color && m1.type == m2.type && m1.type == m3.type)
        {
            return true;
        }
        return false;
    }

    public void SwitchMagic(int init)
    {
        if(monsters[init] == null || monsters[init + 1] == null)
        {
            return;
        }
        else
        {
            MonsterControler tem = monsters[init];
            monsters[init] = monsters[init + 1];
            monsters[init + 1] = tem;
            Set();
            Judge();
        }
    }

    public void CloneMagic(int n)
    {
        if(n == 3)
        {
            monsters[5] = monsters[4];
            monsters[4] = Instantiate(monster3, transform).transform.GetComponent<MonsterControler>();
            monsters[4].SetColor(monsters[3].GetClolr());
            monsters[4].color = monsters[3].color;
            //monsters[4].SetInitEnd(position[6], position[7]);
            //monsters[4].SetCur(position[2]);
            Set();
        }
        if(n == 4)
        {
            monsters[5] = monsters[4];
            monsters[4] = Instantiate(monster3, transform).transform.GetComponent<MonsterControler>();
            monsters[4].SetColor(monsters[5].GetClolr());
            monsters[4].color = monsters[5].color;
            //monsters[4].SetInitEnd(position[6], position[7]);
            //monsters[4].SetCur(position[2]);
            Set();
        }
        Judge();
        
    }

}
