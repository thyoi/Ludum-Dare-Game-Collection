using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColChecker : MonoBehaviour
{
    public LayerMask ColLayer;
    public Collider2D col;
    public AnimationCallBack callBack;
    public bool active;
    public bool once;
    public Boomable boom;

    public void Active()
    {
        active = false;
    }

    public void Update()
    {
        if (active)
        {
            if (col.IsTouchingLayers(ColLayer))
            {
                if(boom != null)
                {
                    boom.StartBoom();
                }
                if (callBack != null) { callBack(); }
                
                if (once)
                {
                    active = false;
                }
            }
        }
    }

}
