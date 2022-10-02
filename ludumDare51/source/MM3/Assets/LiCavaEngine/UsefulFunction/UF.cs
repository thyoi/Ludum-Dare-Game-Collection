using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

static public class UF
{
    #region OrderListOpration
    public enum compareReasult : byte
    {
        less,
        equal,
        greater
    }
    public delegate bool CompareTwoItems<T>(T a, T b);
    static private int front;
    static private int end;
    static private int cur;
    static public void IntOrderListInsert<T>(List<int> list,int item)
    {
        OrderListInsert<int>(list, item, (a, b) => a < b, (a, b) => a == b);
    }
    static public void FloatOrderListInsert(List<float> list, float item)
    {
        OrderListInsert<float>(list, item, (a, b) => a < b, (a, b) => a == b);
    }
    static public void OrderListInsert<T>(List<T> list, T item, CompareTwoItems<T> lessThan, CompareTwoItems<T> equalTo)
    {
        front = 0;
        end = list.Count;
        while (front != end)
        {
            cur = front + (end - front) / 2;
            if (equalTo(item,list[cur]))
            {
                list.Insert(cur + 1, item);
                return;
            }
            else if (lessThan(item,list[cur]))
            {
                end = cur;
            }
            else
            {
                front = cur + 1;
            }
        }
        list.Insert(front, item);
        return;
    }


    #endregion

    #region Log output, object serialization functions
    static public StringBuilder propertieDescriptionStringBuilder = new StringBuilder();
    static public StringBuilder tabStringBuilder = new StringBuilder();
    static public StringBuilder vectorStringBuilder = new StringBuilder();
    static public int g_tabLength = 8;
    static private string tabLengthString = UF.initTabLengthString();
    static public int g_defaultPrintDepth = 10;
    static public string objectInitString = "[-------------\n";
    static public string objectEndString = "-------------]\n";
    static public string objectLineInitString = "|";
    static public string listInitString = "{=============\n";
    static public string listEndString = "=============}\n";

    #region Print Functions
    static public void print(string s)
    {
        Debug.Log(s);
    }

    static public void print(char c)
    {
        if(c<=32 || c == 127)
        {
            Debug.Log("\\" + (int)c);
            return;
        }
        Debug.Log(c);
    }
    
    static public void print<T>(T s, int depth = -1)
    {
        if (depth == -1)
        {
            print(ConverToString(s, g_defaultPrintDepth));
        }
        else
        {
            print(ConverToString(s, depth));
        }
    }

    static public void print<T>(List<T> s, int depth = -1)
    {
        if (depth == -1)
        {
            print(ConverToString(s, g_defaultPrintDepth));
        }
        else
        {
            print(ConverToString(s, depth));
        }
    }

    static public void print<T>(T[] s, int depth = -1)
    {
        if (depth == -1)
        {
            print(ConverToString(s, g_defaultPrintDepth));
        }
        else
        {
            print(ConverToString(s, depth));
        }
    }

    static public string printString = string.Empty;
    #endregion

