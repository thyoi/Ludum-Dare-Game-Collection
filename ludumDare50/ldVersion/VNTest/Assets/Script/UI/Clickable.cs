using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public InputDataManager idm;
    public bool Active = true;
    public int level = 1;
    public bool lClick = false;
    public bool rClick = false;
    public bool lHold = false;
    public bool rHold = false;
    public Rect area = new Rect(0, 0, 0, 0);
    public Vector2 relaivePosition;
    // Start is called before the first frame update
    public Rect getArea()
    {
        return new Rect(area.x + transform.position.x, area.y + transform.position.y, area.width, area.height);
    }
    void Start()
    {
        idm.registeClick(this);
    }


}
