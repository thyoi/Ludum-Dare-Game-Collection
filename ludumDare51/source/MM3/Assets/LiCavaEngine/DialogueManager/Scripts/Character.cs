using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IPoolableGameObject
{
    public FrameAnimation frameAnimation;
    public PositionInPathWithCurveAnimation2D positionAnimation;
    public ScaleWithCurveAnimation2D scaleAnimation;
    public FadeInAnimation fadeAnimation;
    public Transform myTransform;
    private ObjectsPool<Character> myPool;



    public void SetPool(ObjectsPool<Character> pool)
    {
        myPool = pool;
    }

    public void ReturnToPool()
    {
        myPool.returnItem(this);
    }

    public void FadeAndReturnToPool(float t)
    {
        fadeAnimation.outMode = true;
        fadeAnimation.animationTime = t;
        fadeAnimation.StartAnimation(this.ReturnToPool);
    }

    public void InPool()
    {
        FrameAnimeationManager.Delete(frameAnimation);
        frameAnimation.SetNull();
    }

    public void OutPool()
    {
        FrameAnimeationManager.Register(frameAnimation);
    }

    public void SetSprites(Sprite[] s)
    {
        frameAnimation.spriteList = s;
    }

    public float Length
    {
        get
        {
            return frameAnimation.GetSprite(1).rect.width*UF.PixelLength*transform.localScale.x;
        }
    }

    public void SetPosition(Vector2 position)
    {
        Vector3 tem = myTransform.localPosition;
        tem.x = position.x;
        tem.y = position.y;
        myTransform.localPosition = tem;
    }
}
