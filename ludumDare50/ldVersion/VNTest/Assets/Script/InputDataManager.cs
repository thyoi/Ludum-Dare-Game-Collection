using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDataManager : MonoBehaviour
{
    public InputDataManager()
    {
        accessableMark = new bool[accNum];
        accessableLayer = new bool[accNum];
        for(int i = 0; i < accNum; i++)
        {
            accessableMark[i] = true;
            accessableLayer[i] = true;
        }
    }

    public void openLayer(int n)
    {
        for(int i = 0; i < accNum; i++)
        {
            if (i != n)
            {
                accessableLayer[i] = true;
            }
        }
    }

    public void closeLayer(int n)
    {
        for (int i = 0; i < accNum; i++)
        {
            if (i != n)
            {
                accessableLayer[i] = false;
            }
        }
    }


    public static int accNum = 10;
    public mouseData mouseInput;
    public keyData[] keyInput;
    private bool[] accessableMark;
    private bool[] accessableLayer;

    public bool isAccessable(int n)
    {
        if (n < accessableMark.Length)
        {
            return accessableMark[n] && accessableLayer[n];
        }
        else
        {
            return false;
        }
    }

    public mouseData mouse(int n = 0)
    {
        if (isAccessable(n))
        {
            return mouseInput;
        }
        else
        {
            return null;
        }
    }

    public bool mouseD(int type, int n = 0)
    {
        mouseData tem = mouse(n);
        if(tem == null)
        {
            return false;
        }
        else
        {
            switch (type)
            {
                case(0):return tem.leftKey.down;
                case (1): return tem.rightKey.down;
                case (2): return tem.middleKey.down;
                default:return false;
            }

        }
    }

    public keyData key(string s,int n = 0)
    {
        if (isAccessable(n))
        {
            return getKey(s);
        }
        else
        {
            return null;
        }
    }

    public bool keyD(string s,int n = 0)
    {
        keyData tem = key(s, n);
        if(tem == null)
        {
            return false;
        }
        else
        {
            return tem.down;
        }
    }

    public keyData getKey(int n)
    {
        if (n < keyInput.Length)
        {
            return keyInput[n];
        }
        return null;
    }
    public keyData getKey(string n)
    {
        for(int i = 0; i < keyInput.Length; i++)
        {
            if(keyInput[i].keyName == n)
            {
                return keyInput[i];
            }
        }
        return null;
    }
    public keyData getMouse(int n)
    {
        switch (n)
        {
            case (0): return mouseInput.leftKey;
            case (1): return mouseInput.middleKey;
            case (2): return mouseInput.rightKey;
        }
        return null;
    }

    public Hoverable[] hoverObjects = new Hoverable[100];
    public Clickable[] clickObjects = new Clickable[100];
    public int hoverNum = 0;
    public int clickNum = 0;

    public void registeHover(Hoverable g)
    {
        hoverObjects[hoverNum] = g;
        hoverNum++;
    }

    public void registeClick(Clickable g)
    {
        clickObjects[clickNum] = g;
        clickNum++;
    }

    public void checkHover()
    {
        for(int i = 0; i < hoverNum; i++)
        {
            if (hoverObjects[i].Active )
            {
                hoverObjects[i].Hover = UF.pointInRect(mouseInput.position, hoverObjects[i].getArea());
                hoverObjects[i].relaivePosition = mouseInput.position - (Vector2)hoverObjects[i].gameObject.transform.position;
            }
        }
    }

    public void checkClick()
    {
        for (int i = 0; i < clickNum; i++)
        {
            if (accessableMark[clickObjects[i].level] && accessableLayer[clickObjects[i].level]&& clickObjects[i].Active && UF.pointInRect(mouseInput.position, clickObjects[i].getArea()))
            {
                clickObjects[i].lClick = mouseInput.leftKey.down;
                clickObjects[i].rClick = mouseInput.rightKey.down;
                clickObjects[i].lHold = mouseInput.leftKey.hold;
                clickObjects[i].rHold = mouseInput.rightKey.hold;
                clickObjects[i].relaivePosition = mouseInput.position - (Vector2)clickObjects[i].gameObject.transform.position;
            }
            else
            {
                clickObjects[i].lClick = false;
                clickObjects[i].rClick = false;
                clickObjects[i].lHold = false;
                clickObjects[i].rHold = false;
            }
        }
    }







    void Update()
    {
        inputSetter.setMouseData(mouseInput);
        inputSetter.setKeysData(keyInput);
        checkHover();
        checkClick();
    }

}
[System.Serializable]
public class mouseData
{
    public Vector2 position;
    public keyData leftKey;
    public keyData middleKey;
    public keyData rightKey;
    public mouseData()
    {
        leftKey = new keyData("leftKey");
        middleKey = new keyData("middleKey");
        rightKey = new keyData("rightKey");
    }
}

[System.Serializable]
public class keyData
{
    public string keyName;
    public bool hold;
    public bool down;
    public bool up;
    public keyData(string kn = "")
    {
        keyName = kn;
        hold = false;
        down = false;
        up = false;
    }

}