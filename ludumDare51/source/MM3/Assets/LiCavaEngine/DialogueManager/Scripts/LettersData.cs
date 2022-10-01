using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettersData
{
    #region LetterClassify Funtions
    static public bool CharIsOperation(char c)
    {
        return c < 32 || c == 127;
    }

    static public bool CharIsLowercase(char c)
    {
        return (c <= 'z' && c >= 'a');
    }

    static public bool CharIsUppercase(char c)
    {
        return (c <= 'Z' && c >= 'A');
    }

    static public bool CharIsNum(char c)
    {
        return (c < '9' && c >= '0');
    }

    static public bool CharIsAscii(char c)
    {
        return c < 128;
    }

    static public bool CharIsMark(char c)
    {
        return CharIsAscii(c) && !CharIsUppercase(c) && !CharIsLowercase(c) && !CharIsNum(c) && !CharIsOperation(c);
    }

    #endregion

    public LetterHeadData Default;
    public LetterHeadData DefaultAscii;
    public LetterHeadData DefaultAlphabet;
    public LetterHeadData DefaultUpper;
    public LetterHeadData DefaultLower;
    public LetterHeadData DefaultNum;
    public LetterHeadData DefaultOperation;
    public LetterHeadData DefaultMark;
    public LetterHeadData DefaultNoneAscii;

    public LetterHeadData[] asciiDic;
    public Dictionary<char, LetterHeadData> noneAsciiDic;

    public LettersData()
    {
        initDic();
    }

    public LettersData(float defaultTime)
    {
        Default.intervalTime = defaultTime;
        initDic();
    }

    private void initDic()
    {
        Default.set = true;
        asciiDic = new LetterHeadData[128];
        noneAsciiDic = new Dictionary<char, LetterHeadData>();
    }

    public LetterHeadData LetterData(char c)
    {
        if (LettersData.CharIsAscii(c))
        {
            return asciiDic[c].set ? asciiDic[c] : AsciiLetterData(c);
        }
        else
        {
            return noneAsciiDic.ContainsKey(c) ? noneAsciiDic[c] : DefaultNoneAscii.set ? DefaultNoneAscii : Default;
        }
    }

    private LetterHeadData AsciiLetterData(char c)
    {
        if (LettersData.CharIsNum(c))
        {
            return DefaultNum.set ? DefaultNum : DefaultAscii.set ? DefaultAscii : Default;
        }
        else if (LettersData.CharIsOperation(c))
        {
            return DefaultOperation.set ? DefaultOperation : DefaultAscii.set ? DefaultAscii : Default;
        }
        else if (LettersData.CharIsLowercase(c))
        {
            return DefaultLower.set ? DefaultLower : DefaultAlphabet.set ? DefaultAlphabet : DefaultAscii.set ? DefaultAscii : Default;
        }
        else if (LettersData.CharIsUppercase(c))
        {
            return DefaultUpper.set ? DefaultUpper : DefaultAlphabet.set ? DefaultAlphabet : DefaultAscii.set ? DefaultAscii : Default;
        }
        else
        {
            return DefaultMark.set ? DefaultMark : DefaultAscii.set ? DefaultAscii : Default;
        }
    }

    public void SetLetterData(char c, byte sound, float intervalTime)
    {
        if (LettersData.CharIsAscii(c))
        {
            asciiDic[c] = new LetterHeadData(sound, intervalTime);
        }
        else
        {
            noneAsciiDic.Add(c, new LetterHeadData(sound, intervalTime));
        }
    }



}


public struct LetterHeadData
{
    public bool set;
    public byte sound;
    public float intervalTime;

    public LetterHeadData(bool set, byte sound, float intervalTime)
    {
        this.set = set;
        this.sound = sound;
        this.intervalTime = intervalTime;
    }

    public LetterHeadData(byte sound, float intervalTime)
    {
        this.set = true;
        this.sound = sound;
        this.intervalTime = intervalTime;
    }
}
