using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public InputDataManager idm;
    public Sprite[] spriteList;
    public GameObject mouse;
    // Update is called once per frame
    void Update()
    {
        UF.setPosition(mouse, idm.mouseInput.position);
    }
}
