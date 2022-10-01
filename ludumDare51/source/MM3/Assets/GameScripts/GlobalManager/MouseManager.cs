using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    static public MouseManager mainManager;
    static public Vector2 MousePosition()
    {
        return mainManager.GetMousePosition();
    }
    static public void SetMouseSprite(Sprite[] s)
    {
        mainManager.mouseAnimation.SetSprites(s);
    }

    public delegate void MouseCallBack(int[] data);
    public Camera mainCamera;
    public Sprite[] defaultMouseSprites;
    public Transform mouseTransform;
    public FrameAnimation mouseAnimation;


    private List<MouseArea>[] mouseAreaList;

    public Vector2 GetMousePosition()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    void Awake()
    {
        mainManager = this;
    }

    void Update()
    {
        UF.SetPosition(mouseTransform, GetMousePosition());
        mouseAnimation.SetSprites(defaultMouseSprites);
    }
}
