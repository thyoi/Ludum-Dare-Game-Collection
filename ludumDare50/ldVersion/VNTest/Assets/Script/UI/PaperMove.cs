using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperMove : MonoBehaviour
{
    public Vector2 startPosition;
    public Vector2 endPosition;
    public Vector2 curPosition;
    public float Speed = 6;
    public bool move;
    // Start is called before the first frame update
    void Start()
    {
        curPosition = startPosition;
    }
    public void MoveTo(Vector2 to)
    {
        endPosition = to;
        move = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (move )
        {
            curPosition = Vector2.Lerp(curPosition, endPosition, Speed * Time.deltaTime);
            if (UF.isCloseEnough(curPosition, endPosition))
            {
                curPosition = endPosition;
                move = false;
            }
            UF.setPosition(this.gameObject, curPosition);
        }
    }
}
