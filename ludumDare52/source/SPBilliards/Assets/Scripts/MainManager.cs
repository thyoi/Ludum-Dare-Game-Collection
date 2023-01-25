using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainManager : MonoBehaviour
{
    public bool test;
    public Transform TestPosition;
    public BallCountroler Mainball;
    public Transform MainCamera;
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
    public ProcessBar ProBar;
    public AnimeCountroler ProBarAnime;
    public GoalCountroler GoalC;
    public Transform[] Balls1;
    public GameObject[] BallPerfabs;
    public GameObject TimeLinePrefab;
    public CreateBallItem[] ballList;
    public GameObject EmptyPrefab;
    public SansSkill1 SansSkill;
    public Color SansColor;



    public AnimeCountroler Character;
    public AnimeCountroler Box;
    public AnimeCountroler Scans;
    public MouseBox DiaBox;
    public Wiggle CameraShake;

    public SansCountroler SansC;

    public GameObject[] SansPowProfeb;
    public SpriteRenderer[] Hint1;
    public Transform[] BeyBey;


    private int poleCount = 0;
    private Transform TimeLineContainer;

    public void showHint1()
    {
        foreach(SpriteRenderer i in Hint1)
        {
            UF.SetApha(i, 1);
        }
    }

    public void HideHint1()
    {
        foreach (SpriteRenderer i in Hint1)
        {
            UF.SetApha(i, 0);
        }
    }



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
        if (UF.PointInRect(p, new Rect(-2.77f, -1.16f, 5.55f, 2.67f)))
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
        return null;
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
        DiaManager.sound = "char";
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

    public void ShowSans(UF.AnimeCallback c = null)
    {
        DiaManager.sound = "sans0";
        Scans.positionY.Init = -4f;
        Scans.positionY.End = -1.3f;
        Scans.StartAnime(c);
        Scans.positionY.DelayCallback = () => { SoundManager.Play("sansShow"); };
    }

    public void HideSans(UF.AnimeCallback c = null)
    {
        Scans.positionY.End = -4f;
        Scans.positionY.Init = -1.3f;
        Scans.StartAnime(c);
        Scans.positionY.DelayCallback = () => { SoundManager.Play("sansShow"); };
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
        ProBarAnime.StartAnime();
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

        

        BallCounter.SetCallBackM(2, () => { ChaPutBalls(new int[4] { 7, 7, 7, 1 }, new Vector2(-2f, 0.156f), 0.3f, 0);ProBar.NextStage(); GoalC.InitPointCount(5); });
        BallCounter.SetCallBackM(7, () => { ChaPutBalls(new int[5] { 5, 6, 7, 7, 1 }, new Vector2(2f, 0.156f), 0.5f, 0); ProBar.NextStage(); GoalC.InitPointCount(7); });
        BallCounter.SetCallBackM(14, () => { ChaPutBalls(new int[5] { 8, 9, 10, 11, 12 }, new Vector2(0, 0.156f), 0.25f, 0); ProBar.NextStage(); GoalC.InitPointCount(6); });
        BallCounter.SetCallBackM(20, () =>
        {
            GoalC.InitSPBall(4);
            ShowCharacter();
            ProBar.NextStage();
            ShowBox(() =>
            {
                DiaManager.ShowContent("I have found a kind of ball that can move on its own.          \nThis might be more interesting", () =>
                {
                    HideBox();
                    ChaPutBalls(new int[4] { 13, 14, 13, 15 }, new Vector2(0, 0.156f), 0.25f, 1, false);
                });
            });
        });
        GoalC.InitPointCount(3);
    }


    public void startCreateBall2()
    {
        ProBar.NextStage();
        ChaPutBalls(new int[8] { 0, 1, 1, 2, 7, 13, 8, 5 }, new Vector2(0, 0.156f), 1, 0, true,true
            , null);
        BallCounter.SetCallBackM(12, () => { ChaPutBalls(new int[5] { 14, 16, 16, 16, 16 }, new Vector2(-0.3f, 0.156f), 0.7f, 2); ProBar.NextStage(); GoalC.InitPointCount(22); });
        BallCounter.SetCallBackM(35, () => { ChaPutBalls(new int[5] { 17, 17, 18, 18, 19 }, new Vector2(0f, 0.156f), 1.3f, 3); ProBar.NextStage(); GoalC.InitPointCount(15); });
        BallCounter.SetCallBackM(50, () =>
        {
            GoalC.InitSPBall(3);
            ProBar.NextStage();
            ShowCharacter();
            ShowBox(() =>
            {
                DiaManager.ShowContent("It would be more interesting \nif there was a shock wave", () =>
                {
                    HideBox();
                    ChaPutBalls(new int[5] { 4, 4, 20, 20, 21 }, new Vector2(0, 0.156f), 0.25f, 1, false);
                });
            });
        });
        GoalC.InitPointCount(13);
    }

    public void startCreateBall3()
    {
        
        ProBar.NextStage();
        ChaPutBalls(new int[9] { 0, 16, 16, 2, 7, 13, 8, 5 ,4}, new Vector2(0, 0.156f), 0.6f, 0, true,true,null);
        BallCounter.SetCallBackM(22, () => { ChaPutBalls(new int[5] { 8, 9, 10, 11, 12 }, new Vector2(-1f, 0.156f), 0.3f, 2); ProBar.NextStage(); GoalC.InitPointCount(12); });
        BallCounter.SetCallBackM(34, () => { ChaPutBalls(new int[8] { 0, 17, 16, 18, 0, 17, 0, 18 }, new Vector2(1f, 0.156f), 0.5f, 2); ProBar.NextStage(); });
        BallCounter.SetCallBackM(60, () => { ChaPutBalls(new int[20] { 0, 0, 1, 1, 6, 6, 6, 7, 7, 1, 1, 6, 6, 7, 16, 16, 16, 2, 19, 19 }, new Vector2(0, 0.156f), 1f, 2); ProBar.NextStage(); GoalC.InitPointCount(26); });
        BallCounter.SetCallBackM(130, startCreateBall4);
        GoalC.InitPointCount(23);
    }


    public void startCreateBall4()
    {
       
        ProBar.NextStage();
        ShowCharacter();
        ShowBox(() =>
        {
            DiaManager.ShowContent("Big Harvest!", () =>
            {
                HideBox();
                ChaPutBalls(new int[19] { 20,15,8,9,20,15,21,16,21,10, 17, 18, 14, 20, 21, 11, 12, 18, 17 }, new Vector2(0, 0.156f), 0.9f, 1, false);
                ChaPutBalls(new int[9] { 17,18,14,20,21,11,12,18,17 }, new Vector2(0, 0.156f), 0.5f, 1, false,true,() =>
                {
                    BallCounter.SetCallBackM(250, () => {
                        ProBar.NextStage();
                        GoalC.InitPointCount(130);
                        startCreateBall5();
                    });
                });
            });
        });
        BallCounter.SetCallBackM(38, () =>
        {
            ChaPutBalls(new int[10] { 8, 9, 10, 11, 12, 8, 9, 10, 11, 12 }, new Vector2(-1.3f, 0.156f), 0.5f, 2);
            ChaPutBalls(new int[4] { 4, 20, 16, 16 }, new Vector2(-1.3f, 0.156f), 0.5f, 2);
            ChaPutBalls(new int[10] { 17, 18, 17, 18, 17, 18, 17, 18, 17, 18 }, new Vector2(1.3f, 0.156f), 0.5f, 2);
            ChaPutBalls(new int[4] { 20, 4, 16, 16 }, new Vector2(1.3f, 0.156f), 0.5f, 2);
            ProBar.NextStage();
            GoalC.InitPointCount(39);
        });
        BallCounter.SetCallBackM(120, () =>
        {
            ProBar.NextStage();
            GoalC.InitPointCount(82);
            TimeLine tem = CreateTimeLine();
            tem.AddCallBack(0, () =>
            {
                ChaPutBalls(new int[10] { 8, 9, 10, 11, 12, 8, 9, 10, 11, 12 }, new Vector2(-1.3f, 0.156f), 1f, 2, true, false, null);
            });
            tem.AddCallBack(0.1f, () =>
            {
                ChaPutBalls(new int[10] { 8, 9, 10, 11, 12, 8, 9, 10, 11, 12 }, new Vector2(1.3f, 0.156f), 1f, 2, false, false, null);
            });
            tem.AddCallBack(0.2f, () =>
            {
                ChaPutBalls(new int[6] { 20, 21, 20, 21, 20, 21 }, new Vector2(1.3f, 0.156f), 1f, 2, false, true, null);
            });
        });
        
        GoalC.InitPointCount(70);
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
                    HideBox();
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
            if (t.Length >= 4)
            {
                characterCountroler.ShowBalls(t);
            }
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
        GoalC.SetSpell("Stay alive", Color.white);
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
                    
                    //hitManager.BallDeadCallBack = ReCoverFromC1_1;
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
        tem.AddCallBack(1, () => { CreateDArea(new Vector2(0, 0.156f), 1); });
        tem.AddCallBack(4, () => { Crazy1_1(Vector2.right*-0.5f); });
        tem.AddCallBack(5f, () => { Crazy1_1(Vector2.right * 1.11f); });
        tem.AddCallBack(9f, () => { Crazy1_1(Vector2.right * 2.73f); });
        tem.AddCallBack(10f, () => { Crazy1_1(Vector2.right * 1.11f); });
        tem.AddCallBack(14f, Crazy1_2);
        tem.AddCallBack(18.2f, Crazy1_3);
        tem.AddCallBack(24, InitCrazy2);
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
        c1.AddCallBack(0.7f, () => { CreateDArea(new Vector2(-1.11f, 0.156f) + offSet, 1); });
        
    }
    public void Crazy1_2()
    {
        TimeLine c1 = CreateTimeLine();
        c1.AddCallBack(0, () => { Crazy1_2_1 (new Vector2(0, -0.95f),1); });
        c1.AddCallBack(0, () => { Crazy1_2_1(new Vector2(0, 1.26f),-1); });
        c1.AddCallBack(0.6f, () => { CreateDArea(new Vector2(-2.6f, 0.156f), 2); });
        c1.AddCallBack(0.6f, () => { CreateDArea(new Vector2(2.6f, 0.156f), 2); });
        c1.AddCallBack(0.3f, () => { CreateDArea(new Vector2(-1.3f, 0.156f), 2); });
        c1.AddCallBack(0.3f, () => { CreateDArea(new Vector2(1.3f, 0.156f), 2); });
        c1.AddCallBack(0f, () => { CreateDArea(new Vector2(0f, 0.156f), 2); });

    }

    public void Crazy1_3()
    {
        ChaPutBalls(new int[1] { 23 }, new Vector2(-2, 0.156f), 0, 0);
        ChaPutBalls(new int[4] { 22,22,22,22},new Vector2(-2,0.156f),0.3f,1.1f);
        ChaPutBalls(new int[1] { 23 }, new Vector2(2, 0.156f), 0, 0);
        ChaPutBalls(new int[4] {  22, 22, 22, 22 }, new Vector2(2, 0.156f), 0.3f, -1.1f);
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

    public Color DeathSwamp;

    public void InitCrazy2()
    {
        ProBar.NextStage();
        GoalC.SetSpell("Swamp of Death", DeathSwamp);
        hitManager.BallDeadCallBack = null;
        ShowCharacter();
        ShowBox(() =>
        {
            characterCountroler.ChangeSprite(Characters[2]);
            DiaManager.ShowContent("I call this Spell.\nA gorgeous and well-organized attack.", () =>
            {
                characterCountroler.ChangeSprite(Characters[0]);
                DiaManager.ShowContent("Blasting through a bunch of chaotic balls is fun.\nBut a well designed spell will do a better job.", () =>
                {
                    DiaManager.ShowContent("At least I think so.", () =>
                    {
                        DiaManager.ShowContent("If you die in a spell, the spell starts over.\nIt's necessary to ensure continuity of spells.", () =>
                        {
                            characterCountroler.ChangeSprite(Characters[2]);
                            DiaManager.ShowContent("Don't worry, every time you get die in a spell, \nI'll increase your charge speed.", () =>
                            {
                                characterCountroler.ChangeSprite(Characters[0]);
                                DiaManager.ShowContent("Good spells are always at the end,\n so please stick around until the end.", () =>
                                {
                                    DiaManager.ShowContent("Okay, let's move on.\nThe next spell is called.  .  .  .  .  .  .  .", () =>
                                    {
                                        DiaManager.DefaultColor = DeathSwamp;
                                        characterCountroler.ChangeSprite(Characters[6]);
                                        DiaManager.ShowContent("Escape from the Swamp of Death!", () =>
                                        {
                                            HideBox(Crazy2);
                                            HideCharacter();
                                        });
                                        
                                    });
                                });
                            });
                        });
                    });
                });
            });
        });
    }


    public void RecoverFromCrazy2()
    {
        //KillManager.CRK(Vector2.zero, 20, new Color(0, 0, 0, 0));
        ShowCharacter();
        CleanTimeLine();
        HPManager.recoverSpeed += 1;
        ShowBox(() =>
        {
            DiaManager.ShowContent("Don't give up,Let's do it again", () =>
            {
                Crazy2();
                HideBox();
                HideCharacter();
            });
        });
    }

    public Transform[] Crazy2Ball;
    public Transform[] Crazy2DA;
    public Transform[] Crazy2DA2;
    public Transform[] Crazy2DA3;

    public void Crazy2()
    {
        hitManager.BallDeadCallBack = ()=> { KillManager.CRK(Vector2.zero, 20, new Color(100, 0, 0, 0)); RecoverFromCrazy2(); };
        TimeLine tem = CreateTimeLine();
        tem.AddCallBack(0, Crazy2_1);
        tem.AddCallBack(5.6f, Crazy2_2);
        tem.AddCallBack(15f, Crazy2_4);
        tem.AddCallBack(22f, Crazy2_5);
        tem.AddCallBack(32.5f, Crazy2_3);
        tem.AddCallBack(42.5f, InitCrazy3);
    }

    public void Crazy2_1()
    {
        TimeLine tem = CreateTimeLine();
        for(int i = 0; i < 7; i++)
        {
            int ti = i;
            Vector2 tp = Vector2.left * (0.6f * i - 2.5f) + Vector2.up * ((i % 2 == 0) ? 0.125f:-0.125f) ;
            if (i != 3)
            {
                tem.AddCallBack(0.3f * ti, () =>
                {
                    Crazy2_1_1(tp, (ti % 2 == 0) ? 0.7f : -0.7f);
                });
            }
        }

    }
    public void Crazy2_1_1(Vector2 position,float scale)
    {
        TimeLine tem = CreateTimeLine();
        for(int i = 0; i < 5; i++)
        {
            int ti = i;
            Vector2 tp = position + Vector2.up * (i - 2) * scale;
            tem.AddCallBack(0.12f * ti, () =>
            {
                CreateDArea(tp, 0);
            });


        }
    }

    public void Crazy2_2()
    {
        TimeLine tem = CreateTimeLine();
        tem.AddCallBack(0, ()=>{ Crazy2_2_1(24); });
        tem.AddCallBack(1.5f, Crazy2_2_2);
        tem.AddCallBack(5 ,Crazy2_2_3);
    }

    public void Crazy2_2_1(int BallType)
    {
        TimeLine tem = CreateTimeLine();
        for(int i = 0; i < Crazy2Ball.Length; i++)
        {
            int ti = i;
            tem.AddCallBack(0.05f * ti, () =>{ CreateBallAt(BallType, Crazy2Ball[ti].localPosition);
            });
        }
    }

    public void Crazy2_2_2()
    {
        TimeLine tem = CreateTimeLine();
        for (int i = 0; i < Crazy2DA.Length; i++)
        {
            int ti = i;
            tem.AddCallBack(0.1f * ti, () => {
                CreateDArea(Crazy2DA[ti].localPosition, 0);
            });
        }
    }


    public void Crazy2_2_3()
    {
        TimeLine tem = CreateTimeLine();
        for (int i = 0; i < Crazy2DA2.Length; i++)
        {
            int ti = i;
            tem.AddCallBack(0.1f * ti, () => {
                CreateDArea(Crazy2DA2[ti].localPosition, 1);
            });
        }
    }
    public Transform[] Crazy2BallF;

    public void Crazy2_3()
    {
        TimeLine tem = CreateTimeLine();
        tem.AddCallBack(0, Crazy2_3_1);
        tem.AddCallBack(0, Crazy2_3_3);
        tem.AddCallBack(1.6f, Crazy2_3_2);
    }

    public void Crazy2_3_1()
    {
        TimeLine tem = CreateTimeLine();
        tem.AddCallBack(3.1f, () =>
        {
            ChaPutBalls(IntList(3, 4), new Vector2(1.4f, -0.7f), 0.3f, 0);
        });
        tem.AddCallBack(0f, () =>
        {
            CreateNBall(IntList(24, 22), new Vector2(1.4f, -0.7f), 1.0f, Mathf.PI,0.04f);
        });
    }


    public void Crazy2_3_2()
    {
        TimeLine tem = CreateTimeLine();
        for (int i = 0; i < Crazy2DA3.Length; i++)
        {
            int ti = i;
            tem.AddCallBack(0.16f * ti, () => {
                CreateDArea(Crazy2DA3[ti].localPosition, 2);
            });
        }
    }

    public void Crazy2_3_3()
    {
        TimeLine tem = CreateTimeLine();
        for (int i = 0; i < Crazy2BallF.Length; i++)
        {
            int ti = i;
            tem.AddCallBack(0.03f * ti, () => {
                CreateBallAt(24, Crazy2BallF[ti].localPosition);
            });
        }
    }


    public void Crazy2_4()
    {
        TimeLine tem = CreateTimeLine();
        tem.AddCallBack(0, Crazy2_4_1);
        tem.AddCallBack(0.5f, () => { CreateBallAt(3, new Vector2(2.5f, 0.156f)); });
        tem.AddCallBack(1.4f, Crazy2_4_2);

    }

    public Transform[] Crazy2Ball2;
    public Transform[] Crazy2AD4_1;
    public Transform[] Crazy2AD4_2;


    public void Crazy2_4_1()
    {
        TimeLine tem = CreateTimeLine();
        for (int i = 0; i < Crazy2Ball2.Length; i++)
        {
            int ti = i;
            tem.AddCallBack(0.04f * ti, () => {
                CreateBallAt(24, Crazy2Ball2[ti].localPosition);
            });
        }
    }

    public void Crazy2_4_2()
    {
        TimeLine tem = CreateTimeLine();
        for (int i = 0; i < Crazy2AD4_1.Length; i++)
        {
            int ti = i;
            tem.AddCallBack(0.11f * ti, () => {
                CreateDArea(Crazy2AD4_1[ti].localPosition, 1);
            });
        }
    }

    public void Crazy2_5()
    {
        TimeLine tem = CreateTimeLine();
        tem.AddCallBack(0f, Crazy2_5_1);
        tem.AddCallBack(0.2f, () => { CreateNBall(IntList(25, 3), new Vector2(-2.4f, 0.156f), 0.2f, -Mathf.PI / 6, 0); });
        tem.AddCallBack(1.1f, () => { CreateDArea(new Vector2(-2.4f, 0.156f),0); });
        tem.AddCallBack(2.9f, Crazy2_5_2);

    }
    public void Crazy2_5_1()
    {
        TimeLine tem = CreateTimeLine();
        for (int i = 0; i < Crazy2Ball2.Length; i++)
        {
            int ti = i;
            Vector2 tp = Crazy2Ball2[ti].localPosition;
            tp.x = -tp.x;
            tem.AddCallBack(0.03f * ti, () => {
                CreateBallAt(24, tp);
            });
        }
    }

    public void Crazy2_5_2()
    {
        TimeLine tem = CreateTimeLine();
        for (int i = 0; i < Crazy2AD4_2.Length; i++)
        {
            int ti = i;
            tem.AddCallBack(0.18f * ti, () => {
                CreateDArea(Crazy2AD4_2[ti].localPosition, 1);
            });
        }
    }

    public void InitCrazy3()
    {
        GoalC.SetSpell("D:E:T:E:R:M:I:N:A:T:I:O:N:", DeterColor);
        ProBar.NextStage();
        hitManager.BallDeadCallBack = null;
        characterCountroler.ChangeSprite(Characters[6]);
        ShowCharacter();
        ShowBox(() =>
        {
            DiaManager.DefaultColor = Color.white;
            DiaManager.ShowContent("You did it!", () =>
            {
                characterCountroler.ChangeSprite(Characters[2]);
                DiaManager.ShowContent("This is the first Spell in the plan, \nand I really like this Spell.", () =>
                {
                    characterCountroler.ChangeSprite(Characters[0]);
                    DiaManager.ShowContent("Moving through a bunch of large masses balls\n as if deep in a swamp. very vivid.", () =>
                    {
                        DiaManager.ShowContent("There are many interesting Spells in the initial plan, \nbut I don't have time.", () =>
                        {
                            characterCountroler.ChangeSprite(Characters[2]);
                            DiaManager.ShowContent("It was great to play with you.\nNow there is one last Spell, hope you like it.", () =>
                            {
                                characterCountroler.ChangeSprite(Characters[0]);
                                DiaManager.ShowContent("It's a Spell you may familiar with. \nit's called. . . . . . . .", () =>
                                {
                                    HideCharacter();
                                    ShowSans();
                                    DiaManager.DefaultColor = DeterColor;
                                    DiaManager.ShowContent("D: E: T: E: R: M: I: N: A: T: I: O: N:                       ", true, () =>
                                    {
                                        Crazy3();
                                        HideBox();
                                        hitManager.BallInitPosition = new Vector2(1, 0.17f);
                                    });
                                });
                            });
                        });
                    });
                });
            });
        });
    }
    public Color DeterColor;

    private int Crazy3DeadCount;


    public void RecoverFromCrazy3()
    {
        Crazy3DeadCount++;
        if(Crazy3DeadCount == 2)
        {
            showHint1();
        }
        //KillManager.CRK(Vector2.zero, 20, new Color(0, 0, 0, 0));
        SoundManager.Stop("sansC4");
        SansC.SansDeactive();
        CleanTimeLine();
        SansSkill.CleanBone1();
        HPManager.recoverSpeed += 1;
        hitManager.DeActive();
        hitManager.MoveScale = 2.5f;
        ShowBox(() =>
        {
            HPManager.hp += 20;
            DiaManager.sound = "sans1";
            DiaManager.DefaultColor = DeterColor;
            DiaManager.ShowContent("S:t:a:y:  D:e:t:e:r:m:i:n:e:d:                                            ", true,() =>
            {
                Crazy3();
                HideBox();
                hitManager.active = true;
                
            });
        });
    }



    public void Crazy3()
    {
        
        hitManager.BallDeadCallBack = () => { KillManager.CRK(Vector2.zero, 20, new Color(0, 0, 0, 0)); RecoverFromCrazy3(); };
        SansC.SansActive();
        SoundManager.Play("sansC0");
        SansSkill.StartSkill(() =>
        {
            CameraShakeOnce();

        });
        TimeLine tem = CreateTimeLine();
        //tem.AddCallBack(0, Crazy3_1);
        tem.AddCallBack(1f, ()=>{ SoundManager.Play("sansP0"); });
        tem.AddCallBack(1.9f, SansSkill.Bones1_1);
        tem.AddCallBack(2.4f, () => { SoundManager.Play("sansP0"); });
        tem.AddCallBack(3f, () => { SoundManager.Play("sansC4"); });
        tem.AddCallBack(2.1f, SansSkill.Bones2_1);
        tem.AddCallBack(4f, SansSkill.Bones1_2);
        tem.AddCallBack(8, Crazy3_1);
        tem.AddCallBack(15, InitCrazy4);




    }

    public void Crazy3_1()
    {
        TimeLine tem = CreateTimeLine();
        tem.AddCallBack(0, () => { SoundManager.Play("sansP0"); });
        tem.AddCallBack(1.5f, () => { SoundManager.Play("sansP0"); });
        tem.AddCallBack(3f, () => { SoundManager.Play("sansP0"); });
        tem.AddCallBack(4.5f, () => { SoundManager.Play("sansP0"); });
        tem.AddCallBack(0, Crazy3_1_1);
        tem.AddCallBack(1.5f, Crazy3_1_2);
        tem.AddCallBack(3f, Crazy3_1_1);
        tem.AddCallBack(4.8f, Crazy3_1_3);
    }

    public void Crazy3_1_1()
    {
        PowExitBack(new Vector2(-5, 4), new Vector2(-2.2f, 2.1f), 0, 0,1);
        PowExitBack(new Vector2(-5, 4), new Vector2(-3.3f, 1f), 0, 90, 1);
        PowExitBack(new Vector2(5, -3.64f), new Vector2(2.2f, -1.74f), 0, 0, 1);
        PowExitBack(new Vector2(5, -3.64f), new Vector2(3.3f, -0.64f), 180, 270, 1);
    }
    public void Crazy3_1_2()
    {
        PowInAndExitBack(new Vector2(3, 1.6f), -65, 2);
        PowInAndExitBack(new Vector2(-3, 1.6f), 65, 2);
        PowInAndExitBack(new Vector2(3, -1.3f), -115, 2);
        PowInAndExitBack(new Vector2(-3, -1.3f), 115, 2);
    }
    public void Crazy3_1_3()
    {
        PowInAndExitBack(new Vector2(-3.1f, 0.18f), 90, 3);
        PowInAndExitBack(new Vector2(3.1f, 0.18f), -90, 3);
    }



    public void InitCrazy4()
    {
        hitManager.BallDeadCallBack = null;
        characterCountroler.ChangeSprite(Characters[6]);
        ShowCharacter();
        ShowBox(() =>
        {
            DiaManager.sound = "char";
            DiaManager.DefaultColor = Color.white;
            DiaManager.ShowContent("This is it!  classic!", () =>
            {
                DiaManager.ShowContent("hey sans, can you do something cooler?", () =>
                {
                    DiaManager.sound = "sans1";
                    DiaManager.DefaultColor = DeterColor;
                    DiaManager.ShowContent("I: w:i:l:l: t:r:y", () =>
                    {
                        DiaManager.sound = "char";
                        DiaManager.DefaultColor = Color.white;
                        characterCountroler.ChangeSprite(Characters[6]);
                        DiaManager.ShowContent("COMBO!!               ", false, () =>
                        {
                            HideBox();
                            HideCharacter();
                            HideHint1();
                            
                            Crazy4();
                        });
                    });
                });
            });
        });
    }

    public void RecoverFromCrazy4()
    {
        hitManager.MoveScale = 2.5f;
        SansC.SansDeactive();
        CleanTimeLine();
        HPManager.recoverSpeed += 1;
        ShowCharacter();
        characterCountroler.ChangeSprite(Characters[2]);
        ShowBox(() =>
        {
            HPManager.hp += 20;
            DiaManager.sound = "sans1";
            DiaManager.DefaultColor = DeterColor;
            DiaManager.ShowContent("T=h=i=s= i=s= t=h=e= l=a=s=t= s=p=e=l=l=.= D=o=n='=t= g=i=v=e= u=p=.         ", true, () =>
            {
                Crazy4();
                HideBox();
                HideCharacter();
                hitManager.active = true;

            });
        });
    }
    public void Crazy4()
    {
        SansC.SansActive();
        hitManager.BallDeadCallBack = () => { KillManager.CRK(Vector2.zero, 20, new Color(0, 0, 0, 0)); RecoverFromCrazy4(); };
        TimeLine tem = CreateTimeLine();
        tem.AddCallBack(0, Crazy4_1);
        tem.AddCallBack(6f, Crazy4_2);
        tem.AddCallBack(16f, Crazy4_3);
        tem.AddCallBack(25, EndGame);
    }

    public void Crazy4_1()
    {
        TimeLine tem = CreateTimeLine();
        tem.AddCallBack(0, Crazy4_1_1);
    }

    public void Crazy4_1_1()
    {
        TimeLine tem = CreateTimeLine();
        for(int i = 0; i < 50; i++)
        {
            float t = i * 0.1f;
            float r = i * 7;
            bool da = i < 30;
            tem.AddCallBack(t, () => { Crazy4Round(r,da); });
        }
    }


    public void Crazy4Round(float r,bool da = true)
    {
        Vector2 tem = UF.RotateVector2(Vector2.up * 2.7f, r * Mathf.PI / 180);

        Vector2 tem2 = UF.RotateVector2(Vector2.up * -2.5f, (r+90) * Mathf.PI / 180);
        Vector2 tem3 = UF.RotateVector2(Vector2.up * -2.5f, (r - 90) * Mathf.PI / 180);
        PowInAndExitBack(tem, r, 4,0.1f);
        if (da)
        {
            CreateDArea(tem2, 1);
            CreateDArea(tem3, 1);
        }
    }

    public void Crazy4_2()
    {
        TimeLine tem = CreateTimeLine();
        tem.AddCallBack(0, () => { Crazy4_2_1(new Vector2(-2.2f, 2.1f), 0, Vector2.right * 0.2f); });
        tem.AddCallBack(1.4f, () => { Crazy4_2_1(new Vector2(2.2f, -2.1f), 180, Vector2.left * 0.2f); });
        tem.AddCallBack(2.8f, () => { Crazy4_2_1(new Vector2(-2.2f, 2.1f), 0, Vector2.right * 0.2f); });

        tem.AddCallBack(5.6f, () => { Crazy4_2_1(new Vector2(-2.2f, 2.1f), 0, Vector2.right * 0.2f); });
        tem.AddCallBack(7.0f, () => { Crazy4_2_1(new Vector2(2.2f, -2.1f), 180, Vector2.left * 0.2f); });


        tem.AddCallBack(1.5f, () => { Crazy4_2_2(new Vector2(2.2f, 0.156f), 90, Vector2.left * 0.25f); });
        //tem.AddCallBack(2.6f, () => { Crazy4_2_2(new Vector2(2.2f, 0f), 90, Vector2.left * 5f / 14); });
        tem.AddCallBack(5.7f, () => { Crazy4_2_2(new Vector2(-2.2f, 0.156f), 0, Vector2.right * 0.25f); });


    }

    public void Crazy4_2_1(Vector2 p,float f,Vector2 dis)
    {
        TimeLine tem = CreateTimeLine();
        
        for(int i = 0; i < 8; i++)
        {
            float tt = i * 0.1f;
            Vector2 tp = p + dis * i;
            tem.AddCallBack(tt, () => { PowInAndExitBack(tp, f,1); });
        }
    }

    public void Crazy4_2_2(Vector2 p, float f, Vector2 dis)
    {
        TimeLine tem = CreateTimeLine();

        for (int i = 0; i < 10; i++)
        {
            float tt = i * 0.1f;
            Vector2 tp = p + dis * i;
            tem.AddCallBack(tt, () => 
            {
                CreateDArea(tp, 1);
                CreateDArea(tp+Vector2.up*0.6f, 0);
                CreateDArea(tp + Vector2.up * 1.2f, 0);
                CreateDArea(tp + Vector2.up * -0.6f, 0);
                CreateDArea(tp + Vector2.up * -1.2f, 0);
            });
        }
    }

    public void Crazy4_3()
    {
        TimeLine tem = CreateTimeLine();
        tem.AddCallBack(0, () =>
        {
            CreateDArea(new Vector2(-0.6f, 0.156f), 0);
            CreateDArea(new Vector2(0.6f, 0.156f), 0);
        });
        tem.AddCallBack(1.5f, () =>
        {
            CreateDArea(new Vector2(-2.5f, 0.156f), 1);
            CreateDArea(new Vector2(2.5f, 0.156f), 1);
            CreateDArea(new Vector2(0f, -1.1f), 1);
        });
        tem.AddCallBack(2, () =>
        {
            CreateDArea(new Vector2(0f, 1.1f), 2);
            CreateDArea(new Vector2(0f, -0.8f), 2);
        });
        tem.AddCallBack(3.8f, () =>
        {
            CreateDArea(new Vector2(2.4f, 1.5f), 2);
            CreateDArea(new Vector2(2.4f,-1.5f), 2);
            CreateDArea(new Vector2(-2.4f, -1.5f), 2);
            CreateDArea(new Vector2(-2.4f, 1.5f), 2);
        });
        tem.AddCallBack(2.1f, () => { SoundManager.Play("sansP0"); });
        tem.AddCallBack(3.6f, () => { SoundManager.Play("sansP0"); });
        tem.AddCallBack(5.1f, () => { SoundManager.Play("sansP0"); });
        tem.AddCallBack(6.6f, () => { SoundManager.Play("sansP0"); });
        tem.AddCallBack(2.1f, Crazy3_1_1);
        tem.AddCallBack(3.6f, Crazy3_1_2);
        tem.AddCallBack(5.1f, Crazy3_1_1);
        tem.AddCallBack(6.3f, () => { Crazy2_2_1(24); });
        tem.AddCallBack(6.9f, Crazy3_1_3);
    }



    public void CreatePow(Vector2 v1,Vector2 v2, Vector3 v3,float f1,float f2,float f3,int t)
    {
        SansPow ts = Instantiate(SansPowProfeb[t]).transform.GetComponent<SansPow>();
        ts.StartPosition = v1;
        ts.PowPosition = v2;
        ts.EndPosition = v3;

        ts.StartZ = f1;
        ts.PowZ = f2;
        ts.EndZ = f3;
        TimeLine tem = CreateTimeLine();
        tem.AddCallBack(0.9f, CameraShakeOnce);
      
    }

    public void EndGame()
    {
        ProBar.NextStage();
        hitManager.BallDeadCallBack = null;
        characterCountroler.ChangeSprite(Characters[0]);
        ShowCharacter();
        ShowBox(() =>
        {
            DiaManager.sound = "char";
            DiaManager.DefaultColor = Color.white;
            DiaManager.ShowContent("It's so cool.\nThank you sans.",() =>
            {
                DiaManager.sound = "sans1";
                DiaManager.DefaultColor = DeterColor;
                DiaManager.ShowContent("I='m= t=i=r=e=d=.", () =>
                {
                    HideSans();
                    DiaManager.sound = "char";
                    DiaManager.DefaultColor = Color.white;
                    characterCountroler.ChangeSprite(Characters[6]);
                    DiaManager.ShowContent("Bye sans.", () =>
                    {
                        characterCountroler.ChangeSprite(Characters[2]);
                        DiaManager.ShowContent("Well, it's the end of the game.", () =>
                        {
                            DiaManager.ShowContent("Thank you so much for playing to the end.", () =>
                            {
                                DiaManager.ShowContent("There's still a lot of stuff left to do, \nbut I have reach my limit.", () =>
                                {
                                    DiaManager.ShowContent("To be honest, I haven't slept in two days.", () =>
                                    {
                                        characterCountroler.ChangeSprite(Characters[6]);
                                        DiaManager.ShowContent("I'm going to bed.\nsee you next time ludum dare, bye", () =>
                                        {
                                            HideCharacter();
                                            HideBox();
                                            BeyBeyBall(16);
                                        });
                                    });
                                });
                            });
                        });
                    });
                });
            });
        });
    }


    public void BeyBeyBall(int BallType)
    {
        TimeLine tem = CreateTimeLine();
        for (int i = 0; i < BeyBey.Length; i++)
        {
            int ti = i;
            tem.AddCallBack(0.05f * ti, () => {
                CreateBallAt(BallType, BeyBey[ti].localPosition);
            });
        }
    }


    public void PowExitBack(Vector2 v1, Vector2 v2, float f1, float f2, int t)
    {
        Vector2 EndPosition = UF.RotateVector2(Vector2.up, f2 * Mathf.PI / 180)*3 + v2;

        CreatePow(v1, v2, EndPosition, f1, f2, f2, t);
    }

    public void PowInAndExitBack( Vector2 v2,  float f2, int t,float scale = 1)
    {
        Vector2 StartPosition = UF.RotateVector2(Vector2.up, f2 * Mathf.PI / 180) * -3*scale + v2;

        PowExitBack(StartPosition, v2, f2, f2, t);



    }


    public int[] IntList(int i,int n)
    {
        int[] res = new int[n];
        for(int j = 0; j < n; j++)
        {
            res[j] = i;
        }
        return res;
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
        CameraShake.Init();
        DiaBox.ClickFunction = (Vector2 v) => { DiaManager.Clicked(); return true; };
        TimeLineContainer = Instantiate(EmptyPrefab).transform;
        TestPosition.position = new Vector2(100, 100);
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
        RecoverFromCrazy4();
        //ReCoverFromC1_1();
        //startCreateBall4();
        BallCounter.GlobleManager.PushBallCount = 10;
        BallCounter.GlobleManager.MoveBallCount = 10;
        //startCreateBall4();
        poleCount = 5;
        //Mainball.InvTime = 1000;
        //InitCrazy2();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFirstScens();
        CameraShake.Update(Time.deltaTime);
        Vector2 t = CameraShake.GetValue();
        Vector3 tt = new Vector3(t.x, t.y, -10);
        MainCamera.position = tt;
    }
    public void CameraShakeOnce()
    {
        CameraShake.RandomWiggle();
    }

   
}
[System.Serializable]
public class CreateBallItem
{
    public float time;
    public int type;
}
