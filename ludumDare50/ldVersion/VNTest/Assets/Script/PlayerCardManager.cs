using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardManager : MonoBehaviour
{
    public string PlayerName;
    public Clickable[] card5;
    public Clickable cardM;
    public GameObject cardDisc;
    public cardShow[] cardAreas;
    public float cardMoveSpeed = 15;
    public float cardMoveInter = 0.2f;
    public GameObject temCard;
    public SpriteManager sm;
    public int[] cardDect;
    [HideInInspector]
    public int[] cardsDraw = new int[100];
    public int cardDrawCount = 0;
    public int drawable = 3;
    // Start is called before the first frame update
    public bool drawdrawdraw = false;
    public bool[] hasCard = new bool[5];
    public MargeAnime finishA;
    public bool waitclick = true;
    public main Manager;
    public int[] cardClickMask;
    public string clickMassage;
    public oprationShow ops;
    public int mustCard = -1;

    public int hoverDescribe()
    {
        if (waitclick)
        {
            int witch = -1;
            for(int i = 0; i < 5; i++)
            {
                if (card5[i].transform.GetComponent<Hoverable>().Hover)
                {
                    witch = i;
                }
            }
            if (cardM.transform.GetComponent<Hoverable>().Hover && cardM.transform.GetComponent<Hoverable>().Active)
            {
                witch = 5;
            }
            if (witch != -1)
            {
                return cardAreas[witch].getFaceCard();
            }
            else
            {
                return -1;
            }
        }
        else
        {
            return -1;
        }
    }


    public void openAllMask()
    {
        for (int i = 0; i < 6; i++)
        {
            cardClickMask[i] = 1;
        }
    }

    public void closeAllMask()
    {
        for (int i = 0; i < 6; i++)
        {
            cardClickMask[i] = -1;
        }
    }


    public void turnEnd()
    {
        waitclick = false;
        for(int i = 0; i < 5; i++)
        {
            card5[i].transform.GetComponent<cardBackHoverSetter>().Act = false;
        }
        cardM.transform.GetComponent<cardBackHoverSetter>().Act = false;
        finishA.maxSize = 1;
        int cc = 0;
        while (cardAreas[5].topCard()!= -1)
        {
            int tp = getEmptyCardPosition();
            if (tp == -1)
            {
                tp = 0;
            }
            moveATooB(5, tp,cc*0.09f);
            cc++;
        }


    }

    public void turnStart()
    {
        waitclick = true;
        for (int i = 0; i < 5; i++)
        {
            card5[i].transform.GetComponent<cardBackHoverSetter>().Act = true;
        }
        cardM.transform.GetComponent<cardBackHoverSetter>().Act = true;
        finishA.maxSize = 0;
        drawCard();
    }

    public void drawCard()
    {
        //UF.print("" + cardAreas.Length);
        for(int i = 0; i < drawable; i++)
        {
            if (!hasCard[i])
            {
                drawCardFromDeck(i,i*cardMoveInter);
            }
        }
        //drawCardFromDeck(0);
    }
    public Vector2 cdPosition;
    public void drawCardFromDeck(int to, float delay = 0)
    {
        //UF.print(cardDisc.transform.position);
        int tem = dequene();
        if (tem != -1)
        {
            createCardMove(cardDisc.transform.position, getMovePosition(to), tem, to, delay);
        }
        if(cardDrawCount == 0)
        {
            UF.setLocalPosition(cardDisc, new Vector2(100, 100));
        }
        else
        {
            UF.setLocalPosition(cardDisc, cdPosition);
        }
    }



    public void moveATooB(int a, int b, float delay = 0)
    {
        //UF.print(a+"||"+b);
        createCardMove(getMovePosition(a), getMovePosition(b), cardAreas[a].popCard(), b,delay);
        if (a < 5)
        {
            hasCard[a] =  cardAreas[a].onShow;
        }
    }

    public void enquene(int n)
    {
        cardsDraw[cardDrawCount] = n;
        cardDrawCount++;
    }

    public int dequene()
    {
        if (cardDrawCount > 0)
        {
            int tem = cardsDraw[0];
            for (int i = 1; i < cardDrawCount; i++)
            {
                cardsDraw[i - 1] = cardsDraw[i];
            }
            cardDrawCount--;
            return tem;
        }
        else
        {
            return -1;
        }
    }

    public Vector2 getMovePosition(int n)
    {
        if (n < 5)
        {
            return (Vector2)card5[n].transform.position;
        }
        if(n == 5)
        {
            return (Vector2)cardM.transform.position;
        }
        else
        {
            return new Vector2(6, 6);
        }
    }

    public void CheckleftClickOfCardA()
    {

        for(int i = 0; i < 5; i++)
        {
            if (card5[i].lClick )
            {
                //UF.print(i);
                if (cardClickMask[i]>0)
                {
                    
                    sendClickMassage();
                    //card5[i].lClick = false;
                    moveATooB(i, 5);
                }
            }
        }
    }
    public void sendClickMassage(bool totuF = false)
    {
        if (mustCard == -1 || totuF)
        {
            if (clickMassage != null)
            {
                Manager.cardMassage(clickMassage);
                clickMassage = null;
            }
        }
    }
    public int getEmptyCardPosition()
    {
        for(int i = 0; i < drawable; i++)
        {
            if (!hasCard[i])
            {
                return i;
            }
        }
        return -1;
    }
    public void CheckRightClickOfCardM()
    {
        if (cardM.rClick && cardClickMask[0]>0)
        {
            //cardM.rClick = false;
            int tp = getEmptyCardPosition();
            if(tp == -1)
            {
                tp = 0;
            }
            moveATooB(5, tp);
        }
    }

    public void CheckLeftClickOfCardM()
    {
        if (cardM.lClick && cardClickMask[5]>0)
        {
            if (mustCard == -1 || cardAreas[5].getFaceCard() == mustCard)
            {
                sendClickMassage(true);
                //cardM.lClick = false;
                useCard(cardAreas[5].getFaceCard());
            }
        }
    }

    public void useCard(int n)
    {
        GameObject tc= Instantiate(temCard);
        UF.setPosition(tc, cardM.transform.position);
        tc.transform.GetComponent<temCardAnime>().startAnime = true;
        tc.transform.GetComponent<SpriteRenderer>().sprite = sm.getCard(cardAreas[5].getFaceCard());
        int temCN = cardAreas[5].popCard();
        while(temCN != -1)
        {
            enquene(temCN);
            temCN = cardAreas[5].popCard();
        }

        if(n!=13 && n != 14)
        {
            turnEnd();
        }
        CardRule.useCard(this, n,PlayerName);
    }




    
    void Start()
    {
        cardClickMask = new int[6];
        openAllMask();
        cdPosition = cardDisc.transform.localPosition;
        //setDrawable();
        for(int i = 0; i < cardDect.Length; i++)
        {
            enquene(cardDect[i]);
        }
        //cardDrawCount = cardDect.Length;
        //drawCard();
    }

    // Update is called once per frame
    void Update()
    {
        updateCardMove();
        checkCardMove();
        if (drawdrawdraw)
        {
            drawCard();
            drawdrawdraw = false;
        }
        if (waitclick)
        {
            CheckleftClickOfCardA();
            CheckRightClickOfCardM();
            CheckLeftClickOfCardM();
        }
    }






    public cardMove[] cardMoveList = new cardMove[50];
    public int cardMoveCount = 0;
    public void updateCardMove()
    {
        for(int i = 0; i < cardMoveCount; i++)
        {
            cardMoveList[i].update(Time.deltaTime);
        }
    }
    public void checkCardMove()
    {
        if (cardMoveCount > 0)
        {
            int st = 0;
            int en = 0;
            int ttem = cardMoveCount;
            for(int i = 0; i < ttem; i++)
            {
                if (cardMoveList[en].dead)
                {
                    cardMoveEnd(cardMoveList[en]);
                    en++;
                    cardMoveCount--;
                }
                else
                {
                    if (st != en)
                    {
                        cardMoveList[st] = cardMoveList[en];
                    }
                    st++;
                    en++;
                }
            }
        }
    }
    public void cardMoveEnd(cardMove cm)
    {
        if (cm.moveTo < 6)
        {
            cardAreas[cm.moveTo].pushCard(cm.cardID);
            cm.moveTo = 100;
        }
        cm.destory();
    }

    public cardMove createCardMove(Vector2 startPosition, Vector2 endPosition, int cardID, int moveTo, float delay = 0)
    {
        if (moveTo < 5)
        {
            hasCard[moveTo] = true;
        }
        cardMove res = new cardMove();
        res.delay = delay;
        res.moveSpeed = cardMoveSpeed;
        res.startPosition = startPosition;
        res.endPosition = endPosition;
        res.curPosition = startPosition;
        res.moveCard = Instantiate(temCard);
        res.moveCard.transform.GetComponent<SpriteRenderer>().sprite = sm.getCard(cardID);
        res.moveTo = moveTo;
        res.cardID = cardID;
        res.dead = false;
        res.update(0);

        cardMoveList[cardMoveCount] = res;
        cardMoveCount++;
        return res;

    }
}

[System.Serializable]
public class cardMove
{
    public float delay;
    public float moveSpeed;
    public Vector2 startPosition;
    public Vector2 endPosition;
    public Vector2 curPosition;
    public GameObject moveCard;
    public int moveFrom; // 0~4 c5, 5 cm,6 cd
    public int moveTo;
    public int cardID;
    public bool dead;
    public void update(float dt)
    {
        if (!dead)
        {
            if (delay < 0)
            {
                curPosition = Vector2.Lerp(curPosition, endPosition, dt * moveSpeed);
                if(UF.isCloseEnough(curPosition,endPosition))
                {
                    curPosition = endPosition;
                    dead = true;
                }
            }
            else
            {
                delay -= dt;
            }
            UF.setPosition(moveCard,curPosition);
        }
    }

    public void destory()
    {
        if (moveCard != null)
        {
            UF.setPosition(moveCard, new Vector2(100, 100));
            GameObject.Destroy(moveCard);
            moveCard = null;
        }
    }
}

