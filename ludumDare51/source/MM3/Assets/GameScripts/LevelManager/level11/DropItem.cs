using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public float rspeed;

    private float rcount;





    public void Update()
    {

        rcount += rspeed * Time.deltaTime;
        transform.localRotation = Quaternion.AngleAxis(rcount, new Vector3(0, 0, 1));
    }
}
