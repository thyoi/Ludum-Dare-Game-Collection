using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    public bool show;
    public float af = 0;
    public float showTime;
    public float count;
    public SpriteRenderer sr;
    public float delay;
    public SpriteRenderer bsr;
    public bool backGroung = false;
    void Start()
    {
        sr = transform.GetComponent<SpriteRenderer>();
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (show && af<1)
        {
            if (delay <= 0)
            {
                count += Time.deltaTime;
                af = count / showTime;
                if (af > 1)
                {
                    af = 1;
                }
                sr.color = new Color(1, 1, 1, af);
                if (backGroung)
                {
                    UF.setColorA(bsr, af);
                }
            }
            else
            {
                delay -= Time.deltaTime;
            }
        }
    }

    public void setColor(Color c)
    {
        UF.setColor(bsr, c);
    }
}
