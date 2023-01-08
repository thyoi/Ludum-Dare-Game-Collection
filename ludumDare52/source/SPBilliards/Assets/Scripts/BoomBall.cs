using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BoomBall : MonoBehaviour
{
    public AnimeCountroler ac;
    public float CountDown;
    public float size;
    public void Start()
    {
        ac.StartAnime();
    }


    public void Update()
    {
        CountDown -= Time.deltaTime;
        ac.Apha.Time = Mathf.Lerp(0.1f, 0.5f, CountDown / 6);
        if(CountDown <= 0)
        {
            KillManager.CRK(transform.position, size,transform.GetComponent<BallCountroler>().MainColor);
            Destroy(gameObject);
        }
    }
}
