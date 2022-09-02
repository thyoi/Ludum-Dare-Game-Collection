using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputSetter
{
    public static void setMouseData(mouseData md)
    {
        md.position = Input.mousePosition * new Vector2(16.0f/Screen.width,9.0f/Screen.height) - new Vector2(8,4.5f);
        md.leftKey.hold = Input.GetMouseButton(0);
        md.leftKey.down = Input.GetMouseButtonDown(0);
        md.leftKey.up = Input.GetMouseButtonUp(0);

        md.rightKey.hold = Input.GetMouseButton(1);
        md.rightKey.down = Input.GetMouseButtonDown(1);
        md.rightKey.up = Input.GetMouseButtonUp(1);

        md.middleKey.hold = Input.GetMouseButton(2);
        md.middleKey.down = Input.GetMouseButtonDown(2);
        md.middleKey.up = Input.GetMouseButtonUp(2);
    }

    public static void setKeysData(keyData[] kd)
    {
        for(int i = 0; i < kd.Length; i++)
        {
            checkKey(kd[i]);
        }
    }

    public static void checkKey(keyData kd)
    {
        KeyCode tem = getKeyCode(kd.keyName);
        if(tem != KeyCode.None)
        {
            kd.hold = Input.GetKey(tem);
            kd.down = Input.GetKeyDown(tem);
            kd.up = Input.GetKeyUp(tem);
        }
        
    }
    public static KeyCode getKeyCode(string s)
    {
        switch (s)
        {
            case ("w"):return KeyCode.W;
            case ("a"): return KeyCode.A;
            case ("s"): return KeyCode.S;
            case ("d"): return KeyCode.D;
            case ("space"): return KeyCode.Space;
            case ("lShift"): return KeyCode.LeftShift;
            case ("rShift"): return KeyCode.RightShift;
            case ("enter"): return KeyCode.Return;
            default:return KeyCode.None;
        }
    }

}
