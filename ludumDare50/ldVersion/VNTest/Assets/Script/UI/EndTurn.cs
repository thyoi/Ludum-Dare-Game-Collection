using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    public bool active;
    public Hoverable ha;
    public Clickable ca;
    public HoverBorad hoverB;
    public bool click;

    public bool getClick()
    {
        return click && active;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ha.Active = active;
        hoverB.onShow = ha.Hover && active;
        click = ca.lClick;
    }
}
