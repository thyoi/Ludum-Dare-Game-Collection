using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public bool Active;
    public float Demage;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (Active)
        {

            BallCountroler tem = collision.gameObject.transform.GetComponent<BallCountroler>();
            if (tem.MainBall)
            {
                if (HPManager.HPD(Demage))
                {
                    tem.Killed();
                }
            }
            else
            {
                tem.Killed();
            }
        }
    }
}
