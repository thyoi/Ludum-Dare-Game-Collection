using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ValueCountrolerManager
{
    static public string ValueDataUndefinedName = "any_undefined";
    static public string ValueDataDefaultName = "any_default";
    static private Dictionary<string, ValueDataHeadStrust> valueDataHeadInitDic;
    static private byte valueDataNameDicCount;


    public enum OprationName : byte
    {
        any_ignore,
        any_overwrite,
        bool_and,
        bool_or,
        bool_xor,
        bit_and,
        bit_or,
        bit_xor,
        num_add,
        num_sub,
        num_mul,
        num_div,
        num_pow
    }

    static public string[] oprationExpressionMark = new string[] { "", "", "&&", "||", "^", "&", "|", "^", "+", "-", "*", "/", "^" };

    static ValueCountrolerManager()
    {
        valueDataNameDicCount = 0;
        valueDataHeadInitDic = new Dictionary<string, ValueDataHeadStrust>();
        AddValueDataHeadInitData(ValueDataUndefinedName, 0, ValueCountrolerManager.OprationName.any_ignore, 0);
        AddValueDataHeadInitData(ValueDataDefaultName, 0, ValueCountrolerManager.OprationName.any_ignore, 0);
    }

    static private void AddValueDataHeadInitData(string name, sbyte order, OprationName opration, byte flag = 0)
    {
        valueDataHeadInitDic.Add(name, new ValueDataHeadStrust(valueDataNameDicCount, order, opration, flag));
        valueDataNameDicCount++;
    }

    static public string GetNameByNameIndex(byte nameIndex)
    {
        foreach (string s in valueDataHeadInitDic.Keys)
        {
            if (valueDataHeadInitDic[s].nameIndex == nameIndex)
            {
                return s;
            }

        }
        return ValueDataUndefinedName;
    }

    static public bool ContainName(string name)
    {
        return valueDataHeadInitDic.ContainsKey(name);
    }

    static public byte GetNameIndexByName(string name)
    {
        if (!ContainName(name))
        {
            return 0;
        }
        else
        {
            return valueDataHeadInitDic[name].nameIndex;
        }
    }

    static public string GetOperationNameByOprationIndex(byte operationIndex)
    {
        return Enum.GetName(typeof(OprationName), operationIndex);
    }

    static public string GetOperationNameByOprationIndex(OprationName operation)
    {
        return GetOperationNameByOprationIndex((byte)operation);
    }

    static public string GetOperationExpressionMarkByOprationIndex(byte operationIndex)
    {
        if (operationIndex < 0 || operationIndex >= oprationExpressionMark.Length)
        {
            return "?";
        }
        else
        {
            return oprationExpressionMark[operationIndex];
        }
    }

    static public string GetOperationExpressionMarkByOprationIndex(OprationName operationIndex)
    {
        return GetOperationExpressionMarkByOprationIndex((byte)operationIndex);
    }

    static public ValueDataHeadStrust ValueHeadDataWithName(string name)
    {
        if (valueDataHeadInitDic.ContainsKey(name))
        {
        return valueDataHeadInitDic[name];
        }
        else
        {
            return valueDataHeadInitDic[ValueDataUndefinedName];
        }
    }

    static public ValueDataHeadStrust ValueHeadDataWithNameIndex(byte name)
    {
        return ValueHeadDataWithName(GetNameByNameIndex(name));
    }

}
