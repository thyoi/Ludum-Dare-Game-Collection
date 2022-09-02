using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wordsData : MonoBehaviour
{
    private static int offsetLen = 10;
    private TextMesh tm;
    [HideInInspector]
    public Vector2 size = new Vector2(0,0);
    public void inti()
    {
        tm = this.GetComponent<TextMesh>();
        positionOffsetList = new Vector2[offsetLen];
        positionOffsetMark = new bool[offsetLen];
        for(int i = 0;i< offsetLen; i++)
        {
            positionOffsetList[i] = new Vector2(0, 0);
            positionOffsetMark[i] = false;
        }
    }
    public Vector2 position;
    private Vector2[] positionOffsetList;
    private bool[] positionOffsetMark;


    void Update()
    {
        updatePosition();
    }

    public void updatePosition()
    {
        Vector2 tem = position;
        for(int i = 0; i < offsetLen; i++)
        {
            if (positionOffsetMark[i])
            {
                tem += positionOffsetList[i];
            }
        }

        UF.setLocalPosition(this.gameObject, tem);
    }

    public void setContent(string s)
    {
        tm.text = s;
    }

    public string getContent()
    {
        return tm.text;
    }

    public void show()
    {

    }




}
