using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackEffect : MonoBehaviour
{
    public static BackEffect GlobleManager;

    public static void BoomAt(Vector2 v, int type, int type2 = 0)
    {
        if(type == 0)
        {
            GlobleManager.b1.CreateBoom(v, type2);
        }
        else if(type == 1)
        {
            GlobleManager.b2.CreateBoom(v, type2);
        }
        else
        {
            GlobleManager.b3.CreateBoom(v, type2);
        }
    }

    public static Transform BackGroundTransform()
    {
        return GlobleManager.b1.transform;
    }
    public BackEffect()
    {
        GlobleManager = this;
    }



    public BackGroundSystem b1;
    public BackGroundSystem b2;
    public BackGroundSystem b3;



    // Start is called before the first frame update
    void Start()
    {
        b1.Show1();
        //b1.CreateBoom(new Vector2Int(10,10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
