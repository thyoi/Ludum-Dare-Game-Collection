using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UF 
{

    public static void print(string[] s,int n= -1)
    {
        if (s == null)
        {
            return;
        }
        if(n < 0 || n>s.Length)
        {
            n = s.Length;
        }
        for(int i = 0; i < n; i++)
        {
            print(s[i]);
        }

    }
    public static void print(string n)
    {
        if(n == null)
        {
            return;
        }
        n.Replace(' ', '^');
        n.Replace('\n', '@');
        n.Replace('\r', '&');
        //Debug.Log(n + "  lenght:"+ n.Length);
        Debug.Log(n);
    }

    public static void print(float f)
    {
        print("" + f);
    }

    public static void print(int i)
    {
        print("" + i);
    }

    public static void print(Vector2 v)
    {
        print("x:"+v.x+"||y:"+v.y);
    }
    public static void print(bool a)
    {
        if (a)
        {
            print("true");
        }
        else
        {
            print("false");
        }
    }


    public static Vector2 getPosition(GameObject g)
    {
        return (Vector2)(g.transform.position);
    }

    public static void setPosition(GameObject g,Vector2 p)
    {
        Vector3 tem = new Vector3(p.x, p.y, g.transform.position.z);
        g.transform.position = tem;
    }

    public static Vector2 getLocalPosition(GameObject g)
    {
        return (Vector2)(g.transform.localPosition);
    }

    public static void setLocalPosition(GameObject g, Vector2 p)
    {
        Vector3 tem = new Vector3(p.x, p.y, g.transform.localPosition.z);
        g.transform.localPosition = tem;
    }


    public static string[] slice(string[] sl,int s,int end = -1)
    {
        if (end < 0)
        {
            int n = sl.Length - s + end + 1;
            string[] res = new string[n];
            for(int i = 0; i < n; i++)
            {
                res[i] = sl[i + s];
            }
            return res;
        }
        return null;
    }

    public static string slice(string ss,int s,int e)
    {
        string res = "";
        int start = s;
        
        if (s < 0)
        {
            start = ss.Length + s;
        }
        int len = e - start;
        if (e < 0)
        {
            len = ss.Length - start + e + 1;
        }

        for(int i = 0; i < len; i++)
        {
            res += ss[i + start];
        }


        return res;
    }

    public static void setColorA(SpriteRenderer sr,float a)
    {
        Color c = sr.color;
        c.a = a;
        sr.color = c;
    }

    public static void setColor(SpriteRenderer sr, Color c)
    {
        Color cc = sr.color;
        cc.r = c.r;
        cc.g = c.g;
        cc.b = c.b;
        sr.color = cc;
    }
    public static int stringToInt(string s)
    {
        bool flag = (s[0] == '-');
        int res = 0;
        for(int i = 0; i < s.Length; i++)
        {
            
            if(flag && i == 0)
            {
                i++;
            }
            if(charToInt(s[i]) == -1)
            {
                break;
            }
            res *= 10;
            res += charToInt(s[i]);

        }
        if (flag)
        {
            res = -res;
        }
        return res;

    }


    public static float stringToFloat(string s)
    {
        bool flag = false;
        if(s[0] == '-')
        {
            flag = true;
            s = s.Substring(1, s.Length - 1);
        }
        string[] sp = s.Split('.');
        float res = 0;
        if (sp.Length > 1)
        {
            res =  (stringToInt(sp[0]) + stringToFloatSmall(sp[1]));
        }
        else
        {
            res =  stringToInt(s);
        }

        if (flag)
        {
            return -res;
        }
        else
        {
            return res;
        }
    }

    public static float stringToFloatSmall(string s)
    {
        float res = 0;
        float bit = 1;
        for(int i = 0; i < s.Length; i++)
        {
            bit /= 10;
            res += bit * charToInt(s[i]);            
        }
        return res;
    }



    public static int charToInt(char c)
    {
        switch (c)
        {
            case ('0'):return 0;
            case ('1'):return 1;
            case ('2'): return 2;
            case ('3'): return 3;
            case ('4'): return 4;
            case ('5'): return 5;
            case ('6'): return 6;
            case ('7'): return 7;
            case ('8'): return 8;
            case ('9'): return 9;
            default:return -1;
        }
    }

    public static string[] noVoidEnd(string[] s)
    {
        while(s[s.Length-1] == "")
        {
            s = slice(s, 0, -2);
        }
        return s;
    }


    public static bool pointInRect(Vector2 p, Rect r)
    {
        return (p.x >= r.x && p.x < r.width + r.x && p.y >= r.y && p.y < r.y + r.height);
    }


    public static bool isCloseEnough(Vector2 a,Vector2 b)
    {
        return (Mathf.Abs(a.x - b.x) < 0.03f && Mathf.Abs(a.y - b.y) < 0.03f);
    }

    public static void setPositionZ(GameObject g, float z)
    {
        Vector3 tem = g.transform.position;
        tem.z = z;
        g.transform.position = tem;
    }
    public static string stringInChar(string ss,char s,char e)
    {
        int start = -1;
        int end = -1;
        for(int i = 0; i < ss.Length; i++)
        {
            if(start == -1 && ss[i] == s)
            {
                start = i;
            }
            if(end == -1 && ss[i] == e)
            {
                end = i;
            }
        }

        if (end <= start+1 || start == -1 || end == -1)
        {
            return "";
        }
        else
        {
            return ss.Substring(start + 1, end - start - 1);
        }
    }

    public static void setSize(GameObject g,Vector2 s)
    {
        SpriteRenderer sr = g.transform.GetComponent<SpriteRenderer>();
        if(sr!= null)
        {
            sr.size = s;
        }
    }

    public static string noVoidInString(string s)
    {
        int t = 0;
        for(int i = s.Length - 1; i >= 0; i--)
        {
            if(s[i] == ' ')
            {
                t++;
            }
            else
            {
                break;
            }
        }
        return s.Substring(0, s.Length - t);
    }
}



