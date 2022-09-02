using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverable : MonoBehaviour
{
    public InputDataManager idm;
    public bool Active = true;
    public int level = 1;
    public bool Hover = false;
    public Rect area = new Rect(0,0,0,0);
    public Vector2 relaivePosition;


    public Rect getArea()
    {
        return new Rect(area.x + transform.position.x, area.y + transform.position.y, area.width, area.height);
    }


    void Start()
    {
        idm.registeHover(this);
    }


}
