using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//-c: choose block
//-nc: nagtive choose block
//-cn: choose number
//-e: endnum[]split with ','
//-f: img[]split with ','
//-sp: speed[]split with ','
//-sv: voice[]split with ','


public class InstructionReader
{
    public static InstructionReturn readInstruction(string s)
    {
        InstructionReturn res = new InstructionReturn();
        Parameter[] ptem = new Parameter[20];
        if(s.Length == 0)
        {
            return res;
        }
        if(s[0] != '/')
        {
            return res;
        }
        //string tem = s.Replace(" ", "");
        string tem = s;
        string[] ttem = tem.Split('-');
        res.name = ttem[0].Substring(1, ttem[0].Length-1);
        int n = 0;
        for(int i = 1; i < ttem.Length; i++)
        {
            string[] tttem = ttem[i].Split(':');
            ptem[n] = new Parameter();
            ptem[n].name = tttem[0];
            if (tttem.Length > 1)
            {
                ptem[n].stringData = UF.noVoidInString(tttem[1]);
            }
            else
            {
                ptem[n].stringData = "";
            }
            n++;
        }

        //res.parameterList = new Parameter[n];
        for(int i = 0; i < n; i++)
        {
            res.parameterList[i] = ptem[i];
        }
        res.parameterNum = n;
        return res;


    }
}


public class InstructionReturn
{
    public string name = null;
    public Parameter[] parameterList = new Parameter[50];
    public int parameterNum = 0;
    public void check()
    {
        UF.print("insName:" + name);
        for(int i = 0; i < parameterNum; i++)
        {
            UF.print("||" + parameterList[i].name + "::" + parameterList[i].stringData);
        }
    }

    public string getParameter(string n)
    {
        if(parameterList == null)
        {
            return null;
        }
        for(int i = 0; i < parameterNum; i++)
        {
            if(parameterList[i].name == n)
            {
                return parameterList[i].stringData;
            }
        }
        return null;
    }

    public bool hasParameter(string n)
    {
        return !(getParameter(n) == null);
    }

    public void setParameter(string n,string d)
    {
        if (hasParameter(n))
        {
            for (int i = 0; i < parameterNum; i++)
            {
                if (parameterList[i].name == n)
                {
                    parameterList[i].stringData = d;
                }
            }
        }
        else
        {
            parameterList[parameterNum] = new Parameter();
            parameterList[parameterNum].name = n;
            parameterList[parameterNum].stringData = d;
            parameterNum++;
        }
    }

}


public class Parameter
{
    public string name;
    public string stringData;
}
