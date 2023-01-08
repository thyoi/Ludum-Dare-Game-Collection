using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushKill : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform round;
    public CircleCollider2D col;
    public float Scale = 1;
    public float ForceScale;
    public float CenterRaange = 0;
    private bool Finish;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (round != null)
        {
            transform.position = round.position;
            if (round.transform.GetComponent<AnimeCountroler>().ScaleX.GetProges() > 0.1f)
            {
                if (Finish)
                {
                    Destroy(gameObject);
                    col.radius = 0;
                }
                else
                {
                    col.radius = 2 * round.localScale.x * Scale;
                    Finish = true;
                }
               
                
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D tem = collision.gameObject.transform.GetComponent<Rigidbody2D>();
        if (tem != null&& col.radius>0)
        {
            
            Vector2 dir = (Vector2)tem.transform.position-(Vector2)transform.position;
            float dis =dir.magnitude;
            float r = col.radius;
            if (dis > CenterRaange) {
                tem.AddForce(dir * Mathf.Lerp(0.3f, 1, (r - dis) / r) * ForceScale *3, ForceMode2D.Impulse);

            }
        }
    }
}
