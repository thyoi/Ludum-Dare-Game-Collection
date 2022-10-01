using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ValueDataHeadStrust
{
    public byte nameIndex;
    public sbyte orderIndex;
    public ValueCountrolerManager.OprationName operationIndex;
    public byte id;

    public ValueDataHeadStrust(byte name,  sbyte order, ValueCountrolerManager.OprationName operation,byte id = 0)
    {
        nameIndex = name;
        orderIndex = order;
        operationIndex = operation;
        this.id = id;
    }
}
