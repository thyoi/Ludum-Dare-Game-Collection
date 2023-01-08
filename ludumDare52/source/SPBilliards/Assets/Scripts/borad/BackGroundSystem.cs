using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSystem : MonoBehaviour
{
    public delegate void TriggetFunction(BackGrountUnit b);

    public GameObject BackGroundUnitProfab;
    public Vector2 InitPoint;
    public Vector2Int UnitMapSize;
    public BackGrountUnit[,] UnitMap;
    public float UnitDis;
    public float TransformTime;
    public GameObject[] BoomPrefab;

    private float TransformCount;
    private int TransformStep;
    private int TransformMaxStep;
    private int TransformType;
    private bool OnTransform;
    private float sqr3d2;
    private TriggetFunction curTransformFunction;
    private float dt;

    private void CreateMap()
    {
        sqr3d2 = Mathf.Sqrt(3)/2 * UnitDis;
        UnitMap = new BackGrountUnit[UnitMapSize.x, UnitMapSize.y];
        for(int i = 0; i < UnitMapSize.x;i++)
        {
            for(int j = 0; j < UnitMapSize.y; j++)
            {
                UnitMap[i, j] = Instantiate(BackGroundUnitProfab, transform).transform.GetComponent<BackGrountUnit>();
                UnitMap[i, j].transform.localPosition = PositionByIndex(i, j);
                UnitMap[i, j].index = new Vector2Int(i, j);
                if (turned(i,j))
                {
                    UF.SetRotationZ(UnitMap[i, j].transform, 180);

                }
            }
        }
    }

    private bool turned(int x,int y)
    {
        return (x % 2 == 0) ^ (y % 2 == 0);
    }

    private Vector2 PositionByIndex(int x, int y)
    {
        if(x%2 == 1)
        {
            if(y%2== 1)
            {
                return new Vector2(UnitDis / 2 * (x + 1), sqr3d2 * y + sqr3d2 / 3) + InitPoint;
            }
            else
            {
                return new Vector2(UnitDis / 2 * (x + 1), sqr3d2 * y + sqr3d2*2 / 3) + InitPoint;
            }
        }
        else
        {
            if (y % 2 == 1)
            {
                return new Vector2(UnitDis / 2 * (x + 1), sqr3d2 * y + sqr3d2 * 2 / 3) + InitPoint;
            }
            else
            {
                return new Vector2(UnitDis / 2 * (x + 1), sqr3d2 * y + sqr3d2 / 3) + InitPoint;
            }
        }
    }

    private void TriggetUnit(int x, int y, TriggetFunction f)
    {
        if(x>=0 && x<UnitMapSize.x && y>=0 && y < UnitMapSize.y)
        {
            f(UnitMap[x, y]);
        }
    }

    public void LineTransform(TriggetFunction f)
    {
        TransformCount = 0;
        TransformMaxStep = 70;
        TransformStep = -1;
        curTransformFunction = f;
        OnTransform = true;
    }


    public void UpdateTransform(float t)
    {
        TransformCount += t;
        if (TransformCount > TransformTime)
        {
            TransformCount = 0;
            TransformStep++;
            if (TransformStep >= TransformMaxStep)
            {
                OnTransform = false;
            }

            for(int i = 0; i < TransformStep; i++)
            {
                TriggetUnit(i, TransformStep - i - 1, curTransformFunction);
            }

        }
    }
    public void DownAt(int x, int y,float downV,float downT)
    {
        if (x >= 0 && x < UnitMapSize.x && y >= 0 && y < UnitMapSize.y)
        {
            UnitMap[x, y].DownBySet(downV, downT);
        }
    }
    public void CreateBoom(Vector2 position,int type)
    {


        BackGroundBoom tem = Instantiate(BoomPrefab[type]).transform.GetComponent<BackGroundBoom>();
        tem.system = this;
        Vector2 tv = position - InitPoint;
        tv.x /= UnitDis / 2;
        tv.y /= sqr3d2;
        Vector2Int ti = new Vector2Int(Mathf.FloorToInt(tv.x), Mathf.FloorToInt(tv.y));
        tem.StartPoint = ti;
        //Debug.Log(ti)
    }
    public void DownAt(Vector2Int v,float downV, float downT)
    {
        DownAt(v.x, v.y, downV, downT);
    }

    public void Show1()
    {
        LineTransform((BackGrountUnit b) =>
        {
            if (b.Turned())
            {
                b.Transform(1);
            }
            else
            {
                b.Transform(0);
            }
        });
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateMap();
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        if (OnTransform)
        {
            UpdateTransform(dt);
        }
    }
}



