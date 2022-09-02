using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRule
{
    public static void useCard(PlayerCardManager pcm,int n,string playerName)
    {
        switch (n)
        {
            case (0):break;
            case (13):cardDraw(pcm, 1);break;
            case (14):cardDraw(pcm, 3);break;
            default:pcm.ops.showOpration(n, playerName,pcm.cardM.transform.position);break;

        }
    }

    public static void cardDraw(PlayerCardManager pcm,int n)
    {
        for(int i = 0; i < n; i++)
        {
            int tp = pcm.getEmptyCardPosition();
            if (tp != -1)
            {
                pcm.drawCardFromDeck(tp, i * pcm.cardMoveInter);
            }
        }
    }
    //Lycoris
    public static void attack()
    {

    }
}
