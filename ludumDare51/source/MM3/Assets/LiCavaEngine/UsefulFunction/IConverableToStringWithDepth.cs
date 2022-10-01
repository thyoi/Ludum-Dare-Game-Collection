using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public interface IConverableToStringWithDepth
    {
        abstract public string ToStringWithDepth(int depth = 1, int tabNum = 0);

    }
