using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public map curMap = null;
    public bool[] state = new bool[5];
    [HideInInspector]
    public MapBlock[,] blocks;
    public MapToken[] tokens;
    public token[] tokenData;
    public int tokenCount = 0;
    public GameObject blockPrefab;
    public GameObject tokenPrefab;
    public float blockShowTime;
    public float blockShowInnerTime;
    public Vector2 blockStartPosition;
    public SpriteManager sm;
    public Transform PaperArea;
    public void initMap(map m)
    {
        if(curMap!= null)
        {
            cleanMap();
        }

        curMap = m;
        for(int i = 0; i < 5; i++)
        {
            state[i] = false;
        }
    }
    public void showBlock()
    {
        if (!state[0])
        {
            state[0] = true;
            blocks = new MapBlock[curMap.w, curMap.h];
            for(int i = 0; i < curMap.h; i++)
            {
                for(int j = 0; j < curMap.w; j++)
                {
                    createBlock(i, j);
                }
            }




        }
    }

    public void createBlock(int x,int y)
    {
        GameObject ttem = Instantiate(blockPrefab,PaperArea);
        MapBlock tem = setShowData(ttem);
        tem.delay = (y * curMap.w + x) * blockShowInnerTime;
        Vector2 tp = blockStartPosition;
        tp.x += x * 0.5f;
        tp.y += y * 0.5f;
        UF.setLocalPosition(ttem, tp);
        blocks[x, y] = tem;

    }
    public MapBlock setShowData(GameObject ttem)
    {
        MapBlock tem = ttem.transform.GetComponent<MapBlock>();
        tem.show = true;
        tem.showTime = blockShowTime;
        return tem;
        
    }

    public void showPlayer()
    {
        //UF.print("asd");
        tokens = new MapToken[100];
        tokenData = new token[100];
        tokenCount = 0;
        if (!state[1])
        {
            state[1] = true;
            
            for (int i = 0; i < curMap.tokenCount; i++)
            {
                
                if (curMap.tokenList[i].player)
                {
                    createToken(curMap.tokenList[i]);
                }
            }
        }
    }
    public void showMonster()
    {
        if (!state[2])
        {
            state[2] = true;
            for (int i = 0; i < curMap.tokenCount; i++)
            {
                if (!curMap.tokenList[i].player)
                {
                    createToken(curMap.tokenList[i]);
                }
            }
        }
    }

    public void createToken(token t)
    {
        //UF.print("asd");
        GameObject tttem = Instantiate(tokenPrefab,PaperArea);
        MapBlock ttem = setShowData(tttem);
        MapToken tem = tttem.transform.GetComponent<MapToken>();
        Vector2 tp = blockStartPosition;
        tp.x += t.x * 0.5f + 0.275f;
        tp.y += t.y * 0.5f + 0.225f;
        tttem.transform.GetComponent<SpriteRenderer>().sprite = sm.getToken(t.Type);
        UF.setLocalPosition(tttem, tp);
        tokens[tokenCount] = tem;
        tokenData[tokenCount] = t;
        tokenCount++;
        


    }

    public void updateToken()
    {
        for(int i = 0; i < tokenCount; i++)
        {
            Vector2 tp = blockStartPosition;
            tp.x += tokenData[i].x * 0.5f + 0.275f;
            tp.y += tokenData[i].y * 0.5f + 0.225f;
            UF.setLocalPosition(tokens[i].gameObject, tp);
        }
    } 
    public void cleanMap()
    {
        
        for(int i = 0; i < curMap.h; i++)
        {
            for(int j = 0; j < curMap.w; j++)
            {
                blocks[j, i].transform.GetComponent<temCardAnime>().startAnime = true;
            }
        }

        for(int i = 0; i < tokenCount; i++)
        {
            tokens[i].transform.GetComponent<temCardAnime>().startAnime = true;
        }
        tokenCount = 0;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class map
{
    public token[,] data;
    public int w;
    public int h;
    public token[] tokenList;
    public int tokenCount = 0;
    public map(int x, int y)
    {
        data = new token[x, y];
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                data[j, i] = null;
            }
        }
        tokenList = new token[x * y];
        tokenCount = 0;
        w = x;
        h = y;
    }

    public void resetPosition()
    {
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                if(data[j,i]!= null)
                {
                    data[j, i].x = j;
                    data[j, i].y = i;
                }
            }
        }
    }
    public void addToken(token t, int x, int y)
    {

        if (findTokenByXY(x, y) == -1)
        {

            data[x, y] = t;
            tokenList[tokenCount] = t;
            tokenCount++;
            t.map = this;
            t.x = x;
            t.y = y;
        }
        else
        {

            deletToken(x, y);
        }
    }

    public int findTokenByXY(int x, int y)
    {
        //UF.print("" + x + "||" + y+"||"+ tokenCount);
        for (int i = 0; i < tokenCount; i++)
        {
            if (tokenList[i].x == x && tokenList[i].y == y)
            {
                return i;
            }
        }
        return -1;
    }
    public void deletToken(int x, int y)
    {
        deletToken(findTokenByXY(x, y));
        data[x, y] = null;
    }

    public void deletToken(int n)
    {
        if (n >= tokenCount || n < 0)
        {
            return;
        }
        for (int i = n + 1; i < tokenCount; i++)
        {
            tokenList[i - 1] = tokenList[i];
        }
        tokenCount--;
    }

    public int MoveToken(int x1, int y1, int x2, int y2)
    {
        if(x2<0 || x2>=w || y2<0 || y2 >= h)
        {
            return -3;
        }
        if (findTokenByXY(x1, y1) == -1)
        {
            return -1;
        }
        if (findTokenByXY(x2, y2) != -1)
        {
            return -2;
        }
        int tem = findTokenByXY(x1, y1);
        data[x1, y1] = null;
        data[x2, y2] = tokenList[tem];
        tokenList[tem].x = x2;
        tokenList[tem].y = y2;
        return 0;



    }


    public int[] getPlayerHp()
    {
        int[] res = new int[10];
        for (int i = 0; i < tokenCount; i++)
        {
            if (tokenList[i].player)
            {
                res[tokenList[i].Type] = tokenList[i].hp;
            }
        }
        return res;
    }

    public void setPlayerHp(int[] hpl)
    {
        for (int i = 0; i < tokenCount; i++)
        {
            if (tokenList[i].player)
            {
                tokenList[i].hp = hpl[tokenList[i].Type];
            }
        }
    }

    public Vector2[] getPlayerPosition()
    {
        Vector2[] res = new Vector2[2];
        token t1 = findTokenByType(0);
        if (t1 != null)
        {
            res[0] = new Vector2(t1.x, t1.y);
        }
        else
        {
            res[0] = new Vector2(0, 0);
        }
        token t2 = findTokenByType(1);
        if (t2 != null)
        {
            res[1] = new Vector2(t2.x, t2.y);
        }
        else
        {
            res[1] = new Vector2(1, 0);
        }
        return res;

    }

    public token findTokenByType(int type)
    {
        for (int i = 0; i < tokenCount; i++)
        {
            if (tokenList[i].Type == type)
            {
                return tokenList[i];
            }
        }
        return null;
    }





}

