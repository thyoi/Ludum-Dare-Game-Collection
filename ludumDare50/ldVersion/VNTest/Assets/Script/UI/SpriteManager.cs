using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public Sprite[] cardList;
    public Sprite[] tokenList;

    public Sprite getCard(int cid)
    {
        return cardList[cid];
    }

    public Sprite getToken(int tid)
    {
        return tokenList[tid];
    }
}
