using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    [HideInInspector]
    public map[] levelMap;
    public map curentMap = null;
    public token[] players;

    public MapManager mm;
    public dialogueManager dm;
    public float WaitBeforGameStart;
    public bool isStart;
    public PaperMove paper;
    public PaperMove player1;
    public PaperMove endT;
    public EndTurn endTrun;
    public bool canEndTurn = true;
    public showItem describeThing;
    public PlayerCardManager cardDeck1;
    public PlayerCardManager cardDeck2;
    public PlayerCardManager cardDeck3;
    public token playerToken1;
    public token playerToken2;
    public CardMarigeManager cmm;
    public SpriteManager sm;
    public DescribeManager describer;
    public oprationShow ops;

    public showItem[] messageList;
    public int messageCount;
    public int messageCur;
    public float messageTime;
    public float messageTimeCount;
    public int monsterState = -1;
    public float monsterMoveTime = 1;
    public int monsterCount;
    public float monsterTimeCount;

    public void startMonsterRound()
    {
        monsterCount = 0;
        monsterTimeCount = 0.4f;
        monsterState = 1;

    }

    public void endMonsterRound()
    {
        monsterState = -1;
        startRound();
    }
    public void setMap(map m)
    {

        curentMap = m;
        curentMap.resetPosition();
        mm.initMap(m);

    }

    

    public void closeAllCardDeck()
    {
        cardDeck1.closeAllMask();
    }

    public void openAllCardDeck()
    {
        cardDeck1.openAllMask();
    }

    public showItem getHoverCardDescribe()
    {
        int t = cardDeck1.hoverDescribe();
        if (t != -1)
        {
            return genCardDescribe(t);
        }
        t = cardDeck2.hoverDescribe();
        if (t != -1)
        {
            return genCardDescribe(t);
        }
        t = cardDeck3.hoverDescribe();
        if (t != -1)
        {
            return genCardDescribe(t);
        }

        return null;

    }

    public showItem genCardDescribe(int n)
    {
        showItem res = new showItem();
        res.text = cmm.getCardName(n) + ":]" + cmm.getCardDescribe(n);
        res.sp = sm.getCard(n);
        return res;
    }

    public void endRound()
    {
        if (canEndTurn)
        {
            canEndTurn = false;
            cardDeck1.turnEnd();
            endTrun.active = false;
            paper.endPosition = new Vector2(-4.5f, 0);
            paper.move = true;
            endT.endPosition = new Vector2(10, endT.endPosition.y);
            endT.move = true;
            startMonsterRound();
        }
    }

    public void startRound()
    {
        canEndTurn = true;
        cardDeck1.turnStart();
        endTrun.active = true;
        paper.endPosition = new Vector2(-12, 0);
        paper.move = true;
        endT.endPosition = new Vector2(5, endT.endPosition.y);
        endT.move = true;
    }

    void Start()
    {
        describeThing = new showItem();
        describeThing.text = "";
        describeThing.sp = null;
        messageList = new showItem[100];
        messageCount = 1;
        messageCur = 0;
        messageList[0] = new showItem();
        messageList[0].text = "";
        messageList[0].sp = null;
        levelMap = MapIniter.mapInit();
        setMap(levelMap[0]);
        playerToken1 = curentMap.findTokenByType(0);
        playerToken2 = curentMap.findTokenByType(1);
        //mm.showBlock();
        //mm.showPlayer();
        //mm.showMonster(); 
    }
    public float dt;
    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        if (!isStart)
        {
            if (WaitBeforGameStart <= 0)
            {
                startGame();
            }
            else
            {
                WaitBeforGameStart -= dt;
            }
        }
        if (endTrun.click)
        {
            endRound();
        }
        updateDescribe();
        updateMessage();
        if (monsterState > 0)
        {
            if (monsterTimeCount > 0)
            {
                monsterTimeCount -= dt;
            }
            else
            {
                if (monsterCount >= curentMap.tokenCount)
                {
                    endMonsterRound();
                }
                else
                {
                    string ttt = monsters.action(curentMap, monsterCount);
                    monsterCount++;
                    if(ttt != "")
                    {

                        addMessage(genOneLine(ttt), 1);
                        monsterTimeCount = monsterMoveTime;
                    }
                }
            }
        }


    }

    public showItem[] genOneLine(string s)
    {
        showItem[] res = new showItem[1];
        res[0] = new showItem();
        res[0].text = s;
        return res;
    }
    public void updateMessage()
    {
        if(messageCur >= messageCount - 1)
        {

        }
        else
        {
            messageTimeCount += dt;
            if (messageTimeCount > messageTime)
            {
                messageTimeCount = 0;
                messageCur++;
            }
        }
        describeThing = messageList[messageCur];
    }

    public void addMessage(showItem[] sl,int len)
    {
        if (len > 0)
        {
            messageCur = messageCount;
            messageTimeCount = 0;
            for(int i = 0; i < len; i++)
            {
                messageList[messageCount] = sl[i];
                messageCount++;
            }
        }
    }
    public void updateDescribe()
    {
        showItem tem = getHoverCardDescribe();
        if(tem == null)
        {
            describer.setContent(describeThing);
        }
        else
        {
            describer.setContent(tem);
        }
    }

    public void startGame()
    {
        isStart = true;
        //dm.startDialogues("start");
        //endTrun.active = false;
       player1.move = true;
        cardDeck1.drawCard();
        cardDeck2.drawCard();
        cardDeck3.drawCard();
        //endT.move = true;
        describer.transform.GetComponent<PaperMove>().move = true;
    }
    public void cardMassage(string s)
    {
        UF.print(s);
        switch (s)
        {
            case ("c1"):dm.startDialogues("after_left_click_the_card"); cardDeck1.openAllMask(); break;
            case ("c2"): dm.startDialogues("after_left_click_the_card_again"); cardDeck1.openAllMask();
                ops.tuto = true;
                ops.tutoPosition = new Vector2Int(-1, 1);
                break;
            case ("c3"):cardDeck1.mustCard = -1;
                dm.startDialogues("after_combo_click");
                break;
        }
    }
    public void dialogueMassgae(string s)
    {
        //UF.print(s);
        switch (s)
        {

            case ("11"):showPaper(); break;
            case ("12"):mm.showBlock();break;
            case ("13"):mm.showPlayer();break;
            case ("17"):mm.showMonster();break;
            case ("20"):
                hidePaper();
                player1.move = true;
                describer.transform.GetComponent<PaperMove>().move = true;
                endT.move = true;
                UF.setPositionZ(player1.gameObject, 0);
                break;
            case ("21"):cardDeck1.drawCard();break;
            case ("23"):tuto1();break;
            case ("27"):tuto2();break;
            case ("31"): tuto3();cardDeck1.turnStart(); break;

        }
    }
    public void tuto1()
    {   cardDeck1.cardClickMask[0] = -1;
        cardDeck1.cardClickMask[1] = -1;
        cardDeck1.cardClickMask[5] = -1;
        cardDeck1.clickMassage = "c1";
    }
    public void tuto2()
    {
        cardDeck1.cardClickMask[0] = -1;
        cardDeck1.cardClickMask[1] = -1;
        cardDeck1.cardClickMask[2] = -1;
        cardDeck1.clickMassage = "c2";
    }
    public void tuto3()
    {
        cardDeck1.mustCard = 15;
        cardDeck1.clickMassage = "c3";
    }

    public void showPaper()
    {
        paper.MoveTo(new Vector2(-4.5f, 0));
    }
    public void hidePaper()
    {
        paper.MoveTo(new Vector2(-12, 0));
    }

    public void opration(int cardID, string playerName, Vector2Int[] data)
    {
        showItem[] mes = new showItem[20];
        int mesCount = 0;
        if(ops.tuto && ops.tutoPosition == new Vector2Int(-1, 1))
        {
            ops.tuto = false;
            dm.startDialogues("after_move");
        }

        if(cardID == 3 || cardID == 8)
        {
            for(int i = data.Length-1; i > 0; i--)
            {
                data[i] = data[i] - data[i - 1];
                
            }
            token t = playerToken1;
            if (playerName != "Lycoris")
            {
                t = playerToken2;
            }
            int tem;
            for (int i = 0; i < data.Length; i++)
            {
                tem = curentMap.MoveToken(t.x, t.y, t.x + data[i].x, t.y + data[i].y);
                if(tem == 0)
                {
                    mes[mesCount] = new showItem(playerName + " took a step ]to the " + getDriction(data[i]));
                    mesCount++;
                }
                else
                {
                    if(tem == -3)
                    {
                        mes[mesCount] = new showItem(playerName + " took a step ]to the " + getDriction(data[i]) + "but was ]blocked by the wall");
                        mesCount++;
                        break;
                    }
                    if(tem == -2)
                    {
                        mes[mesCount] = new showItem(playerName + " took a step ]to the " + getDriction(data[i]) + "but was ]blocked by something");
                        mesCount++;
                        break;
                    }
                }

            }
            mm.updateToken();
            addMessage(mes, mesCount);
            
        }
    }


    public string getDriction(Vector2Int d)
    {
        if(d.x>0 && d.y == 0)
        {
            return "right";
        }
        else if(d.x == 0 && d.y > 0)
        {
            return "front";
        }
        else if(d.x <0 && d.y == 0)
        {
            return "left";
        }
        else if(d.x == 0 && d.y < 0)
        {
            return "back";
        }
        else
        {
            return "";
        }
    }
}





