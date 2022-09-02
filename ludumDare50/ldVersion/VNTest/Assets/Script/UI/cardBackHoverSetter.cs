using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardBackHoverSetter : MonoBehaviour
{
    public Vector2 size;
    public HoverBorad hb;
    public Hoverable ha;
    public Clickable ca;
    public cardShow cs;
    public bool active = false;
    public bool Act = true;
    void Start()
    {
        if (ha != null)
        {
            ha.area = new Rect( - size.x / 2, - size.y / 2, size.x, size.y);
        }
        ca.area = new Rect( - size.x / 2,  - size.y / 2, size.x, size.y);
    }

    // Update is called once per frame
    void Update()
    {
        active = cs.onShow&&Act;
        if (hb != null)
        {
            hb.onShow = ha.Hover && active;
            ha.Active = active;

        }
        ca.Active = active;
    }
}
