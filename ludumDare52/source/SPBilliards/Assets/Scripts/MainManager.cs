using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
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

    public AnimeCountroler Character;
    public AnimeCountroler Box;
    public MouseBox DiaBox;

    private int poleCount = 0;

    public void HitPole()
    {
        poleCount++;
        if(poleCount == 1)
        {
            StartConverSation2();
        }
    }


    public TimeLine CreateTimeLine()
    {
        TimeLine t = Instantiate(TimeLinePrefab).transform.GetComponent<TimeLine>();
        t.Init();
        t.finalTime = 200;
        return t;
    }


    public void RandomBall(int i)
    {
        CreateBallAt(i, new Vector2(Random.Range(-2.4f, 2.4f), Random.Range(-1.2f, 1.2f)));
    }

    private void CreateBallAt(int i, Vector2 p)
    {
        Transform t = Instantiate(BallPerfabs[i]).transform;
        t.position = p;
        particalManager.GlobalManager.BoomParticalBust(30, p, t.GetComponent<BallCountroler>().MainColor, 1.5f, true);
        particalManager.GlobalManager.CreateRoundPartical(p, 1f, t.GetComponent<BallCountroler>().MainColor, true);

    }


    public void ShowCharacter(UF.AnimeCallback c = null)
    {
        Character.positionY.Init = -3.7f;
        Character.positionY.End = -0.9f;
        Character.StartAnime(c);
        Character.positionY.DelayCallback = () => { SoundManager.Play("CharacterShow"); };
    }

    public void ShowBox(UF.AnimeCallback c = null)
    {
        Box.positionY.Init = -3;
        Box.positionY.End = -1.84f;
        Box.StartAnime(c);
    }

    public void HideCharacter(UF.AnimeCallback c = null)
    {
        Character.positionY.End= -3.7f;
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
                    FirstScensMask.StartAnime(() => { FirstConversation(); DestoryFirstScens();  });
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

    }

    public void startCreateBall()
    {
        TimeLine tem = CreateTimeLine();
        foreach(CreateBallItem c in ballList)
        {
            tem.AddCallBack(c.time, () => { RandomBall(c.type); });
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        DiaBox.ClickFunction = (Vector2 v) => { DiaManager.Clicked(); return true; };
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
