using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PropertyCollection : IConverableToStringWithDepth
{
    public string name;
    public Dictionary<string, float> floatDic;
    public Dictionary<string, string> stringDic;
    public Dictionary<string, bool> boolDic;
    public Dictionary<string, PropertyCollection> propertyCollectionDic;

    public PropertyCollection()
    {
        name = "";
        floatDic = new Dictionary<string, float>();
        stringDic = new Dictionary<string, string>();
        boolDic = new Dictionary<string, bool>();
        propertyCollectionDic = new Dictionary<string, PropertyCollection>();
    }

    public void DeepCopyWithoutName(PropertyCollection p)
    {
        foreach(string s in p.floatDic.Keys)
        {
            floatDic[s] = p.floatDic[s];
        }
        foreach (string s in p.stringDic.Keys)
        {
            stringDic[s] = p.stringDic[s];
        }
        foreach (string s in p.boolDic.Keys)
        {
            boolDic[s] = p.boolDic[s];
        }
        foreach (string s in p.propertyCollectionDic.Keys)
        {
            propertyCollectionDic[s] = p.propertyCollectionDic[s];
        }
    }

    public void AddProperty(string name, float value)
    {
        floatDic[name] = value;
    }
    public void AddProperty(string name, string value)
    {
        stringDic[name] = value;
    }
    public void AddProperty(string name, bool value)
    {
        boolDic[name] = value;
    }
    public void AddProperty(string name, PropertyCollection value)
    {
        propertyCollectionDic[name] = value;
    }

    public PropretyCollectionStatic.propertyType getValueType(string name)
    {
        if (floatDic.ContainsKey(name))
        {
            return PropretyCollectionStatic.propertyType.flo;
        }
        else if (stringDic.ContainsKey(name))
        {
            return PropretyCollectionStatic.propertyType.str;
        }
        else if (boolDic.ContainsKey(name))
        {
            return PropretyCollectionStatic.propertyType.bol;
        }
        else if (propertyCollectionDic.ContainsKey(name))
        {
            return PropretyCollectionStatic.propertyType.pro;
        }
        else
        {
            return PropretyCollectionStatic.propertyType.Error;
        }
    }

    public string StringProperty(string name)
    {
        return stringDic[name];
    }

    public float FloatProperty(string name)
    {
        return floatDic[name];
    }

    public bool BoolProperty(string name)
    {
        return boolDic[name];
    }

    public PropertyCollection PropertyCollectionProperty(string name)
    {
        return propertyCollectionDic[name];
    }



    public string ToStringWithDepth(int depth, int tabNum)
    {
        if (depth <= 0)
        {
            return "[(Object) : PropertyCollection ]\n";
        }
        StringBuilder temString = new StringBuilder();
        foreach(string i in floatDic.Keys)
        {
            UF.GenPropertieDescription(temString, i, floatDic[i], depth, tabNum);
        }
        foreach (string i in boolDic.Keys)
        {
            UF.GenPropertieDescription(temString, i, boolDic[i], depth, tabNum);
        }
        foreach (string i in stringDic.Keys)
        {
            UF.GenPropertieDescription(temString, i, stringDic[i], depth, tabNum);

        }
        foreach (string i in propertyCollectionDic.Keys)
        {
            UF.GenPropertieDescription(temString, i, propertyCollectionDic[i], depth, tabNum);
        }


        return temString.ToString();
    }

}



