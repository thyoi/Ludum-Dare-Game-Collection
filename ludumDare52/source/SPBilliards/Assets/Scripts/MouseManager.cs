using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public static MouseManager GlobleManager;
    public static Vector2 MousePosition()
    {
        return GlobleManager.curMousePosition;
    }


    public Camera mainCamera;
    public Sprite[] MouseSprites;
    public Transform MouseItem;
    public SpriteRenderer MouseSpriteRender;

    public MouseBox[] MouseBoxs;

    private Vector2 curMousePosition;
    private Sprite[] curMouseSprites;

    public MouseManager()
    {
        GlobleManager = this;
    }


    private void SetMouseSprite(int i)
    {
        MouseSpriteRender.sprite = curMouseSprites[i];
    }

    private Vector2 GetMousePosition()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    
    private bool CheckHover()
    {
        for(int i = 0; i < MouseBoxs.Length; i++)
        {
            if (MouseBoxs[i].active && MouseBoxs[i].MouseInArea(curMousePosition))
            {
                if (MouseBoxs[i].HoverFunction(curMousePosition))
                {
                    if (MouseBoxs[i].MouseSprites != null)
                    {
                        curMouseSprites = MouseBoxs[i].MouseSprites;
                    }
                    return true ;
                }
            }
        }
        return false;
    }

    private void CheckClick()
    {
        for (int i = 0; i < MouseBoxs.Length; i++)
        {
            if(MouseBoxs[i].active && MouseBoxs[i].MouseInArea(curMousePosition))
            {
                if (MouseBoxs[i].ClickFunction(curMousePosition))
                {
                    return;
                }
            }
        }
    }

    private void CheckUp()
    {
        for (int i = 0; i < MouseBoxs.Length; i++)
        {
            if (MouseBoxs[i].active && MouseBoxs[i].MouseInArea(curMousePosition))
            {
                if (MouseBoxs[i].UpFunction(curMousePosition))
                {
                    return;
                }
            }
        }
    }

    private void CheckRightClick()
    {
        for (int i = 0; i < MouseBoxs.Length; i++)
        {
            if (MouseBoxs[i].active && MouseBoxs[i].MouseInArea(curMousePosition))
            {
                if (MouseBoxs[i].RightClickFunction(curMousePosition))
                {
                    return;
                }
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        curMousePosition = GetMousePosition();
        curMouseSprites = MouseSprites;
    }

    // Update is called once per frame
    void Update()
    {
        curMousePosition = GetMousePosition();
        MouseItem.position = curMousePosition;
        curMouseSprites = MouseSprites;
        bool hoverFlag = CheckHover();
        if (Input.GetMouseButton(0))
        {
            SetMouseSprite(2);
        }
        else if (hoverFlag)
        {
            SetMouseSprite(1);
        }
        else
        {
            SetMouseSprite(0);
        }
        if (Input.GetMouseButtonDown(0))
        {
            CheckClick();
        }
        if (Input.GetMouseButtonDown(1))
        {
            CheckRightClick();
        }
        if (Input.GetMouseButtonUp(0))
        {
            CheckUp();
        }
        {

        }


    }
}
