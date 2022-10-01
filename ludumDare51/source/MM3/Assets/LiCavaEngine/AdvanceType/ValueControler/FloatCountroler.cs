using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

//[System.Serializable]
public class FloatCountroler : IConverableToStringWithDepth
{
    private ValueDataHeadStrust head;
    [SerializeField]private float defaultValue;
    private List<FloatCountroler> factorList;

    #region Construct Functions
    public FloatCountroler()
    {
        head = ValueCountrolerManager.ValueHeadDataWithName(ValueCountrolerManager.ValueDataDefaultName);
        defaultValue = 0;
        factorList = null;
    }
    public FloatCountroler(float value)
    {
        head = ValueCountrolerManager.ValueHeadDataWithName(ValueCountrolerManager.ValueDataDefaultName);
        defaultValue = value;
        factorList = null;
    }

    public FloatCountroler(int value)
    {
        head = ValueCountrolerManager.ValueHeadDataWithName(ValueCountrolerManager.ValueDataDefaultName);
        defaultValue = value;
        factorList = null;
    }

    public FloatCountroler(string name, float value = 0)
    {
        head = ValueCountrolerManager.ValueHeadDataWithName(name);
        defaultValue = value;
        factorList = null;
    }

    public FloatCountroler(byte nameIndex, float value = 0)
    {
        head = ValueCountrolerManager.ValueHeadDataWithNameIndex(nameIndex);
        defaultValue = value;
        factorList = null;
    }

    public FloatCountroler(FloatCountroler ori)
    {
            DeepCopy(ori);
    }

    public void DeepCopy(FloatCountroler ori)
    {
        if (ori == null)
        {
            head = ValueCountrolerManager.ValueHeadDataWithName(ValueCountrolerManager.ValueDataDefaultName);
            defaultValue = 0;
            factorList = null;
        }
        else
        {
            head = ori.head;
            defaultValue = ori.defaultValue;
            DeepCopyFactorList(ori.factorList);
        }
    }

    public void DeepCopyFactorList(List<FloatCountroler> list)
    {
        if (list == null)
        {
            factorList = null;
        }
        else
        {
            factorList = new List<FloatCountroler>();
            foreach(FloatCountroler i in list)
            {
                factorList.Add(new FloatCountroler(i));
            }
        }
    }
    #endregion

    public byte NameIndex
    {
        get { return head.nameIndex; }
        set { head.nameIndex = value; }
    }

    public sbyte OrderIndex
    {
        get { return head.orderIndex; }
        set { head.orderIndex = value; }
    }

    public ValueCountrolerManager.OprationName OperationIndex
    {
        get { return head.operationIndex; }
        set { head.operationIndex = value; }
    }

    public bool IgnoreType
    {
        get { return OperationIndex == ValueCountrolerManager.OprationName.any_ignore; }
        set { OperationIndex = ValueCountrolerManager.OprationName.any_ignore; }
    }

    public bool OverwriteType
    {
        get { return OperationIndex == ValueCountrolerManager.OprationName.any_overwrite; }
        set { OperationIndex = ValueCountrolerManager.OprationName.any_overwrite; }
    }

    public bool Empty
    {
        get { return NameIndex == 0; }
    }

    public float DefaultValue
    {
        get { return defaultValue; }
        set { defaultValue = value; }
    }

    #region Value Accessors
    public float Value
    {
        get
        {
            if (factorList != null)
            {
                return getValue();
            }
            else
            {
                return defaultValue;
            }
        }
        set
        {
            this.defaultValue = value;
            this.Clear();
        }
    }

    private float getValue()
    {
        float res = defaultValue;
        foreach (FloatCountroler i in factorList)
        {
            if (!(i.IgnoreType || i.Empty))
            {
                res = CalclulateValueWithOperationindex(res, i.Value, i.OperationIndex);
            }
        }
        return res;
    }

    private float CalclulateValueWithOperationindex(float a, float b, ValueCountrolerManager.OprationName operation)
    {
        switch (operation)
        {
            case (ValueCountrolerManager.OprationName.any_ignore):
                return a;
            case (ValueCountrolerManager.OprationName.any_overwrite):
                return b;
            case (ValueCountrolerManager.OprationName.num_add):
                return a + b;
            case (ValueCountrolerManager.OprationName.num_sub):
                return a - b;
            case (ValueCountrolerManager.OprationName.num_mul):
                return a * b;
            case (ValueCountrolerManager.OprationName.num_div):
                return a / b;
            case (ValueCountrolerManager.OprationName.num_pow):
                return UF.Pow(a, b);
            default:
                return a;
        }
    }
    #endregion


    public void Clear()
    {
        factorList = null;
    }

    public void AddFactor(FloatCountroler factor)
    {
        if (!factor.Empty)
        {
            if (factorList == null)
            {
                factorList = new List<FloatCountroler>();
                factorList.Add(factor);
            }
            else
            {
                UF.OrderListInsert(factorList, factor, 
                    (FloatCountroler a, FloatCountroler b) => a.OrderIndex < b.OrderIndex, 
                    (FloatCountroler a, FloatCountroler b) => a.OrderIndex == b.OrderIndex);
            }
        }
    }

    public void AddFactorInCopy(FloatCountroler factor)
    {
        AddFactor(new FloatCountroler(factor));
    }

