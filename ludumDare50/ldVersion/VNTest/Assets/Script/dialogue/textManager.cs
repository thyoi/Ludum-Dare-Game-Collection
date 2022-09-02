using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textManager : MonoBehaviour
{
    public charData[] charDatas;
    public GameObject wordPrefab;
    public Vector2 wordSize;
    public Vector2 startPosition;
    public float lineDis;
    public int lineNum;
    public float wordsDis;
    public int wordsNum;
    public string text;
    public wordsData[] wordList;
    // Start is called before the first frame update
    void Start()
    {
        wordList = new wordsData[lineNum * wordsNum];
        for(int i = 0;i<lineNum * wordsNum; i++)
        {
            GameObject tem = Instantiate(wordPrefab, this.transform);
            wordList[i] = tem.GetComponent<wordsData>();
            wordList[i].inti();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //setPosition();
    }

    public void setPosition()
    {
        Vector2 tem = startPosition;
        for(int i = 0; i < lineNum; i++)
        {
            for(int j = 0; j < wordsNum; j++)
            {

                wordList[i * wordsNum + j].position = tem;
                tem.x += wordList[i * wordsNum + j].size.x + wordsDis;
            }
            tem.y -= lineDis;
            tem.x = startPosition.x;
        }
    }

    public float wordYPosition(int i)
    {
        float y = startPosition.y;
        y -= lineDis * i;
        return y;
    }
    public Vector2 wordsPosition(int i,int j)
    {
        float x = startPosition.x;
        float y = startPosition.y;
        x += (wordSize.x + wordsDis) * j;
        y -= lineDis * i;
        return new Vector2(x, y);
    }

    public void setContent(string t, bool clear = false)
    {
        setText(t, clear);
        setPosition();

    }
    public void setText(string t,bool clear = false)
    {
        text = t;
        if (clear)
        {
            clearContent();
        }
        int tem = 0;
        for(int i = 0; i < lineNum; i++)
        {
            for(int j = 0; j < wordsNum; j++)
            {
                if (text.Length == tem)
                {
                    return;
                }

                if (text[tem] != ']')
                {
                    if(!clear && wordList[i * wordsNum + j].getContent() != ""+text[tem])
                    {
                        wordList[i * wordsNum + j].show();
                    }
                    wordList[i * wordsNum + j].setContent("" + text[tem]);
                    wordList[i * wordsNum + j].size = getCharSize(text[tem]);
                    tem++;
                }
                else
                {
                    tem++;
                    break;
                }
                
            }
        }
        
    }

    public Vector2 getCharSize(char c)
    {
        foreach(charData cd in charDatas)
        {
            if(cd.c == c)
            {
                return cd.size;
            }
        }
        return wordSize;
    }
    public void clearContent()
    {
        for(int i = 0; i < lineNum * wordsNum; i++)
        {
            wordList[i].setContent("");
        }
    }
}
[System.Serializable]
public class charData
{
    public char c;
    public Vector2 size;
}


