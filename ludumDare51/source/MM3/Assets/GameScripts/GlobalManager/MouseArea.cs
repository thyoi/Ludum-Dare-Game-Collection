using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseArea : MonoBehaviour
{
    public MouseManager.MouseCallBack ClickCallBack;
    public Sprite[] mouseSprites;
    public Rect area;
    public int[] data;
    public bool active;

    private Transform myTransform;

    public void Awake()
    {
        myTransform = transform;
    }


    public void Update()
    {
        if (active)
        {
            if (MouseInArea())
            {
                Hover();
                if (Input.GetMouseButtonDown(0) && ClickCallBack != null)
                {
                    ClickCallBack(data);
                }
            }

        }
    }

    private bool MouseInArea()
    {
        Vector3 tem = new Vector3(1,1,1);
        if (transform.parent != null)
        {
            tem = transform.parent.localScale;
        }
        return UF.PointInRect(MouseManager.MousePosition(), myTransform.position.x + area.x*tem.x, myTransform.position.y + area.y*tem.y, area.width*tem.x, area.height * tem.y);
    }

    private void Hover()
    {
        if(mouseSprites!=null && mouseSprites.Length == 4)
        {
            MouseManager.SetMouseSprite(mouseSprites);
        }
    }
    public void Active()
    {
        active = true;
    }

}
