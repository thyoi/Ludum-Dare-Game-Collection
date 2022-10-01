using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLineDrawer : MonoBehaviour
{
    static public float DefaultZValue = 0;
    static public DebugLineData DrawRect(Rect r)
    {
        GameObject g = GameObject.FindGameObjectWithTag("DebugLineManager");
        if (g is null)
        {
            return null;
        }
        else
        {
            return g.transform.GetComponent<DebugLineDrawer>().DrawRect(r, DefaultZValue);
        }
    }

    static public DebugLineData DrawRect(int x, int y, int w, int h)
    {
        return DrawRect(new Rect(x, y, w, h));
    }



    private List<DebugLineData> lineList = new List<DebugLineData>();
    private int idCounter = 0;

    public GameObject DebugLinePrefab;


    private int GenNewId()
    {
        return idCounter++;
    }

    public DebugLineData DebugLineWithPositionList(Vector3[] positions, bool loop = false)
    {
        DebugLineData res = Instantiate(DebugLinePrefab, this.transform).transform.GetComponent<DebugLineData>();
        res.id = GenNewId();
        res.Loop = loop;
        res.SetPositions(positions);
        lineList.Add(res);
        return res;
    }

    #region Delete and Clear Functions
    public void clear()
    {
        foreach(DebugLineData i in lineList)
        {
            Destroy(i.transform.gameObject);
        }
        lineList = new List<DebugLineData>();
    }

    private void DeleteLineWithIndex(int index)
    {
        if(index != -1)
        {
            Destroy(lineList[index].transform.gameObject);
        }
    }

    public void DelLineWithIndex(int index)
    {
        if(index>=0 && index < lineList.Count)
        {
            DeleteLineWithIndex(index);
        }
    }

    private int FindLineIndexWithId(int id)
    {
        for(int i = 0; i < lineList.Count; i++)
        {
            if(lineList[i].id == id)
            {
                return i;
            }
        }
        return -1;
    }

    public void DelLineWithId(int id)
    {
        DeleteLineWithIndex(FindLineIndexWithId(id));
    }

    public void DelLineWithInstance(DebugLineData ins)
    {
        if (ins != null)
        {
            DeleteLineWithIndex(ins.id);
        }
    }
    #endregion

    public DebugLineData DrawRect(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
    {
        return DebugLineWithPositionList(new Vector3[] { v1, v2, v3, v4 }, true);
    }

    public DebugLineData DrawRect(Vector2 v1, Vector2 v2, Vector2 v3, Vector2 v4, float z = 0)
    {
        return DrawRect(UF.Vector2To3(v1, z), UF.Vector2To3(v2, z), UF.Vector2To3(v3, z), UF.Vector2To3(v4, z));
    }

    public DebugLineData DrawRect(Rect r, float z = 0)
    {
        return DrawRect(new Vector2(r.x, r.y), new Vector2(r.x+r.width, r.y), new Vector2(r.x+r.width, r.y+r.height), new Vector2(r.x, r.y+r.height), z);
    }

    public DebugLineData DrawRect(float x, float y, float w, float h)
    {
        return DrawRect(new Rect(x, y, w, h), DefaultZValue);
    }

}

