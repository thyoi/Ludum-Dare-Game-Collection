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




    public float TouchTime;


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
