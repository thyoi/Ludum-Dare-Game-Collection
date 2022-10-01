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
        return UF.PointInRect(MouseManager.MousePosition(), myTransform.position.x + area.x, myTransform.position.y + area.y, area.width, area.height);
    }

    private void Hover()
    {
        if(mouseSprites!=null && mouseSprites.Length == 4)
        {
            MouseManager.SetMouseSprite(mouseSprites);
        }
    }
}
