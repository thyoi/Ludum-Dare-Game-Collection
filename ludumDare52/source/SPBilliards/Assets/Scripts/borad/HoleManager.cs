using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    public static HoleManager GlobalManager;
    public static Vector2 CheckHole(Vector2 p)
    {
        return GlobalManager.CheckInHole(p);
    }
    public HoleManager()
    {
        HoleManager.GlobalManager = this;
    }


    public CircleCollider2D[] holes;

    public Vector2 CheckInHole(Vector2 p)
    {
        for(int i = 0; i < holes.Length; i++)
        {
            if (UF.PointInRound(holes[i], p))
            {
                return holes[i].transform.position;
            }
        }
        return Vector2.zero;
    }


}
