using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oprationShow : MonoBehaviour
{
    public Transform opAreaTransform;
    public GameObject blockTem;
    public GameObject tokenTem;
    public opAreaMove anime;
    public MapBlock[,] blockList;
    public int blockCountX;
    public int blockCountY;
    public MapToken[] tokenList;
    public int tokenCount;
    public main Manager;
    public SpriteManager sm;
    public float blockShowTime;
    

    public bool waitClick = false;

    public int lastCardID;
    public string lastPlayerName;
    public Vector2Int[] returnData;
    public int returnDataCount = 0;
    public int moveCount;
    public Vector2Int moveStartPosition;
    public Hoverable hoverItem;
    public Clickable clickItem;
    public bool tuto = false;
    public Vector2Int tutoPosition;


    public void showOpration(int cardID, string playerName,Vector2 startPosition)
    {
        lastCardID = cardID;
        lastPlayerName = playerName;
        if (cardID == 3)
        {
            showBox(startPosition, new Vector2(4, 4));
            showBlock(4, 1,0.3f);
            returnData = new Vector2Int[2];
            returnDataCount = 0;
            moveStartPosition = new Vector2Int(0, 0);
            moveCount = 2;
            if(playerName == "Lycoris")
            {
                creatToken(0, 0, 0, 0);
            }
            else
            {
                creatToken(1, 0, 0, 0);
            }
        }
        else if(cardID == 8)
        {
            showBox(startPosition, new Vector2(4, 4));
            showBlock(4, 1, 0.3f);
            returnData = new Vector2Int[3];
            returnDataCount = 0;
            moveStartPosition = new Vector2Int(0, 0);
            moveCount = 3;
            if (playerName == "Lycoris")
            {
                creatToken(0, 0, 0, 0);
            }
            else
            {
                creatToken(1, 0, 0, 0);
            }
        }

        else if(cardID == 15)
        {
            showBox(startPosition, new Vector2(6, 3));
        }
    }

    public void cleanBackGroundAll()
    {
        for(int i = 0; i < 100; i++)
        {
            for(int j = 0; j < 100; j++)
            {
                if (blockList[j, i] != null)
                {
                    blockList[j, i].setColor(new Color(0,0,0,0));            
                }
            }
            
        }
    }




    public void showBlock(int n, int type, float delay = 0)
    {
        if(type == 1)
        {
            for(int i = 0;i<n;i++)
            {
                showBlock(i, -1, i * 0.05f + delay);
            }
        }
        else if(type == -1)
        {
            if(n == 0)
            {
                createBlock(0, 0, delay);
            }
            else
            {
                for(int i = 0; i < n; i++)
                {
                    createBlock(n-i, i, delay);
                    createBlock(i-n, -i, delay);
                    createBlock(-i, n-i, delay);
                    createBlock(i, i-n, delay);
                }
            }
        }
    }


    public void DrawBlock(Vector2Int st, Vector2Int ed, Color c,int n,int type)
    {
        if (type == 1)
        {
            for (int i = 0; i < n; i++)
            {
                DrawBlock(st,ed,c,i,-1);
            }
        }
        else if (type == -1)
        {
            if (n == 0)
            {
                setColorXY(st, c);
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    setColorXY(n - i + st.x, i+st.y, c);
                    setColorXY(i - n+st.x, -i + st.y, c);
                    setColorXY(-i + st.x, n - i + st.y, c);
                    setColorXY(i + st.x, i - n + st.y, c);
                }
            }
        }

        else if(type == 2)
        {
            Vector2Int[] temPath = getPath(st, ed);
            int len = temPath.Length;
            if (temPath.Length > n)
            {
                len = n;
            }
            for(int i = 0; i < len; i++)
            {
                setColorXY(temPath[i], c);
            }
        }
    }

    public Vector2Int[] getPath(Vector2Int st, Vector2Int ed)
    {
        Vector2Int[] tem = new Vector2Int[100];
        int pointCount = 0;
        int dx = 1;
        int dy = 1;
        int disX = Mathf.Abs(st.x - ed.x);
        int disY = Mathf.Abs(st.y - ed.y);
        if (st.x > ed.x)
        {
            dx = -1;
        }
        if (st.y > ed.y)
        {
            dy = -1;
        }
        int countX = 0;
        int countY = dy;
        for(int i = 0; i <= disX; i++)
        {
            tem[pointCount] = new Vector2Int(st.x + countX, st.y);
            pointCount++;
            countX += dx;
        }
        for (int i = 0; i < disY; i++)
        {
            tem[pointCount] = new Vector2Int(ed.x, st.y+countY);
            pointCount++;
            countY += dy;
        }
        Vector2Int[] res = new Vector2Int[pointCount];
        for(int i = 0; i < pointCount; i++)
        {
            res[i] = tem[i];
        }
        return res;

    }

    public void setColorXY(int x, int y, Color c)
    {
        if (blockList[x + 50, y + 50] != null)
        {
            blockList[x + 50, y + 50].setColor(c);
        }
    }

    public void setColorXY(Vector2Int p, Color c)
    {
        if (blockList[p.x + 50, p.y + 50] != null)
        {
            blockList[p.x + 50, p.y + 50].setColor(c);
        }
    }

    public void createBlock(int x,int y ,float delay)
    {
        GameObject ttem = Instantiate(blockTem, opAreaTransform);
        MapBlock tem = ttem.GetComponent<MapBlock>();
        blockList[x+50,y+50] = tem;
        tem.show = true;
        tem.delay = delay;
        tem.showTime = blockShowTime;
        tem.backGroung = true;
        UF.setLocalPosition(ttem, new Vector2(x * 0.5f - 0.25f, y * 0.5f - 0.25f));

    }


    public void creatToken(int t,int x,int y,float delay)
    {
        if (blockList[x, y] != null)
        {
            GameObject ttem = Instantiate(tokenTem, opAreaTransform);
            MapBlock tem = ttem.GetComponent<MapBlock>();
            tokenList[tokenCount] = ttem.GetComponent<MapToken>();
            tokenCount++;
            tem.show = true;
            tem.delay = delay;
            tem.showTime = blockShowTime;
            tem.GetComponent<SpriteRenderer>().sprite = sm.getToken(t);
            UF.setLocalPosition(ttem, new Vector2(x * 0.5f + 0.025f, y * 0.5f));
        }
    }

    public void clean()
    {
        for(int i = 0; i < 100; i++)
        {
            for(int j = 0; j < 100; j++)
            {
                if (blockList[j, i] != null)
                {
                    blockList[j, i].transform.GetComponent<temCardAnime>().delayDestory(0.3f);
                    blockList[j, i] = null;
                }
            }
            
        }
        for(int i = 0; i < tokenCount; i++)
        {
            tokenList[i].transform.GetComponent<temCardAnime>().delayDestory(0.3f);
        }
    }

    public void showBox(Vector2 startPosition, Vector2 size)
    {
        Manager.closeAllCardDeck();
        waitClick = true;
        anime.startMove(startPosition, new Vector2(0, 0));
        anime.setSize(anime.initSize);
        anime.startScale(anime.initSize, size, 0.2f);
    }

    public void endBox()
    {
        anime.startScale(anime.getCurSize(), new Vector2(0, 0));
        Manager.openAllCardDeck();
        waitClick = false;
        clean();
    }


    // Start is called before the first frame update
    void Start()
    {
        mousePositionXY = new Vector2Int(0, 0);
        blockList = new MapBlock[100,100];
        blockCountX = 0;
        blockCountY = 0;
        tokenList = new MapToken[100];
        tokenCount = 0;
    }
    public Vector2Int mousePositionXY;
    public Vector2Int[] temPath;
    private void checkMousePosition()
    {
        if (!hoverItem.Hover)
        {
            mousePositionXY = new Vector2Int(0, 0);
        }
        else
        {
            int x = Mathf.FloorToInt((hoverItem.relaivePosition.x + 0.25f) / 0.5f);
            int y = Mathf.FloorToInt((hoverItem.relaivePosition.y + 0.25f) / 0.5f);
            mousePositionXY.x = x;
            mousePositionXY.y = y;
        }
    }
    // Update is called once per frame
    void Update()
    {
        checkMousePosition();
        cleanBackGroundAll();
        if(lastCardID == 3 || lastCardID == 8)
        {
            moveOpration();
        }
    }
    public Color moveBackColor0;
    public Color moveBackColor1;
    public Color moveBackColor2;
    public Color moveBackColor3;

    public void moveOpration()
    {
        temPath = getPath(moveStartPosition, mousePositionXY);
        DrawBlock(moveStartPosition, mousePositionXY, moveBackColor0, moveCount + 1, 1);
        DrawBlock(moveStartPosition, mousePositionXY, moveBackColor3, 5, 2);
        DrawBlock(moveStartPosition, mousePositionXY, moveBackColor1, moveCount + 1, 2);
        int endB = moveCount + 1;
        if (temPath.Length < moveCount + 1)
        {
            endB = temPath.Length;
        }

        setColorXY(temPath[endB - 1], moveBackColor2);
        for (int i = 0; i < returnDataCount; i++)
        {
            setColorXY(returnData[i], moveBackColor2);
        }
        if (waitClick && clickItem.lClick &&(!tuto || mousePositionXY == tutoPosition))
        {
            if (temPath.Length == 1)
            {
                returnData[returnDataCount] = temPath[0];
                returnDataCount++;
                moveCount -= 1;
            }
            else if (temPath.Length <= moveCount)
            {
                for (int i = 1; i < temPath.Length; i++)
                {
                    returnData[returnDataCount] = temPath[i];
                    returnDataCount++;
                }
                moveCount -= temPath.Length - 1;
                moveStartPosition = mousePositionXY;
            }
            else if (temPath.Length == moveCount + 1)
            {
                for (int i = 1; i < temPath.Length; i++)
                {
                    returnData[returnDataCount] = temPath[i];
                    returnDataCount++;
                }
                returnToManager(returnData);
            }

        }
    }



    public void returnToManager(Vector2Int[] data)
    {
        Manager.opration(lastCardID, lastPlayerName, data);
        endBox();
    }
}