    #region Find Factor Functions
    private int FineFactorIndexWithNameIndex(byte nameIndex)
    {
        if (factorList == null)
        {
            return -1;
        }
        else
        {
            for (int i = 0; i < factorList.Count; i++)
            {
                if (factorList[i].NameIndex == nameIndex)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    private int FineFactorIndexWithName(string name)
    {
        if (!ValueCountrolerManager.ContainName(name))
        {
            return -1;
        }
        else
        {
            return FineFactorIndexWithNameIndex(ValueCountrolerManager.GetNameIndexByName(name));
        }
    }
    #endregion

    #region Get Factor Functions
    private FloatCountroler GetFactorWithIndex(int index)
    {
        if(index <= -1)
        {
            return null;
        }
        else
        {
            return factorList[index];
        }
    }

    public FloatCountroler FactorWithIndex(int index)
    {
        if(factorList == null || index>= factorList.Count)
        {
            return GetFactorWithIndex(-1);
        }
        else
        {
            return GetFactorWithIndex(index);
        }
    }

    public FloatCountroler FactorWithNameIndex(byte nameIndex)
    {
        return GetFactorWithIndex(FineFactorIndexWithNameIndex(nameIndex));
    }

    public FloatCountroler FactorWithName(string name)
    {
        return GetFactorWithIndex(FineFactorIndexWithName(name));
    }
    #endregion

    #region Delete Factor Functions
    private void DeleteFactorWithIndex(int index)
    {
        if (index != -1)
        {
            factorList.RemoveAt(index);
        }
    }

    public void DelFactorWithIndex(int index)
    {
        if(factorList == null || index >= factorList.Count)
        {
            DeleteFactorWithIndex(-1);
        }
        else
        {
            DeleteFactorWithIndex(index);
        }
    }

    public void DelFectorWithNameIndex(byte index)
    {
        DeleteFactorWithIndex(FineFactorIndexWithNameIndex(index));
    }

    public void DelFactorWithName(string name)
    {
        DeleteFactorWithIndex(FineFactorIndexWithName(name));
    }
    #endregion

    public string ToStringWithDepth(int depth, int tabNum)
    {
        if (depth <= 0)
        {
            return "[(Object) : ValueCountroler ]\n";
        }
        StringBuilder temString = new StringBuilder();
        UF.GenPropertieDescription(temString, "(Object)", "ValueCountrolerStruct", depth, tabNum);
        UF.GenPropertieDescription(temString, "defaultValue", defaultValue, depth, tabNum);
        UF.GenPropertieDescription(temString, "Operation", ValueCountrolerManager.GetOperationNameByOprationIndex(OperationIndex), depth, tabNum);
        if (!(factorList == null))
        {
            UF.GenPropertieDescription(temString, "Value", Value, depth, tabNum);
            UF.GenPropertieDescription(temString, "Expression", GetExpression(), depth, tabNum);
        }
        UF.GenPropertieDescription(temString, "Name", ValueCountrolerManager.GetNameByNameIndex(NameIndex), depth, tabNum);
        UF.GenPropertieDescription(temString, "Order", OrderIndex, depth, tabNum);
        if (!(factorList == null))
        {
            UF.GenPropertieDescription(temString, "factorList", factorList, depth, tabNum);
        }
        return temString.ToString();
    }

    #region Expression
    private string GetExpression()
    {
        if(factorList == null)
        {
            return UF.ConverToString(defaultValue);
        }
        else
        {
            int overwriteIndex = OverwriteIndex();
            if (overwriteIndex == -1)
            {
                return "( " + UF.ConverToString(defaultValue) + ' ' + GetChildenExpression() + " )";
            }
            else
            {
                if(overwriteIndex == factorList.Count - 1)
                {
                    return factorList[overwriteIndex].GetExpression();
                }
                else
                {
                    return "( " + factorList[overwriteIndex].GetExpression() + ' ' + GetChildenExpression(overwriteIndex+1) + " )";
                }
            }
        }
    }

    private int OverwriteIndex()
    {
        int res = -1;
        for (int i =  0; i < factorList.Count; i ++){
            if(factorList[i].OperationIndex == ValueCountrolerManager.OprationName.any_overwrite)
            {
                res = i;
            }
        }
        return res;
    }

    private string GetChildenExpression(int startIndex = 0)
    {
        string res = string.Empty;
        for(int i = startIndex;i<factorList.Count;i++)
        {
            if(factorList[i].OperationIndex == ValueCountrolerManager.OprationName.any_overwrite)
            {
                res = string.Empty;
            }
            res += GetChildExpression(factorList[i]);
            if(i < factorList.Count - 1)
            {
                res += ' ';
            }

        }
        return res;
    }
    private string GetChildExpression(FloatCountroler child)
    {
        switch (child.OperationIndex)
        {
            case (ValueCountrolerManager.OprationName.any_ignore):
                return "ignore" + child.GetExpression();
            case (ValueCountrolerManager.OprationName.any_overwrite):
                return child.GetExpression();
            default:
                return ValueCountrolerManager.GetOperationExpressionMarkByOprationIndex(child.OperationIndex) + " " + child.GetExpression();
        }
    }
#endregion
}
