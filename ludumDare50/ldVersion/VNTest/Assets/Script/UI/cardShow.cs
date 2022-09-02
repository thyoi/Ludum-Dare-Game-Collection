using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardShow : MonoBehaviour
{
    public bool show = true;
    public bool onShow = false;
    [HideInInspector]
    public int[] cardID;
    [HideInInspector]
    public int cardCount = 0;
    public MargeAnime ma;
    public SpriteManager sm;
    public SpriteRenderer sr;
    public CardMarigeManager cmm;
    public bool margeable = false;
    
    private void Start()
    {
        ma = transform.GetComponent<MargeAnime>();
        sr = transform.GetComponent<SpriteRenderer>();
        cardID = new int[10];
        cardCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (show != onShow)
        {
            if (onShow)
            {
                UF.setLocalPosition(this.gameObject, new Vector2(0, 0));
            }
            else
            {
                UF.setLocalPosition(this.gameObject, new Vector2(100, 100));
            }
            show = onShow;
        }
    }

    public int topCard()
    {
        if (!onShow)
        {
            return -1;
        }
        else
        {
            return cardID[cardCount-1];
        }
    }

    public int popCard()
    {
        if (!onShow)
        {
            return -1;
        }
        else
        {
            cardCount--;
            if (cardCount == 0)
            {
                onShow = false;
            }
            else
            {
                ma.curSize = 1.3f;
                if (margeable)
                {
                    sr.sprite = sm.getCard(merageCard());
                }
                else
                {
                    sr.sprite = sm.getCard(cardID[cardCount]);

                }
            }

            return cardID[cardCount];

        }
    }

    public void pushCard(int cid)
    {
        //UF.print("" + cardID.Length + "||" + cardCount);
        if (onShow)
        {
            ma.curSize = 1.3f;
        }
        onShow = true;
        
        cardID[cardCount] = cid;
        cardCount++;
        if (margeable)
        {
            sr.sprite = sm.getCard(merageCard());
        }
        else
        {
            sr.sprite = sm.getCard(cardID[cardCount-1]);
        }
        

    }

    public int getFaceCard()
    {
        if (margeable)
        {
            return merageCard();
        }
        else
        {
            return topCard();
        }
    }

    public int merageCard()
    {
        if(cardCount == 1)
        {
            return cardID[0];
        }
        return cmm.getMerageCardId(cardID,cardCount);
    }
}