    #region ConverToString Functions
    static public string ConverToString<T>(List<T> list, int depth = 0, int tabNum = 0)
    {
        if (list.Count == 0)
        {
            return "[ List : " + typeof(T) + " , Count : " + list.Count + " ]";
        }
        if (MulLine(list))
        {
            return ConverMulLineListToString(list, depth, tabNum);
        }
        if(depth <= 0)
        {
            return "[ List : " + typeof(T) + " , Count : " + list.Count + " ]";
        }
        printString = "{";
        for (int i = 0; i < list.Count; i++)
        {

            printString += UF.ConverToString(list[i], depth - 1, tabNum+1);
            if (i == list.Count - 1)
            {
                printString += " ";
            }
            else
            {
                    printString += ", ";
            }
        }
        printString += "}";
        return printString;
    }
    static public string ConverMulLineListToString<T>(List<T> list, int depth = 0, int tabNum = 0)
    {
        if (depth <= 0)
        {
            return GenTabString(tabNum) + "[ List : " + typeof(T) + " , Count : " + list.Count + " ]\n";
        }
        printString = GenTabString(tabNum)+listInitString;
        for (int i = 0; i < list.Count; i++)
        {

            printString += UF.ConverToString(list[i], depth - 1, tabNum+1);

        }
        printString += GenTabString(tabNum) + listEndString;
        return printString;
    }
    static public string ConverToString<T>(T[] items, int depth = 0, int tabNum = 0)
    {
        return ConverToString(new List<T>(items),depth,tabNum);
    }
    static public string ConverIConverableToStringWithDepthToString(IConverableToStringWithDepth item,int depth = 0,int tabNum = 0)
    {
        string res = item.ToStringWithDepth(depth, tabNum);
        return GenTabString(tabNum) + objectInitString + res + GenTabString(tabNum) + objectEndString;
    }
    static public string ConverToString<T>(T item, int depth = 0, int tabNum = 0)
    {
        if(item == null)
        {
            return "NULL";
        }
        else if (item is IConverableToStringWithDepth)
        {
            return ConverIConverableToStringWithDepthToString(item as IConverableToStringWithDepth, depth, tabNum);
        }
        return item.ToString();
    }

    #endregion

    #region MulLine Functions
    static public bool MulLine<T>(List<T> list)
    {   
        if(list == null)
        {
            return false;
        }
        if(list.Count <= 0)
        {
            return false;
        }
        else
        {
            return MulLine(list[0]);
        }
    }

    static public bool MulLine<T>(T[] array)
    {
        return MulLine(new List<T>(array));
    }

    static public bool MulLine<T>(T item)
    {
        if (item is IConverableToStringWithDepth)
        {
            return true;
        }
        return false;
    }
    #endregion
    static private string initTabLengthString()
    {
        tabStringBuilder.Clear();
        for (int i = 0; i < g_tabLength; i++)
        {
            tabStringBuilder.Append(" ");
        }
        return tabStringBuilder.ToString();
    }
    static public string GenTabString(int tabNum)
    {
        tabStringBuilder.Clear();
        for (int i = 0; i < tabNum; i++)
        {
            tabStringBuilder.Append(tabLengthString);
        }
        return tabStringBuilder.ToString();
    }

    #region GenPropertie Functions
    static public void GenPropertieDescription<T>(StringBuilder temString, string name, T value, int depth, int tabNum)
    {
        temString.Append(GenTabString(tabNum));
        temString.Append(objectLineInitString);
        temString.Append(name);
        temString.Append(" : ");
        if (value == null)
        {
            temString.Append("NULL");
        }
        else
        {
            if (MulLine(value))
            {
                temString.Append("\n");
            }
            temString.Append(UF.ConverToString(value, depth - 1, tabNum + 1));
        }
        if (!(MulLine(value)))
        {
            temString.Append("\n");
        }

    }
    static public void GenPropertieDescription<T>(StringBuilder temString, string name, List<T> value , int depth, int tabNum)
    {
        temString.Append(GenTabString(tabNum));
        temString.Append(objectLineInitString);
        temString.Append(name);
        temString.Append(" : ");
        if (value == null)
        {
            temString.Append("NULL");
        }
        else
        {
            if (MulLine(value))
            {
                temString.Append("\n");
            }
            temString.Append(UF.ConverToString(value, depth - 1, tabNum + 1));
        }
        if (!(MulLine(value)))
        {
            temString.Append("\n");
        }

    }
    static public void GenPropertieDescription<T>(StringBuilder temString, string name, T[] value, int depth, int tabNum)
    {
        temString.Append(GenTabString(tabNum));
        temString.Append(objectLineInitString);
        temString.Append(name);
        temString.Append(" : ");
        if (value == null)
        {
            temString.Append("NULL");
        }
        else
        {
            if (MulLine(value))
            {
                temString.Append("\n");
            }
            temString.Append(UF.ConverToString(value, depth - 1, tabNum + 1));
        }
        if (!(MulLine(value)))
        {
            temString.Append("\n");
        }

    }
    #endregion
    #endregion

