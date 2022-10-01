using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetAbleItem<T>
{
    public T Value
    {
        get;
        set;
    }
}
