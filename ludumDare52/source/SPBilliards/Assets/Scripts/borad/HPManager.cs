using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform mask1;
    public Transform mask2;
    public float hp;


    public float hpState1;
    public float hpState2;
    public Color c1;
    public Color c2;
    public float recoverSpeed;

    public Vector2 Y1Range;
    public Vector2 Y2Range;
    public Vector2 MaskBase1;
    public Vector2 MaskBase2;
    public Vector2 Base;
    public Vector2 Base1;
    public Vector2 Final;
    public Vector2 Final1;
    public UF.AnimeCallback hp1Callback;
    public UF.AnimeCallback hp2Callback;



    public float ParticalTime;

    public float particalCount;
    public int hpState = 3;

    public bool Partical;

    public void StartPartical()
    {
        Partical = true;
    }

    public void CreatePartical()
    {
        switch (hpState)
        {
            case 1:particalManager.GlobalManager.BoomParticalBust(1, Mathf.Lerp(Y1Range.x, Y1Range.y, hp / hpState1)
                * Vector2.up + Base1, c1,0.3f, true, null, true);break;
            case 2:
                particalManager.GlobalManager.BoomParticalBust(1, Mathf.Lerp(Y2Range.x, Y2Range.y, (hp-hpState1) / (hpState2-hpState1))
                * Vector2.up + Base, c2,0.3f, true, null, true); break;
            case 3:
                particalManager.GlobalManager.BoomParticalBust(1,  Final, c2,0.5f, true,null,true); break;
        }
    }

    public void UpdatePartical()
    {
        if (Partical)
        {
            particalCount += Time.deltaTime;
            if (ParticalTime <= particalCount)
            {
                particalCount = 0;
                CreatePartical();

            }
        }
    }

    public void RecoverHP()
    {
        hp += recoverSpeed * Time.deltaTime;
        if (hp > hpState2)
        {
            hp = hpState2;
        }

    }

    public void UpdateHP()
    {
        if (hp < hpState1)
        {
            hpState = 1;
            mask1.localPosition = MaskBase1 + Vector2.up * (Mathf.Lerp(Y1Range.x, Y1Range.y, hp / hpState1));
            mask2.localPosition = MaskBase2;
        }
        else if (hp < hpState2)
        {
            
            if (hpState == 1)
            {
                particalManager.GlobalManager.CreateShinePartical(Final1, 0.6f, c1, true);
                if (  hp1Callback != null)
                {
                hp1Callback();
                }
            }
            hpState = 2;
            mask1.localPosition = MaskBase1 + Vector2.up * Y1Range.y;
            mask2.localPosition = MaskBase2 + Vector2.up * (Mathf.Lerp(Y2Range.x, Y2Range.y, (hp - hpState1) / (hpState2 - hpState1)));
        }
        else
        {
            if (hpState != 3)
            {
                particalManager.GlobalManager.CreateShinePartical(Final, 1.3f, c2, true);
                if(hp2Callback != null)
                {
                    hp2Callback();
                }
            }
            hpState = 3;
            mask1.localPosition = MaskBase1 + Vector2.up * Y1Range.y;
            mask2.localPosition = MaskBase2 + Vector2.up * Y2Range.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePartical();
        UpdateHP();
        RecoverHP();

    }

    public void HPDown(float n)
    {
        hp -= n;
        if (hp < 0)
        {
            hp = 0;
        }
        UpdateHP();
    }
}
