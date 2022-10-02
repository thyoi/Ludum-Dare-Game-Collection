using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level8 : MonoBehaviour
{
    public Level myLevel;
    private bool start;

    public void Awake()
    {
        myLevel.initFunction = InitLevel;
        myLevel.endFunction = EndLevel;
    }
    public void InitLevel()
    {
        Init();
        start = true;
    }
    public void EndLevel()
    {
        start = false;
    }

    public void Init()
    {
        PlayerTurn();
        borad = new int[size.x, size.y];
        boradGround = new int[size.x, size.y];
        boomBorad = new Boomable[size.x, size.y];
        boomBoradGround = new Boomable[size.x, size.y];
        Set(0, 0, 2);
        Set(2, 0, 2);
        Set(3, 1, 2);
        Set(3, 2, 2);
        Set(1, 3, 2);
        Set(1, 4, 2);
        Set(2, 5, 2);
        Set(3, 4, 2);
        Set(6, 4, 2);
        Set(6, 2, 2);
        Set(6, 1, 2);
        Set(0, 2, 1);
        Set(8, 2, 4);
        Set(8, 3, 4);
        Set(8, 4, 4);
        boradGround[4,0] = 1;
        boradGround[4, 1] = 1;
        boradGround[4, 2] = 1;
        boradGround[5, 0] = 1;
        boradGround[5, 1] = 1;
        boradGround[5, 2] = 1;
        boradGround[6, 0] = 1;
        boradGround[4, 4] = 1;
        boradGround[4, 5] = 1;
        boradGround[5, 4] = 1;
        boradGround[5, 5] = 1;
        PlayerPosition = new Vector2Int(0, 2);
    }

    public GameObject[] tokens;
    public Vector2 offset;
    public Vector2Int size;
    public Color color;
    public Sprite[] boxSprites;

    private Vector2Int PlayerPosition;
    private int[,] borad;
    private int[,] boradGround;
    private Boomable[,] boomBorad;
    private Boomable[,] boomBoradGround;
    private bool playerTurn;

    public void PlayerTurn()
    {
        playerTurn = true;
    }

    public Boomable CreateToken(Vector2 p, int t, AnimationCallBack callback = null)
    {
        GameObject tem = Instantiate(tokens[t - 1], transform);
        UF.SetLocalPosition(tem.transform, p);
        StageManager.InitItem(tem.transform.GetComponent<LeaveStage>());
        tem.transform.GetComponent<ScaleWithCurveAnimation2D>().StartAnimation(callback);
        tem.transform.GetComponent<SpriteRenderer>().color = color;
        return tem.transform.GetComponent<LeaveStage>().boom;
    }

    public Boomable CreateToken(int x, int y, int t, AnimationCallBack callback = null)
    {
        return CreateToken(IntToPosition(x, y), t, callback);
    }

    public Vector2 IntToPosition(int x, int y)
    {
        return new Vector2(32 * UF.PixelLength * (x - 2) + offset.x, 32 * UF.PixelLength * (y - 2) + offset.y);
    }

    public void Set(int x, int y, int t)
    {
        if (t == 4)
        {
            boomBoradGround[x, y] = CreateToken(x, y, t);
            boradGround[x, y] = t;
        }
        else
        {
            boomBorad[x, y] = CreateToken(x, y, t);
            borad[x, y] = t;
        }
    }

    public void Move(Vector2Int position, Vector2Int dri, AnimationCallBack callback = null)
    {
        PositionInPathWithCurveAnimation2D tem;
        tem = boomBorad[position.x, position.y].transform.parent.GetComponent<PositionInPathWithCurveAnimation2D>();
        tem.initPosition = IntToPosition(position.x, position.y);
        tem.endPosition = IntToPosition(position.x + dri.x, position.y + dri.y);
        boomBorad[position.x + dri.x, position.y + dri.y] = boomBorad[position.x, position.y];
        boomBorad[position.x, position.y] = null;
        borad[position.x + dri.x, position.y + dri.y] = borad[position.x, position.y];
        borad[position.x, position.y] = 0;
        tem.StartAnimation(callback);
    }
    public bool TryMove(Vector2Int position, Vector2Int dri, AnimationCallBack callback = null)
    {
        Vector2Int dis = position + dri;
        if (dis.x < 0 || dis.x >= size.x || dis.y < 0 || dis.y >= size.y || boradGround[dis.x, dis.y] == 1)
        {
            return false;
        }
        if (borad[dis.x, dis.y] == 0)
        {
            Move(position, dri, callback);
            return true;
        }
        else
        {
            if (TryMove(dis, dri))
            {
                Move(position, dri, callback);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void PlayerMove(Vector2Int dri)
    {
        if (TryMove(PlayerPosition, dri, () => { Judge(); PlayerTurn(); }))
        {
            PlayerPosition += dri;
            playerTurn = false;
            SoundManager.PlaySound("push");
        }
    }

    public void Update()
    {
        if (start && playerTurn)
        {
            Vector2Int tem = Vector2Int.zero;
            tem.x = Input.GetKeyDown(KeyCode.A) ? (Input.GetKeyDown(KeyCode.D) ? 0 : -1) : (Input.GetKeyDown(KeyCode.D) ? 1 : 0);
            tem.y = Input.GetKeyDown(KeyCode.S) ? (Input.GetKeyDown(KeyCode.W) ? 0 : -1) : (Input.GetKeyDown(KeyCode.W) ? 1 : 0);
            if (tem.x != 0 || tem.y != 0)
            {
                PlayerMove(tem);
            }
        }
    }

    public void Judge()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                if (borad[i, j] == 2 && boradGround[i, j] == 4)
                {
                    borad[i, j] = 3;
                    boradGround[i, j] = 0;
                    boomBoradGround[i, j].StartBoom();
                    boomBorad[i, j].transform.parent.GetComponent<FrameAnimation>().SetSprites(boxSprites);
                    SoundManager.PlaySound("boxset");
                }
            }
        }
        JudgeMatch();
    }
    public void JudgeMatch(AnimationCallBack callback = null)
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                if (i < size.x - 2 && UF.Equile(borad[i, j], borad[i + 1, j], borad[i + 2, j], 0))
                {
                    TokenBoom(i, j, callback);
                    TokenBoom(i + 1, j);
                    TokenBoom(i + 2, j);
                    SoundManager.PlaySound("boom1");
                    return;
                }
                if (j < size.y - 2 && UF.Equile(borad[i, j], borad[i, j + 1], borad[i, j + 2], 0))
                {
                    TokenBoom(i, j, callback);
                    TokenBoom(i, j + 1);
                    TokenBoom(i, j + 2);
                    SoundManager.PlaySound("boom1");
                    return;
                }
                if (i < size.x - 2 && j < size.y - 2 && UF.Equile(borad[i, j], borad[i + 1, j + 1], borad[i + 2, j + 2], 0))
                {
                    TokenBoom(i, j, callback);
                    TokenBoom(i + 1, j + 1);
                    TokenBoom(i + 2, j + 2);
                    SoundManager.PlaySound("boom1");
                    return;
                }
                if (i < size.x - 2 && j > 1 && UF.Equile(borad[i, j], borad[i + 1, j - 1], borad[i + 2, j - 2], 0))
                {
                    TokenBoom(i, j, callback);
                    TokenBoom(i + 1, j - 1);
                    TokenBoom(i + 2, j - 2);
                    SoundManager.PlaySound("boom1");
                    return;
                }
            }
        }
    }

    private void TokenBoom(int x, int y, AnimationCallBack callback = null)
    {
        borad[x, y] = 0;
        boomBorad[x, y].StartBoom(callback);
    }
}
