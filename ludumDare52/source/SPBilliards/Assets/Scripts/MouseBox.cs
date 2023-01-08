using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBox : MonoBehaviour
{

    [HideInInspector]
    public UF.MouseReact HoverFunction = (Vector2 position)=> { return false; };
    [HideInInspector]
    public UF.MouseReact ClickFunction = (Vector2 position)=> { return false; };
    [HideInInspector]
    public UF.MouseReact RightClickFunction = (Vector2 position) => { return false; };
    [HideInInspector]
    public UF.MouseReact UpFunction = (Vector2 position) => { return false; };
    public Sprite[] MouseSprites = null;
    public BoxCollider2D Area;
    public Transform MyTransform;
    public bool active = true;
    private Rect myRect;

    public void Start()
    {
        myRect = new Rect(-Area.size.x / 2, -Area.size.y / 2, Area.size.x, Area.size.y);
    }


    public bool MouseInArea(Vector2 MousePosition)
    {
        return UF.PointInRect(MousePosition, new Rect(myRect.x + MyTransform.position.x, myRect.y + MyTransform.position.y, myRect.width,myRect.height));
    }

}
