using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCountroler : MonoBehaviour
{
    public enum Line
    {
        none,
        up,
        down,
        left,
        right,
    }
    public bool Active;
    public Rect rect;
    public bool mainBrick;
    public bool wall;
    public Boomable boom;

    private float savetyDisSqure;
    // Start is called before the first frame update
    void Awake()
    {
        Dis(rect.x, rect.y);
        Dis(rect.x, rect.y+rect.height);
        Dis(rect.x + rect.width, rect.y);
        Dis(rect.x + rect.width, rect.y + rect.height);
    }

    private void Dis(float x,float y)
    {
        float tem =  x * x + y * y;
        if (tem > savetyDisSqure)
        {
            savetyDisSqure = tem;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool InRange(Vector2 v)
    {
        v -= (Vector2)transform.position;
        return ((v.x * v.x + v.y * v.y) < savetyDisSqure);
    }

    public Line Collection(Vector2 v)
    {
        v -= (Vector2)transform.position;
        //UF.print(v);
        if (UF.PointInRect(v, rect))
        {
            float tem;
            float min = Mathf.Abs(v.y-rect.y-rect.height);
            Line res = Line.up;
            tem = Mathf.Abs(v.y - rect.y);
            if(min> tem)
            {
                min = tem;
                res = Line.down;
            }
            tem = Mathf.Abs(v.x - rect.x-rect.width);
            if (min > tem)
            {
                min = tem;
                res = Line.right;
            }
            tem = Mathf.Abs(v.x - rect.x);
            if (min > tem)
            {
                res = Line.left;
            }
            return res;
        }
        else
        {
            return Line.none;
        }
    }


    public void Hit()
    {
        if(!wall && !mainBrick)
        {
            Active = false;
            boom.StartBoom();
        }
    }
    
}
