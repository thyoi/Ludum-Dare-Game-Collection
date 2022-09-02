using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBorad : MonoBehaviour
{
    public Vector2 maxSize;
    public Vector2 minSize;
    public SpriteRenderer sr;
    private bool show = true;
    public bool onShow = false;
    public MargeAnime ma;

    // Start is called before the first frame update
    void Start()
    {
        sr = transform.GetComponent<SpriteRenderer>();    
        ma = transform.GetComponent<MargeAnime>();
    }

    // Update is called once per frame
    void Update()
    {
        if(show != onShow)
        {
            if (onShow)
            {
                UF.setLocalPosition(this.gameObject, new Vector2(0, 0));
                ma.curSize = 0.8f;
            }
            else
            {
                UF.setLocalPosition(this.gameObject, new Vector2(100, 100));
            }
            show = onShow;
        }
    }
}
