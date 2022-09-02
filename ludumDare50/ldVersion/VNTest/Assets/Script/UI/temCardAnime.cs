using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temCardAnime : MonoBehaviour
{

    public float speed = 15;
    public float MaxSize = 1.5f;
    public float CurentSize = 1;
    public bool startAnime = false;
    public SpriteRenderer sr;
    public float curA = 1;
    public float speedA = 5;
    public float delay = 0;
    void Start()
    {
        sr = transform.GetComponent<SpriteRenderer>();
    }

    public void delayDestory(float t)
    {
        delay = t;
        curA = 0;
        CurentSize = MaxSize;
        startAnime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startAnime)
        {
            if (delay <= 0)
            {
                CurentSize = Mathf.Lerp(CurentSize, MaxSize, speed * Time.deltaTime);
                curA = Mathf.Lerp(curA, 0, speedA * Time.deltaTime);
                if (CurentSize == MaxSize && curA == 0)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    transform.localScale = new Vector3(CurentSize, CurentSize, 1);
                    sr.color = new Color(1, 1, 1, curA);
                }
            }
            else
            {
                delay -= Time.deltaTime;
            }
        }
    }
}
