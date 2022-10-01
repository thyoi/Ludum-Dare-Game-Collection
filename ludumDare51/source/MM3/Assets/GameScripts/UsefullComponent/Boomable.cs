using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomable : MonoBehaviour
{
    public int timeLayer = 3;
    public float totalTime = 3;
    public float markDelay = 0.05f;
    public float delay;
    public float size = 1;
    public AnimationCurve mainScale;
    public float fadeTime = 1;
    public float ScaleTime = 1;
    public int particalNum = 10;

    public GameObject boomRound;
    public GameObject boomMark;
    public GameObject[] boomParticalList;

    private float dt;
    private bool onBoom;
    private ScaleWithCurveAnimation2D scaleAnimation;
    private FadeInAnimation fadeAnimation;
    private SpriteRenderer spriteRenderer;


    public void StartBoom()
    {
        onBoom = true;
        scaleAnimation.curve = mainScale;
        scaleAnimation.initSize = 1;
        scaleAnimation.EndSize = 1.2f;
        scaleAnimation.animationTime = ScaleTime;
        fadeAnimation.outMode = true;
        fadeAnimation.animationTime = fadeTime;
        creatBoomRound();
        if (boomParticalList.Length == 3)
        {
            CreateParticals(particalNum);
        }
        transform.parent = null;

        fadeAnimation.StartAnimation();
        scaleAnimation.StartAnimation();
    }

    private void Awake()
    {
        scaleAnimation = transform.parent.GetComponent<ScaleWithCurveAnimation2D>();
        fadeAnimation = transform.parent.GetComponent<FadeInAnimation>();
        spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        dt = TimeManager.DeltaTime(timeLayer);
        if(onBoom && dt > 0)
        {
            if (delay > 0)
            {
                delay -= dt;
            }
            else
            {
                totalTime -= dt;
                if(boomMark != null)
                {
                    UpdateMark();
                }
                if(totalTime <= 0)
                {
                    Destroy(transform.gameObject);
                }
            }
        }
    }

    private void UpdateMark()
    {
        if (markDelay > 0)
        {
            markDelay -= dt;
            if (markDelay <= 0)
            {
                GameObject tem = Instantiate(boomMark, transform);
                tem.transform.GetComponent<FadeInAnimation>().StartAnimation();
                tem.transform.GetComponent<SpriteRenderer>().color = spriteRenderer.color;
            }
        }
    }

    private void creatBoomRound()
    {
        if(boomRound != null)
        {
            GameObject tem = Instantiate(boomRound, transform);
            tem.transform.GetComponent<BoomRound>().StartBoom(size);
            tem.transform.GetComponent<SpriteRenderer>().color = spriteRenderer.color;
        }
    }

    private void CreateParticl(Vector2 endPosition, int index)
    {
        GameObject tem = Instantiate(boomParticalList[index], transform);
        tem.transform.GetComponent<FadeInAnimation>().StartAnimation();
        tem.transform.GetComponent<PositionInPathWithCurveAnimation2D>().EndPosition = endPosition;
        tem.transform.GetComponent<PositionInPathWithCurveAnimation2D>().StartAnimation();
        tem.transform.GetComponent<SpriteRenderer>().color = spriteRenderer.color;
    }

    private void CreateParticals(int num)
    {
        for(int i = 0; i < num; i++)
        {
            float angel = Random.Range(0, 2*Mathf.PI);
            float dis = size * (1 + Random.Range(-0.3f, 0.3f));
            float index = Random.Range(0, 3);
            int ind;
            if (index < 1)
            {
                ind = 0;
            }
            else if (index < 2)
            {
                ind = 1;
            }
            else
            {
                ind = 2;
            }
            CreateParticl(new Vector2(Mathf.Sin(angel) * dis, Mathf.Cos(angel) * dis), ind);
        }
    }

}
