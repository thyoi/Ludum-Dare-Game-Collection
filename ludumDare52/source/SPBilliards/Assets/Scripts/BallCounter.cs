using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCounter : MonoBehaviour
{
    public int BallNum = 0;
    public static BallCounter GlobleManager;
    public DialogManager Dia;
    public UF.AnimeCallback[] BallCallBack = new UF.AnimeCallback[1000];
    public SpriteRenderer border;
    private int cbCount = 0;
    public AnimeCountroler ac;
    public AnimeCountroler ability1;
    public AnimeCountroler ability2;
    public HitManager hitM;
    public Transform MainBall;
    public MainManager MainM;

    public  int MoveBallCount = 0;
    public  int PushBallCount = 0;

    public void MoveAbilityUnlock()
    {
        hitM.DeActive();
        ability1.StartAnime(() =>
        {
            hitM.MoveAbility = true;
            hitM.active = true;
            MainM.startCreateBall2();
        });
        particalManager.GlobalManager.BoomParticalBust(100, Vector2.zero, Color.white, 4, true);
        SoundManager.Play("ab");
    }

    public void PushAbilityUnlock()
    {
        hitM.DeActive();
        ability2.StartAnime(() =>
        {
            hitM.PushAbility = true;
            hitM.active = true;
            MainM.startCreateBall4();
        });
        particalManager.GlobalManager.BoomParticalBust(100, Vector2.zero, Color.white, 4, true);
        SoundManager.Play("ab");
    }


    public static void NB(BallCountroler b)
    {
        GlobleManager.NewBall(b);
    }

    public static void SetCallBack(int n,UF.AnimeCallback cb)
    {
        GlobleManager.BallCallBack[n] = cb;
    }

    public static void SetCallBackM(int n, UF.AnimeCallback cb)
    {
        GlobleManager.BallCallBack[GlobleManager.cbCount+n] = cb;
    }

    public static Transform MainBallTransform()
    {
        return GlobleManager.MainBall;
    }

    public void NextCB()
    {
        if (BallCallBack[cbCount] != null)
        {
            BallCallBack[cbCount]();
            BallCallBack[cbCount] = null;
            

        }
        cbCount++;
    }


    public void Bomm(float size)
    {
        ac.ScaleX.Init = size;
        ac.ScaleY.Init = size;
        ac.StartAnime();
    }

    public BallCounter()
    {
        GlobleManager = this;
    }
    // Start is called before the first frame update




    public void NewBall(BallCountroler b)
    {
        if (b.Scroe > 0)
        {
            if (b.MoveBall)
            {
                MoveBallCount++;
                if(MoveBallCount == 4)
                {
                    MoveAbilityUnlock();
                }
            }
            if (b.PushBall)
            {
                PushBallCount++;
                if(PushBallCount == 3)
                {
                    PushAbilityUnlock();
                }
            }
            BallNum += b.Scroe;
            for (int i = 0; i < b.Scroe; i++)
            {
                NextCB();
            }
         
            Bomm(1.1f+b.Scroe*0.1f);
            border.color = b.MainColor;
            Dia.DefaultColor = b.MainColor;
            Dia.ShowContent("" + BallNum);
            particalManager.GlobalManager.BoomParticalBust(20 * b.Scroe, transform.position, b.MainColor, 1.4f + b.Scroe * 0.2f, true);
            if (b.Scroe > 2)
            {
                particalManager.GlobalManager.CreateShinePartical(transform.position, 1.4f + b.Scroe * 0.2f, b.MainColor, true);
            }
            else if(b.Scroe == 1)
            {
                particalManager.GlobalManager.CreateRoundPartical(transform.position, 1.4f, b.MainColor, true);
            }

        }
    }


    void Start()
    {
        Dia.Silent = true;
        Dia.ShowContent("0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
