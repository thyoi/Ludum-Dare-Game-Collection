using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainManager : MonoBehaviour
{
    public bool test;
    public AnimeCountroler FirstScensMask;
    public GameObject FirstScensObject;
    public bool FirstScensEnd;
    public Rect FirstScensButtonArea;
    public Sprite[] MouseNormal;
    public Sprite[] MouseGetIt;
    public DialogManager DiaManager;
    public CharacterSpriteCountroler characterCountroler;
    public Sprite[] Characters;
    public AnimeCountroler AllItem;
    public HitManager hitManager;
    public AnimeCountroler HPBar;
    public HPManager HPManager;
    public Transform[] Balls1;
    public GameObject[] BallPerfabs;
    public GameObject TimeLinePrefab;
    public CreateBallItem[] ballList;
    public GameObject EmptyPrefab;


    public AnimeCountroler Character;
    public AnimeCountroler Box;
    public MouseBox DiaBox;

    private int poleCount = 0;
    private Transform TimeLineContainer;

    public void CleanTimeLine()
    {
        Destroy(TimeLineContainer.gameObject);
        TimeLineContainer = Instantiate(EmptyPrefab).transform;
    }

    public void HitPole()
    {
        poleCount++;
        if(poleCount == 3)
        {
            StartConverSation2();
        }
    }


    public TimeLine CreateTimeLine()
    {
        TimeLine t = Instantiate(TimeLinePrefab).transform.GetComponent<TimeLine>();
        t.Init();
        t.finalTime = 200;
        t.transform.parent = TimeLineContainer;
        return t;
    }


    public BallCountroler RandomBall(int i)
    {
        return CreateBallAt(i, new Vector2(Random.Range(-2.4f, 2.4f), Random.Range(-1.2f, 1.2f)));
    }

    private BallCountroler CreateBallAt(int i, Vector2 p,bool sound = true)
    {
        Transform t = Instantiate(BallPerfabs[i]).transform;
        t.position = p;
        particalManager.GlobalManager.BoomParticalBust(30, p, t.GetComponent<BallCountroler>().MainColor, 1.5f, true);
        particalManager.GlobalManager.CreateRoundPartical(p, 1f, t.GetComponent<BallCountroler>().MainColor, true);
        if (sound)
        {
            SoundManager.Play("newball");
        }
        return t.GetComponent<BallCountroler>();

    }

    private void CreateNBall(int[] t,Vector2 p,float r,float dAngle,float time)
    {
        TimeLine tem = CreateTimeLine();
        int n = t.Length;
        float angle = Mathf.PI * 2 / n;
        
        for(int i = 0; i < n; i++)
        {
            int bt = t[i];
            float ag = dAngle + i * angle;
            tem.AddCallBack(time * i, () => { CreateBallAt(bt,p + new Vector2(Mathf.Sin(ag),Mathf.Cos(ag))*r); });
        }
    }


    public void ShowCharacter(UF.AnimeCallback c = null)
    {
        Character.positionY.Init = -4f;
        Character.positionY.End = -0.9f;
        Character.StartAnime(c);
        Character.positionY.DelayCallback = () => { SoundManager.Play("CharacterShow"); };
    }

    public void ShowBox(UF.AnimeCallback c = null)
    {
        Box.positionY.Init = -3;
        Box.positionY.End = -1.84f;
        DiaManager.clean();        Box.StartAnime(c);
    }

    public void HideCharacter(UF.AnimeCallback c = null)
    {
        Character.positionY.End= -4f;
        Character.positionY.Init = -0.9f;
        Character.StartAnime(c);
        Character.positionY.DelayCallback = () => { SoundManager.Play("CharacterShow"); };
    }
    public void HideBox(UF.AnimeCallback c = null)
    {
        Box.positionY.End = -3;
        Box.positionY.Init = -1.84f;
        Box.StartAnime(c);
    }


    public void DestoryFirstScens()
    {
        Destroy(FirstScensObject);
    }
    private void UpdateFirstScens()
    {
        if (!FirstScensEnd)
        {
            if (UF.PointInRect( MouseManager.MousePosition(),FirstScensButtonArea))
            {
                MouseManager.GlobleManager.MouseSprites = MouseGetIt;
                if (Input.GetMouseButtonDown(0))
                {
                    if (!test)
                    {
                        FirstScensMask.StartAnime(() => { FirstConversation(); DestoryFirstScens(); });
                    }
                    else
                    {
                        FirstScensMask.StartAnime(() => { FastTest(); });
                    }
                    FirstScensEnd = true;
                    MouseManager.GlobleManager.MouseSprites = MouseNormal;
                    FirstScensMask.transform.position = MouseManager.MousePosition();
                    particalManager.GlobalManager.BoomParticalBust(30, MouseManager.MousePosition(), Color.white,2, true);
                    SoundManager.Play("start");

                }
            }
            else
            {
                MouseManager.GlobleManager.MouseSprites = MouseNormal;
            }
        }


    }

    public void FirstConversation()
    {
        ShowCharacter();
        ShowBox(() => { DiaManager.ShowContent("Hello, player.\nThank you for coming to play with me.", () =>
        {
            characterCountroler.ChangeSprite(Characters[0]);
            AllItem.StartAnime();
            DiaManager.ShowContent("I found this thing in the basement. I haven't quite\nfigured out how it works, but it's barely playable now.", () =>
            {
                characterCountroler.ChangeSprite(Characters[1]);
                DiaManager.ShowContent("Here's your cue.");
                characterCountroler.touchCallback = () =>
                {
                    characterCountroler.ChangeSprite(Characters[2]);
                    DiaManager.ShowContent("I'll go get more balls, give me a second.", () =>
                    {
                        EndConversation1();
                    });
                };
            });
        }); });
    }

    public void EndConversation1()
    {
        HideBox();
        HideCharacter();
        hitManager.active = true;
    }

    public void AddBalls1()
    {
        foreach(Transform t in Balls1)
        {
            Vector2 tem = t.transform.position;
            tem.x += 9;
            t.transform.position = tem;
        }
        particalManager.GlobalManager.BoomParticalBust(50, new Vector2(1, 0.5f), Color.white, 2,true);
        SoundManager.Play("add1");
    }

    public void StartConverSation2()
    {
        hitManager.tur = false;
        hitManager.DeActive();
        DiaManager.clean();
        ShowCharacter(); 
        ShowBox(() =>
        {
            characterCountroler.ChangeSprite(Characters[3]);
            DiaManager.ShowContent("More balls!", () =>
            {
                characterCountroler.ChangeSprite(Characters[0]);
                AddBalls1();
                DiaManager.ShowContent("There were a lot of balls, but I couldn't find them\nanymore, so I put some weird stuff in there too.", () =>
                {
                    characterCountroler.ChangeSprite(Characters[5]);
                    DiaManager.ShowContent("Oh, I forgot to turn on the cue for you. \nLet me hold it for a while.", () =>
                    {
                        characterCountroler.ChangeSprite(Characters[4]);
                        DiaManager.ShowContent("WHAT THE.. I thought there was a..\nOh, figure out.", () =>
                        {
                            characterCountroler.ChangeSprite(Characters[1]);
                            DiaManager.ShowContent("Here you are, Enjoy the new feature");
                            characterCountroler.touchCallback = () =>
                            {
                                EndConversation2();
                            };
                        });
                    });
                });
            });
        });
        
    }

    public void EndConversation2()
    {
        HideBox();
        HideCharacter();
        hitManager.active = true;
        HPBar.StartAnime(HPManager.StartPartical);
        SoundManager.Play("m");
        startCreateBall();
        //StartFinallRound1();

    }

    public void startCreateBall()
    {
        //TimeLine tem = CreateTimeLine();
        //tem.AddCallBack(20, () =>{

        // ChaPutBalls(new int[4] { 0, 0, 0 ,1}, new Vector2(-2f, 0.156f), 0.3f, 0);
        //});
        //tem.AddCallBack(40, () =>{ ChaPutBalls(new int[5] { 5, 6, 7, 7, 1 }, new Vector2(2f, 0.156f), 0.5f, 0); });
        //tem.AddCallBack(80, () => { ChaPutBalls(new int[5] { 8, 9, 10, 11, 12 }, new Vector2(0, 0.156f), 0.25f, 0); });

        //BallCounter.SetCallBackM(0, () => { ChaPutBalls(new int[5] { 4, 4, 20, 20, 21 }, new Vector2(0, 0.156f), 0.3f, 1, false); });
        //BallCounter.SetCallBackM(0, () => { ChaPutBalls(new int[5] { 17, 17, 18, 18, 19 }, new Vector2(-1.3f, 0.156f), 1.3f, 3); });



        BallCounter.SetCallBackM(2, () => { ChaPutBalls(new int[4] { 7, 7, 7, 1 }, new Vector2(-2f, 0.156f), 0.3f, 0); });
        BallCounter.SetCallBackM(7, () => { ChaPutBalls(new int[5] { 5, 6, 7, 7, 1 }, new Vector2(2f, 0.156f), 0.5f, 0); });
        BallCounter.SetCallBackM(14, () => { ChaPutBalls(new int[5] { 8, 9, 10, 11, 12 }, new Vector2(0, 0.156f), 0.25f, 0); });
        BallCounter.SetCallBackM(20, () =>
        {
            ShowCharacter();
            ShowBox(() =>
            {
                DiaManager.ShowContent("I have found a kind of ball that can move on its own.          \nThis might be more interesting", () =>
                {
                    HideBox();
                    ChaPutBalls(new int[4] { 13, 14, 13, 15 }, new Vector2(0, 0.156f), 0.25f, 1, false);
                });
            });
        });
    }


    public void startCreateBall2()
    {
        ChaPutBalls(new int[8] { 0, 1, 1, 2, 7, 13, 8, 5 }, new Vector2(0, 0.156f), 1, 0, true,true
            , () => {

                BallCounter.SetCallBackM(12, () => { ChaPutBalls(new int[5] { 14, 16, 16, 16, 16 }, new Vector2(-0.3f, 0.156f), 0.7f, 2); });
                BallCounter.SetCallBackM(35, () => { ChaPutBalls(new int[5] { 17, 17, 18, 18, 19 }, new Vector2(0f, 0.156f), 1.3f, 3); });
                BallCounter.SetCallBackM(50, () => 
                {
                    ShowCharacter();
                    ShowBox(() =>
                    {
                        DiaManager.ShowContent("It would be more interesting \nif there was a shock wave", () =>
                        {
                            HideBox();
                            ChaPutBalls(new int[5] { 4, 4,20, 20,21 }, new Vector2(0, 0.156f), 0.25f, 1, false);
                        });
                    });
                });

            });
    }

    public void startCreateBall3()
    {
        ChaPutBalls(new int[9] { 0, 1, 1, 2, 7, 13, 8, 5 ,4}, new Vector2(0, 0.156f), 0.6f, 0, true,true, () =>
        {
            BallCounter.SetCallBackM(10, () => { ChaPutBalls(new int[5] { 8, 9, 10, 11, 12 }, new Vector2(-1f, 0.156f), 0.3f, 2); });
            BallCounter.SetCallBackM(20, () => { ChaPutBalls(new int[8] {0,17,0,18,0,17,0,18}, new Vector2(1f, 0.156f), 0.5f, 2); });
            BallCounter.SetCallBackM(30, () => { ChaPutBalls(new int[20] { 0,0,1,1,6,6,6,7,7,1,1,6,6,7,16,16,16,2,19,19}, new Vector2(0, 0.156f), 1f, 2); });
            BallCounter.SetCallBackM(95, startCreateBall4);
        });
    }


    public void startCreateBall4()
    {
        ShowCharacter();
        ShowBox(() =>
        {
            DiaManager.ShowContent("Feel the chaos!", () =>
            {
                HideBox();
                ChaPutBalls(new int[19] { 20,15,8,9,20,15,21,16,21,10, 17, 18, 14, 20, 21, 11, 12, 18, 17 }, new Vector2(0, 0.156f), 0.9f, 1, false);
                ChaPutBalls(new int[9] { 17,18,14,20,21,11,12,18,17 }, new Vector2(0, 0.156f), 0.5f, 1, false,true,() =>
                {
                    BallCounter.SetCallBackM(38, () => 
                    { 
                        ChaPutBalls(new int[10] { 8,9,10,11,12,8,9,10,11,12 }, new Vector2(-1.3f, 0.156f), 0.5f, 2);
                        ChaPutBalls(new int[4] {4,20,16,16}, new Vector2(-1.3f, 0.156f), 0.5f, 2);
                        ChaPutBalls(new int[10] { 17,18,17,18,17,18,17,18,17,18 }, new Vector2(1.3f, 0.156f), 0.5f, 2);
                        ChaPutBalls(new int[4] { 20, 4, 16, 16 }, new Vector2(1.3f, 0.156f), 0.5f, 2);
                    });
                    BallCounter.SetCallBackM(120, () =>
                    {
                        TimeLine tem = CreateTimeLine();
                        tem.AddCallBack(0, () =>
                        {
                            ChaPutBalls(new int[10] { 8, 9, 10, 11, 12, 8, 9, 10, 11, 12 }, new Vector2(-1.3f, 0.156f), 1f, 2,true,false,null);
                        });
                        tem.AddCallBack(0.1f, () =>
                        {
                            ChaPutBalls(new int[10] { 8, 9, 10, 11, 12, 8, 9, 10, 11, 12 }, new Vector2(1.3f, 0.156f), 1f, 2, false,false, null);
                        });
                        tem.AddCallBack(0.2f, () =>
                        {
                            ChaPutBalls(new int[6] { 20, 21, 20, 21, 20, 21 }, new Vector2(1.3f, 0.156f), 1f, 2, false, true,null);
                        });
                    });
                    BallCounter.SetCallBackM(250, () => {
                        startCreateBall5();
                    });
                });
            });
        });
    }

    public void startCreateBall5()
    {
        ShowCharacter();
        ShowBox(() =>
        {
            DiaManager.ShowContent("real bomb!!          ", true, () =>
            {
                ChaPutBalls(new int[20] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, new Vector2(0, 0.156f), 1.1f, 0, false,true, () =>
                {
                    TimeLine tem = CreateTimeLine();
                    tem.AddCallBack(6, StartFinallRound1);
                });
            });
        });
        
    }
    


        public void ChaPutBalls(int[] t,Vector2 p,float r,float dAngle,bool show = true,bool hide = false,UF.AnimeCallback cb = null)
    {
        TimeLine tem = CreateTimeLine();
        tem.AddCallBack(0, () =>
        {
            if (show)
            {
                ShowCharacter();
            }
            characterCountroler.ChangeSprite(Characters[3]);
            characterCountroler.ShowBalls(t);
        });

        tem.AddCallBack(1, () =>
        {
            characterCountroler.ChangeSprite(Characters[0]);
            CreateNBall(t, p, r, dAngle, 0);
        });
        tem.AddCallBack(1, cb);

        tem.AddCallBack(1.3f, () =>
        {
            HideCharacter();
        });

        
    }


    public void StartFinallRound1()
    {
        ShowCharacter();
        characterCountroler.ChangeSprite(Characters[0]);
        ShowBox(() =>
        {
            
            DiaManager.ShowContent("I feel like it's going in the wrong direction.", () =>
            {
                characterCountroler.ChangeSprite(Characters[2]);
                DiaManager.ShowContent("Let's play something different!", () =>
                {
                    characterCountroler.ChangeSprite(Characters[0]);
                    DiaManager.ShowContent("Get ready! It will be tough.",()=>
                    {
                        HideBox();
                        HideCharacter();
                        Crazy1();
                    });
                    
                    hitManager.BallDeadCallBack = ReCoverFromC1_1;
                });
            });
        });
        
    }



    public void ReCoverFromC1_1()
    {
        CleanTimeLine();
        ShowCharacter();
        ShowBox(() =>{
            DiaManager.ShowContent("This may be a little difficult.", () =>
            {
                HideBox();
                HideCharacter();
                Crazy1();
            });
        });
        
    }
    public void Crazy1()
    {
        TimeLine tem = CreateTimeLine();
        tem.AddCallBack(1, () => { CreateDArea(new Vector2(0, 0.156f), 2); });
        tem.AddCallBack(4, () => { Crazy1_1(Vector2.right*-0.5f); });
        tem.AddCallBack(5f, () => { Crazy1_1(Vector2.right * 1.11f); });
        tem.AddCallBack(9f, () => { Crazy1_1(Vector2.right * 2.73f); });
        tem.AddCallBack(10f, () => { Crazy1_1(Vector2.right * 1.11f); });
        tem.AddCallBack(14f, Crazy1_2);
    }

    public void Crazy1_1(Vector2 offSet)
    {
        TimeLine c1 = CreateTimeLine();
        c1.AddCallBack(0.1f, () => { CreateDArea(new Vector2(-1.67f,1.02f)+offSet,0); });
        c1.AddCallBack(0.2f, () => { CreateDArea(new Vector2(-0.56f, 1.02f) + offSet, 0); });
        c1.AddCallBack(0.3f, () => { CreateDArea(new Vector2(0, 0.156f) + offSet, 0); });
        c1.AddCallBack(0.4f, () => { CreateDArea(new Vector2(-0.56f, -0.7f) + offSet, 0); });
        c1.AddCallBack(0.5f, () => { CreateDArea(new Vector2(-1.67f, -0.7f) + offSet, 0); });
        c1.AddCallBack(0.6f, () => { CreateDArea(new Vector2(-2.23f, 0.156f) + offSet, 0); });
        c1.AddCallBack(0.7f, () => { CreateDArea(new Vector2(-1.11f, 0.156f) + offSet, 2); });
        
    }
    public void Crazy1_2()
    {
        TimeLine c1 = CreateTimeLine();
        c1.AddCallBack(0, () => { Crazy1_2_1 (new Vector2(0, -0.95f),1); });
        c1.AddCallBack(0, () => { Crazy1_2_1(new Vector2(0, 1.26f),-1); });
        c1.AddCallBack(1.6f, () => { CreateDArea(new Vector2(-2.6f, 0.156f), 2); });
        c1.AddCallBack(1.6f, () => { CreateDArea(new Vector2(2.6f, 0.156f), 2); });
        c1.AddCallBack(1.3f, () => { CreateDArea(new Vector2(-1.3f, 0.156f), 2); });
        c1.AddCallBack(1.3f, () => { CreateDArea(new Vector2(1.3f, 0.156f), 2); });
        c1.AddCallBack(1f, () => { CreateDArea(new Vector2(0f, 0.156f), 2); });

    }

    public void Crazy1_2_1(Vector2 offSet,float scale)
    {
        TimeLine c1 = CreateTimeLine();
        for(int i = 0; i < 11; i++)
        {
            int tt = i;
            c1.AddCallBack(0.06f*i, () => { CreateDArea((new Vector2((-2.5f+tt*0.5f)*scale, 0f) + offSet), 0); });
        }
    }






    public void CreateDArea(Vector2 position, int type)
    {
        Transform tem = Instantiate(DAreas[type]).transform;
        tem.transform.position = position;
        tem.parent = BackEffect.BackGroundTransform();
        SoundManager.Play("b5");
    }

    public GameObject[] DAreas;

    // Start is called before the first frame update
    void Start()
    {
        DiaBox.ClickFunction = (Vector2 v) => { DiaManager.Clicked(); return true; };
        TimeLineContainer = Instantiate(EmptyPrefab).transform;
        //FastTest();
    }

    public void FastTest()
    {
        AllItem.StartAnime();
        hitManager.active = true;
        hitManager.tur = false;
        hitManager.PushAbility = true;
        hitManager.MoveAbility = true;
        HPBar.StartAnime(HPManager.StartPartical);
        ReCoverFromC1_1();
        BallCounter.GlobleManager.PushBallCount = 10;
        BallCounter.GlobleManager.MoveBallCount = 10;
        //startCreateBall4();
        poleCount = 5;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFirstScens();
    }

    

}
[System.Serializable]
public class CreateBallItem
{
    public float time;
    public int type;
}
