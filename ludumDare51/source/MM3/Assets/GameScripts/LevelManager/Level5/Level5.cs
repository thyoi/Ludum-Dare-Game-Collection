using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : MonoBehaviour
{
    public Level myLevel;
    private bool start;

    public void Awake()
    {
        myLevel.initFunction = InitLevel;
        myLevel.endFunction = EndLevel;
        Init();
    }
    public void Init()
    {
        Position1 = new Vector2Int(-1, 1);
        filpMap = new bool[4, 3];
        boomBorad = new FlipCard[4, 3];
    }
    public void InitLevel()
    {
        start = true;


        title2.callBack = () => { titleBoom2.StartBoom(); titleBoom1.StartBoom(); };
        foreach (FlipCard i in cardInit)
        {
            i.StartRotate();
        }
        foreach (MouseArea i in cardButton)
        {
            boomBorad[i.data[0], i.data[1]] = i.transform.GetComponent<FlipCard>();
            boomBorad[i.data[0], i.data[1]].callBack = i.Active;
            i.ClickCallBack = PickCard;
        }
        PlayerTurn();
    }
    public void EndLevel()
    {
        start = false;
    }

    public FlipCard[] cardInit;
    public Boomable titleBoom1;
    public FlipCard title1;
    public Boomable titleBoom2;
    public FlipCard title2;
    public MouseArea[] cardButton;

    private int[,] borad;
    private FlipCard[,] boomBorad;
    private bool playerTurn;
    private Vector2Int Position1;
    public bool[,] filpMap;

    private int lx;
    private int ly;


    public void PlayerTurn()
    {
        playerTurn = true;
    }

    public void PickCard(int[] data)
    {
        if (playerTurn)
        {
            Pick(data[0], data[1]);
        }
    }
    public void Pick(int x,int y)
    {
        lx = x;
        ly = y;
        playerTurn = false;
        SoundManager.PlaySound("show");
        OpenCard(x, y, () => { JudgeBoom(() => { Judge(PlayerTurn); }); });

    }
    public void Judge(AnimationCallBack callBack = null)
    {
        if (Position1.x < 0)
        {
            Position1.x = lx;
            Position1.y = ly;
            callBack();
            return;
        }
        else
        {
            if(boomBorad[Position1.x,Position1.y].cardType == boomBorad[lx, ly].cardType)
            {
                Position1.x = -1;
                SoundManager.PlaySound("tip");
                callBack();
                return;
            }
            else
            {
                SoundManager.PlaySound("deshow");
                DeopenCard(Position1.x, Position1.y, callBack);
                DeopenCard(lx,ly);
                Position1.x = -1;
            }
        }
    }
    public void JudgeBoom(AnimationCallBack callBack = null)
    {
        if(filpMap[0,0] && filpMap[1,1] && filpMap[2, 2])
        {
            Position1.x = -1;
            boomBorad[0, 0].boom.StartBoom(PlayerTurn);
            boomBorad[1, 1].boom.StartBoom();
            boomBorad[2, 2].boom.StartBoom();
            Disable(0, 0);
            Disable(1, 1);
            Disable(2, 2);
            filpMap[0, 0] = false;
            SoundManager.PlaySound("boom1");
            return;
        }
        if (filpMap[1, 0] && filpMap[2, 0] && filpMap[3, 0])
        {
            Position1.x = -1;
            boomBorad[1, 0].boom.StartBoom(PlayerTurn);
            boomBorad[2, 0].boom.StartBoom();
            boomBorad[3, 0].boom.StartBoom();
            Disable(1, 0);
            Disable(2, 0);
            Disable(3, 0);
            filpMap[1, 0] = false;
            SoundManager.PlaySound("boom1");
            return;
        }
        callBack();
    }

    public void Disable(int x, int y)
    {
        boomBorad[x, y].transform.GetComponent<MouseArea>().active = false;
    }
    public void Enable(int x, int y)
    {
        boomBorad[x, y].transform.GetComponent<MouseArea>().active = true;
    }

    private void OpenCard(int x,int y,AnimationCallBack callBack = null)
    {
        boomBorad[x, y].curDrgee = 0;
        boomBorad[x, y].endDrgee = 180;
        boomBorad[x, y].callBack = callBack;
        boomBorad[x, y].StartRotate();
        filpMap[x, y] = true;
    }
    private void DeopenCard(int x, int y, AnimationCallBack callBack = null)
    {
        boomBorad[x, y].curDrgee = 180;
        boomBorad[x, y].endDrgee = 0;
        boomBorad[x, y].callBack = callBack;
        boomBorad[x, y].StartRotate();
        filpMap[x, y] = false;
    }

    public void ActvateB()
    {
        foreach(MouseArea i in cardButton)
        {
            i.active = true;
        }
    }
}
