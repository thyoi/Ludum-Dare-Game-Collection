using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundKill1 : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform round;
    public CircleCollider2D col;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (round != null)
        {
            transform.position = round.position;
            col.radius = 4* round.localScale.x;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallCountroler tem = collision.gameObject.transform.GetComponent<BallCountroler>();
        if(tem != null)
        {
            tem.Killed();
        }
    }
}