    #region Convert Data Type
    static public byte ConvertIntToByte(int i)
    {
        if (i < Byte.MinValue)
        {
            return byte.MinValue;
        }
        else if(i> Byte.MaxValue)
        {
            return byte.MaxValue;
        }
        else
        {
            return (Byte)i;
        }
    }
    static public int ConvertFloatToInt(float f)
    {
        if (f <= int.MinValue)
        {
            return int.MinValue;
        }
        else if(f> int.MaxValue)
        {
            return int.MaxValue;
        }
        else
        {
            return (int)f;
        }
    }

    static public Vector3 Vector2To3(Vector2 v,float z = 0)
    {
        return new Vector3(v.x, v.y, z);
    }

    static public ushort ConvertCharToShort(char c)
    {
        return (ushort)Convert.ToInt16(c);
    }
    #endregion

    #region BitOperation

    static public bool GetBit(ushort value,int Index)
    {
        return (value & (1 << Index)) == 0;
    }

    static public float ConverStringToFloat(string s)
    {
        string[] tem = s.Split('.');
        if(tem.Length == 1)
        {
            return ConverStringBeforePoint(tem[0]);
        }
        else
        {
            return ConverStringBeforePoint(tem[0])+ ConverStringAfterPoint(tem[1]);
        }
    }


    static public int ConverStringToInt(string s)
    {
        int res = 0;
        bool negtive = s[0] == '-';
        for(int i =negtive?1:0;i<s.Length;i++)
        {
            res += s[i] - (int)'0';
            if (i != s.Length - 1)
            {
                res *= 10;
            }
        }
        return negtive?-1*res:res;
    }
    static private float ConverStringBeforePoint(string s)
    {
        float res = 0;
        bool negtive = s[0] == '-';
        for (int i = negtive ? 1 : 0; i < s.Length; i++)
        {
            res += s[i] - (int)'0';
            if (i != s.Length - 1)
            {
                res *= 10;
            }
        }
        return negtive ? -1 * res : res;
    }

    static private float ConverStringAfterPoint(string s)
    {
        float res = 0;
        float t = 0.1f;
        foreach(char c in s)
        {
            res += (c - (int)'0') * t;
            t /= 10;
        }
        return res;
    }

    static public bool ConverStringToBool(string s)
    {
        if(s[0] == 't' || s[0] == 't')
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    #region Math Function
    static public int Pow(int a, int b)
    {
        return ConvertFloatToInt(Mathf.Pow(a, b));
    }

    static public float Pow(float a, float b)
    {
        return Mathf.Pow(a, b);
    }

    static public bool LongerBlockDistance(Vector2 a, Vector2 b)
    {
        return (Mathf.Abs(a.x) + Mathf.Abs(a.y)) > (Mathf.Abs(b.x) + Mathf.Abs(b.y));
    }

    static public float Lerp(float a, float b, float t)
    {
        return a + (b - a) * t;
    }

    static public Vector2 Lerp(Vector2 a, Vector2 b, float t)
    {
        return a + (b - a) * t;
    }

    #endregion


    static public float PixelLength = 1.0f / 54.0f;
    static public void SetPosition(Transform t, Vector2 v)
    {
        Vector3 tem = t.position;
        tem.x = v.x;
        tem.y = v.y;
        t.position = tem;
    }
    static public void SetLocalPosition(Transform t, Vector2 v)
    {
        Vector3 tem = t.localPosition;
        tem.x = v.x;
        tem.y = v.y;
        t.localPosition = tem;
    }

    static public bool PointInRect(Vector2 point, float x,float y,float w,float h)
    {
        return (point.x > x && point.x < (x + w) && point.y > y && point.y < (y + h));
    }
    static public bool PointInRect(Vector2 point, Rect r)
    {
        return PointInRect(point,r.x,r.y,r.width,r.height);
    }

    static public bool Equile(int a,int b,int c,int differ)
    {
        return a == b && a == c && a != differ;
    }

    static public float NomLen(Vector2 v)
    {
        return Mathf.Sqrt(v.x * v.x + v.y * v.y);
    }
}