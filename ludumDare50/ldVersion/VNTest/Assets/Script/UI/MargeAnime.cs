using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MargeAnime : MonoBehaviour
{
    public float maxSize = 1;
    public float curSize = 1;
    public float speed = 10;
    public bool Title = false;
    public Vector2 initTitleSize;

    // Update is called once per frame
    void Update()
    {
        if(curSize != maxSize)
        {
            curSize = Mathf.Lerp(curSize, maxSize, speed * Time.deltaTime);
            if (!Title)
            {
                transform.localScale = new Vector3(curSize, curSize, 1);
            }
            else
            {
                transform.GetComponent<SpriteRenderer>().size = initTitleSize * curSize;
            }
        }
    }
}
