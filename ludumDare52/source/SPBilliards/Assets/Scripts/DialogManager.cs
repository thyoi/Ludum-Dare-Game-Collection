using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public Sprite[] lower;
    public Sprite[] Upper;
    public Sprite[] mark;
    public Sprite[] num;
    public GameObject ChaPrefab;
    public GameObject empty;


    public float lineDis;
    public float chaDis;

    public float showTime;

    public string content;
    public UF.AnimeCallback CallBack;
    public Color DefaultColor = Color.white;
    public bool Silent;


    private Transform container;
    private float showCount;
    private int charCount;
    private Vector2 nextPosition;
    private bool finish;
    private bool onShow;
    private bool PlaySound;

    private float dt;
    private bool auto;

    public void Clicked()
    {
        if (!auto)
        {
            if (onShow)
            {
                showTime = 0;
                PlaySound = false;
            }
            if (finish)
            {

                finish = false;
                if (CallBack != null)
                {
                    CallBack();
                }
            }
        }
    }

    public void clean()
    {
        if (container != null)
        {
            Destroy(container.gameObject);
        }
    }
    public void ShowContent(string s,UF.AnimeCallback cb = null)
    {
        clean();
        auto = false;
        PlaySound = true;
        onShow = true;
        finish = false;
        showCount = 0; 
        charCount = -1;
        nextPosition = Vector2.zero;
        content = s;
        CallBack = cb;
        container = Instantiate(empty).transform;
        container.parent = transform;
        container.localPosition = new Vector2(0, 0);
        container.localScale = new Vector2(1, 1);
        showTime = 0.04f;
    }

    public void ShowContent(string s, bool a,UF.AnimeCallback cb = null)
    {
        ShowContent(s, cb);
        auto = a;
    }

    public void UpdateShow(float t)
    {
        showCount += t;
        if (showCount >= showTime)
        {
            showCount = 0;
            charCount++;
            if (charCount >= content.Length-1)
            {
                charCount = content.Length-1;
                onShow = false;
                finish = true;
                if (auto)
                {
                    finish = false;
                    if (CallBack != null)
                    {
                        CallBack();
                    }
                }

            }
            SetCharacter(content[charCount]);

        }
    }

    public void SetCharacter(char c)
    {
        Transform tem = CreateCha(c);
        tem.localPosition = nextPosition;
        Sprite tt = tem.GetComponent<SpriteRenderer>().sprite;
        if (tt != null)
        {
            nextPosition += Vector2.right * (tt.rect.width / 100f + chaDis);
            if (PlaySound && !Silent)
            {
                SoundManager.Play("char");
            }
        }
        else
        {
            if(c == ' ')
            {
                nextPosition += Vector2.right * (10f / 100f + chaDis);
            }
            else if(c == '\n')
            {
                nextPosition += Vector2.down * lineDis;
                nextPosition.x = 0;
            }
        }

    }

    public Transform CreateCha(char c)
    {
        Transform res = Instantiate(ChaPrefab).transform;
        res.parent = container;
        res.localScale = new Vector2(1, 1);
        if (c<='z' && c >= 'a') {
            res.GetComponent<SpriteRenderer>().sprite = lower[c - 'a'];
        }
        else if(c<='Z' && c > 'A')
        {
            res.GetComponent<SpriteRenderer>().sprite = Upper[c - 'A'];
        }
        else if(c<='9' && c >= '0')
        {
            res.GetComponent<SpriteRenderer>().sprite = num[c - '0'];
        }
        else if(c == ',')
        {
            res.GetComponent<SpriteRenderer>().sprite = mark[0];
        }
        else if(c == '.')
        {
            res.GetComponent<SpriteRenderer>().sprite = mark[1];
        }
        else if(c == '!')
        {
            res.GetComponent<SpriteRenderer>().sprite = mark[2];
        }
        else if(c == '?')
        {
            res.GetComponent<SpriteRenderer>().sprite = mark[3];
        }
        else if (c == '\'')
        {
            res.GetComponent<SpriteRenderer>().sprite = mark[4];
        }
        else
        {
            res.GetComponent<SpriteRenderer>().sprite = null;
        }
        res.GetComponent<SpriteRenderer>().color = DefaultColor;
        return res;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dt = TimeManager.DT();
        if (onShow)
        {
            UpdateShow(dt);
        }
    }
}
