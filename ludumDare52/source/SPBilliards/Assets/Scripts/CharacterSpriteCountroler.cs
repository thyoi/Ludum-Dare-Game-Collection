using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterSpriteCountroler : MonoBehaviour
{
    public SpriteRenderer Body;
    public AnimeCountroler MyAnimeCountroler;
    public Sprite[] BodySprites;
    public AnimeCountrolerData TouchAnime;
    public AnimeCountrolerData TouchRecoverAnime;
    public AnimeCountrolerData StayAnime;
    public UF.AnimeCallback touchCallback;
    public SpriteRenderer[] Ballsborad;
    public SpriteRenderer FirstBallBack;
    public Transform BallsTransform;




    public float TouchTime;

    public Color[] cList;
    public Sprite[] sList;


    private bool onTouch;
    private float touchCount;
    private float interTime;
    private float closeTime;
    private float count;
    private bool eyeOpen;

    


    private void SetBodySprite(int n)
    {
        Body.sprite = BodySprites[n];
    }

    public Sprite GetBallSprite(int i)
    {
        if (i < sList.Length)
        {
            return sList[i];
        }
        else
        {
            return sList[0];
        }
    }

    public Color GetBallColor(int i)
    {
        if (i < cList.Length)
        {
            return cList[i];
        }
        else
        {
            return cList[0];
        }
    }
    public void ShowBalls(int[] bs)
    {
        BallsTransform.localPosition = Vector2.zero;
        FirstBallBack.sprite = GetBallSprite(bs[0]);
        FirstBallBack.color = GetBallColor(bs[0]);
        for (int i = 0; i < 4; i++)
        {
            Ballsborad[i].color = GetBallColor(bs[0]);
        }
    }

    public void HideBalls()
    {
        BallsTransform.localPosition = new Vector2(100,100);

    }



        private void UpdateTouch()
    {
        touchCount += TimeManager.DT();
        if (touchCount >= TouchTime)
        {
            onTouch = false;
            SetBodySprite(0);
            count = 0;
            eyeOpen = true;
        }
    }

    public void Touch()
    {
        touchCount = 0;
        onTouch = true;
        SetBodySprite(1);
        MyAnimeCountroler.StartAnime(TouchAnime) ;
        SoundManager.Play("touch");
        if(touchCallback != null)
        {
            touchCallback();
            touchCallback = null;
        }
        
    }

    public void ChangeSprite(Sprite s)
    {
        touchCount = 0;
        onTouch = true;
        BodySprites[0] = s;
        BodySprites[1] = s;
        SetBodySprite(1);
        MyAnimeCountroler.StartAnime(TouchAnime);
        SoundManager.Play("touch");
        HideBalls();

    }

    // Start is called before the first frame update
    void Start()
    {
        eyeOpen = true;
        count = 0;
        onTouch = false;
        touchCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (onTouch)
        {
            UpdateTouch();
        }

    }
}
