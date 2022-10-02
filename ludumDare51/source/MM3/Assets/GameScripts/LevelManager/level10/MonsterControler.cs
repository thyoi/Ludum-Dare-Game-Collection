using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControler : MonoBehaviour
{
    public ScaleAnimation2D bord;
    public FollowPositionAnimation2D positionAnimation;
    public Boomable boom;
    public SpriteRenderer spriteRenderer;
    public int type;
    public int color;

    public void Start()
    {
        bord.CurSize = 0;
        bord.EndSize = 0;
        positionAnimation.EndPosition = transform.position;
    }

    public void SetInitEnd(Vector2 init, Vector2 end)
    {
        positionAnimation.curPosition = init;
        positionAnimation.endPosition = end;
    }
    public void Onbord()
    {
        bord.EndSize = 1;
    }

    public void OffBord()
    {
        bord.EndSize = 0;
    }
    public void StartBoom()
    {
        boom.StartBoom();
    }

    public void SetPosition(Vector2 p)
    {
        positionAnimation.EndPosition = p;
    }
    public void SetCur(Vector2 p)
    {
        positionAnimation.CurPosition = p;
    }

    public void SetColor0(Color c)
    {
        spriteRenderer.color = c;
        color = 0;
    }

    public void SetColor(Color c)
    {
        spriteRenderer.color = c;
    }
    public Color GetClolr()
    {
        return spriteRenderer.color;
    }

}
