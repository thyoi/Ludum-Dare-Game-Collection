using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opAreaMove : MonoBehaviour
{

    public Vector2 initSize;
    public float moveSpeed;
    public float scaleSpeed;
    public AnimeArrVector2 position;
    public AnimeArrVector2 size;
    public GameObject mask;
    public float dt;











    // Start is called before the first frame update
    void Start()
    {
        UF.setPosition(this.gameObject, new Vector2(100,100));
        position = new AnimeArrVector2();
        size = new AnimeArrVector2();
        position.speed = moveSpeed;
        size.speed = scaleSpeed;
    }

        // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        position.update(dt);
        size.update(dt);
        if (true)
        {
            UF.setPosition(this.gameObject,position.cur);
        }
        if (true)
        {
            UF.setSize(this.gameObject, size.cur);
            mask.transform.localScale = new Vector3(size.cur.x - 0.1f, size.cur.y - 0.1f, 1);
            if(size.cur.x == 0)
            {
                //transform.localScale = new Vector3(0,0,0);
            }
            else
            {
                //transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void setSize(Vector2 v)
    {
        UF.setSize(this.gameObject, v);
    }

    public Vector2 getCurSize()
    {
        return size.cur;
    }

    public void startMove(Vector2 s,Vector2 e,float delay = 0)
    {
        position.cur = s;
        position.end = e;
        position.start = true;
        position.delay = delay;
    }

    public void startScale(Vector2 s, Vector2 e, float delay = 0)
    {
        if(s == null)
        {
            s = initSize;
        }
        size.cur = s;
        size.end = e;
        size.start = true;
        size.delay = delay;
    }
}
