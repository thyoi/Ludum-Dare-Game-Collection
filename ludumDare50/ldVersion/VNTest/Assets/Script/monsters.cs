using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsters : MonoBehaviour
{
    public static  string action(map m,int n)
    {
        if(n == 0 || n == 1)
        {
            return "";
        }
        else if(n == 2)
        {
            return "Slime bounces around ]and does nothing";
        }
        else
        {
            return "";
        }
    }
}
