using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpSetter : MonoBehaviour
{
    public GameObject mask1;
    public GameObject mask2;
    public int MaxHp;
    public int hp;
    public float Maxdx;


    public void sethp(int n)
    {
        hp = n;
        if (hp > MaxHp)
        {
            setDx(0);
            return;
        }
        else
        {
            setDx((1-hp*1.0f/MaxHp)*-1*Maxdx);
        }
    }

    public void setDx(float dx)
    {
        UF.setLocalPosition(mask1, new Vector2(dx, 0));
        UF.setLocalPosition(mask2, new Vector2(dx+0.1f, 0));
    }

    void Update()
    {
        sethp(hp);
    }
}
