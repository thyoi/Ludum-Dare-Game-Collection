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
            col.radius = 3* round.localScale.x;
            if (round.transform.GetComponent<AnimeCountroler>().ScaleX.GetProges() > 0.5f)
            {
                Destroy(gameObject);
            }
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
