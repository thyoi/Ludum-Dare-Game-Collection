using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameAnimation : MonoBehaviour
{
    public Sprite[] spriteList;
    public float interTime = 0.2f;
    [HideInInspector]
    public int id;
    private int frameCount;

    private SpriteRenderer spriteRenderer;

    public void Awake()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        FrameAnimeationManager.Register(this);
    }

    public void SetSprites(Sprite[] s)
    {
        spriteList = s;
        frameCount = 0;
        SetSprite(spriteList[frameCount]);
    }
    public void NextFrame()
    {
        frameCount++;
        if (frameCount >= spriteList.Length)
        {
            frameCount = 0;
        }
        SetSprite(spriteList[frameCount]);
    }

    private void SetSprite(Sprite s)
    {
        spriteRenderer.sprite = s;
    }

    public void OnDestroy()
    {
        FrameAnimeationManager.Delete(this);
    }

    public void SetNull()
    {
        spriteRenderer.sprite = null;
    }

    public Sprite GetSprite(int index)
    {
        return spriteList[index];
    }

}
