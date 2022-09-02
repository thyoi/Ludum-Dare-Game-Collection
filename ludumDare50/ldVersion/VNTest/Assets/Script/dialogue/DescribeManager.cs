using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescribeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public textReader reader;
    public SpriteRenderer showCard;
    public showItem curItem;
    private void Start()
    {
        curItem = new showItem();
        curItem.text = "";
        curItem.sp = null;
        showCard.color = new Color(1, 1, 1, 0);
        clear();
    }
    public void setContent(string s,Sprite sp = null)
    {
        if (s != curItem.text || sp != curItem.sp)
        {
            //UF.print(s);
            reader.readText(s);
            if (sp != null)
            {
                showCard.color = new Color(1, 1, 1, 1);
                showCard.sprite = sp;
            }
            else
            {
                showCard.color = new Color(1, 1, 1, 0);
            }
            curItem.text = s;
            curItem.sp = sp;
        }
    }
    public void setContent(showItem si)
    {
        
        setContent(si.text, si.sp);
    }

    public void clear()
    {
        setContent("");
    }

}
[System.Serializable]
public class showItem
{
    public string text;
    public Sprite sp = null;
    public showItem()
    {
        text = "";
        sp = null;
    }
    public showItem(string s, Sprite ss = null)
    {
        text = s;
        sp = ss;
    }
}
