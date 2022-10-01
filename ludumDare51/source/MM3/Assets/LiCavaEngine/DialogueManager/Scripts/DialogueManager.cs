using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public delegate void CharacterInitFunction(Character c);
    [TextArea(10, 10)]
    public string text;
    public float interTime;
    public float LineDistant;
    public float characterDistant;
    public int font = 1;
    public Vector2 startPosition;
    public CharacterInitFunction initFunction;
    public CharacterInitFunction endFunction;
    public AnimationCallBack callBack;
    public int timeLayer = 0;

    private int textCounter;
    private int lineCount;
    private float timeCounter;
    private Vector2 nextPosition;
    private float dt;
    private bool start;
    private List<Character> CharacterList = new List<Character>();

    public bool Finish
    {
        get { return textCounter >= text.Length; }
    }
    private void InitCount()
    {
        textCounter = 0;
        lineCount = 0;
        timeCounter = 0;
        nextPosition = startPosition;
    }

    public void ShowTextOrder(string s)
    {
        text = s;
        InitCount();
        start = true;
    }

    public void Start()
    {
        ShowText();
    }

    public void Update()
    {
        dt = TimeManager.DeltaTime(timeLayer);
        if (dt>0 && start)
        {
            updateText();
        }
    }

    private void updateText()
    {
        timeCounter -= dt;
        if (timeCounter <= 0)
        {
            timeCounter += interTime;
            ShowNextChar();
            textCounter++;
            if (textCounter >= text.Length)
            {
                CallBackOnce();
                start = false;
            }
        }
    }

    private void ShowNextChar()
    {
        if (text[textCounter] == '\n')
        {
            lineCount++;
            nextPosition.x = startPosition.x;
            nextPosition.y = startPosition.y - LineDistant * lineCount;
        }
        else if (text[textCounter] == ' ')
        {
            nextPosition.x += CharaterManager.Space(font) + characterDistant;
        }
        else
        {
            Character tem = CharaterManager.GetCharacter(text[textCounter],font);
            tem.SetPosition(nextPosition);
            if (initFunction != null)
            {
                initFunction(tem);
            }
            CharacterList.Add(tem);
            nextPosition.x += tem.Length + characterDistant;
        }
    }

    private void CallBackOnce()
    {
        if (callBack != null)
        {
            callBack();
            callBack = null;
        }
    }

    public void ShowText()
    {
        while (textCounter < text.Length)
        {
            ShowNextChar();
            textCounter++;
        }
        start = false;
    }

}
