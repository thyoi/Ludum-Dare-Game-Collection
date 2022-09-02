using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMarigeManager : MonoBehaviour
{
    public IOManager iom;
    [HideInInspector]
    public string cardRawInfo;
    public cardInfo[] cardInfos;

    private void Start()
    {
        cardRawInfo = iom.readStringFromData(1);
        string[] tem = cardRawInfo.Split('\n');
        tem = UF.noVoidEnd(tem);
        cardInfos = new cardInfo[tem.Length];
        for(int i= 0; i < tem.Length; i++)
        {
                cardInfos[i] = genCardInfo(tem[i]);
        }

    }

    public cardInfo genCardInfo(string s)
    {
        InstructionReturn ir = InstructionReader.readInstruction("/" + s);
        cardInfo res = new cardInfo();
        if (ir.hasParameter("n"))
        {
            res.name = ir.getParameter("n");
        }
        else
        {
            res.name = "";
        }

        if (ir.hasParameter("d"))
        {
            res.describe = ir.getParameter("d");
        }


        if (ir.hasParameter("m"))
        {
            string recipe = UF.stringInChar(ir.getParameter("m"),'{','}');
            string[] tem = recipe.Split(';');
            //UF.print(tem);
            foreach(string rec in tem)
            {
                res.addRecipe(UF.stringInChar(rec, '[', ']'), rec[0] == 'o');
            }
        }

        return res;

    }

    public int getMerageCardId(int[] cards, int cardsNum)
    {
        for(int i = 0;i<cardInfos.Length;i++)
        {
            if (cardInfos[i].merageable(cards, cardsNum))
            {
                return i;
            }
        }
        return 0;
    }

    public string getCardName(int n)
    {
        return cardInfos[n].name;
    }

    public string getCardDescribe(int n)
    {
        return cardInfos[n].describe;
    }
}

[System.Serializable]
public class cardInfo
{
    public string name;
    public string[] orderRecipe = new string[10];
    public int ORN = 0;
    public string[] deorderRecipe = new string[10];
    public int DRN = 0;
    public string describe = "";

    public void addRecipe(string s,bool order = false)
    {
        if (order)
        {
            orderRecipe[ORN] = s;
            ORN++;
        }
        else
        {
            deorderRecipe[DRN] = s;
            DRN++;
        }
    }
    public void check()
    {
        UF.print(name);
        UF.print("order:");
        UF.print(orderRecipe);
        UF.print("deorder:");
        UF.print(deorderRecipe);
    }

    public bool merageable(int[] cards,int cardNum)
    {
        bool flag = false;
        for(int i = 0; i < ORN; i++)
        {
            if (equal(cards, cardNum, orderRecipe[i], true))
            {
                flag = true;
            }
        }
        for (int i = 0; i < DRN; i++)
        {
            if (equal(cards, cardNum, deorderRecipe[i], false))
            {
                flag = true;
            }
        }
        return flag;
    }

    public bool equal(int[] cards,int cardNum,string recipe,bool order = false)
    {
        if (order)
        {
            string[] tem = recipe.Split(',');
            if(tem.Length != cardNum)
            {
                return false;
            }
            for(int i = 0; i < cardNum; i++)
            {
                if(UF.stringToInt(tem[i]) != cards[i])
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            string[] tem = recipe.Split(',');
            if (tem.Length != cardNum)
            {
                return false;
            }
            int[] ttt = new int[cardNum];
            for(int i = 0;i< cardNum; i++)
            {
                ttt[i] = UF.stringToInt(tem[i]);
            }
            for(int i = 0; i < cardNum; i++)
            {
                if (!intInList(ttt, cards[i]))
                {
                    return false;
                }
            }
            return true;


        }
    }

    public bool intInList(int[] nl,int n)
    {
        for (int i = 0; i < nl.Length; i++){
            if(nl[i] == n)
            {
                nl[i] = -1;
                return true;
            }
        }
        return false;
    }
}



