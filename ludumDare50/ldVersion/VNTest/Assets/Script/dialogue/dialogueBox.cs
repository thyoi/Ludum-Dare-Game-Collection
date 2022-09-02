using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueBox : MonoBehaviour
{
    public SpriteRenderer sr;
    public float state;
    public Vector2 minSize;
    public Vector2 maxSize;
    public Vector2 center;
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        state = -1;
        updateSize();
    }


    public void updateSize()
    {
        if(state < 0)
        {
            UF.setPosition(this.gameObject,new Vector2(-100, -100));
        }
        else
        {
            UF.setPosition(this.gameObject, center);
            Vector2 tem = minSize + state * (maxSize - minSize);
            sr.size = tem;
        }
    }



    public void setState(float s)
    {
        state = s;
        updateSize();

    }
}
