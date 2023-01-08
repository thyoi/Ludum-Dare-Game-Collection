using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SansSkill1 : MonoBehaviour
{
    public float SkillTime;
    public float CallbackTime;
    public Vector2 initPosition;
    public Vector2 EndPosition;
    public bool OnSkill;
    public AnimationCurve curve;
    public UF.AnimeCallback Callback;
    public SpriteRenderer MainBallSprite;
    public SpriteRenderer MainBallSprite2;
    public Rigidbody2D MainBallR;
    public AnimeCountroler[] Items;

    public Color SansColor;
    public float ParticalTime;
    public AnimeCountroler Bones1;
    public AnimeCountroler Bones2;
    
    //private float CallBackCount;

    private float count;
    private float ParticalCount;


    public void CleanBone1()
    {
        Bones1.active = false;
        Bones1.transform.position = new Vector2(0, -1.2f);
        Bones2.active = false;
        Bones2.transform.position = new Vector2(-100, -0.586f);
    }
    public void Bones1_1()
    {
        Bones1.positionY.Init = -1.2f;
        Bones1.positionY.End = 0f;
        Bones1.StartAnime();
        SoundManager.Play("sansC3");
    }

    public void Bones1_2()
    {
        Bones1.positionY.Init = 0f;
        Bones1.positionY.End = -1.2f;
        Bones1.StartAnime();
    }


    public void Bones2_1()
    {
        Bones2.StartAnime();
    }



    public void StartSkill(UF.AnimeCallback cb = null)
    {
        OnSkill = true;
        count = 0;
        Callback = cb;
        MainBallSprite.color = SansColor;
        MainBallSprite2.color = SansColor;
        MainBallR.bodyType = RigidbodyType2D.Kinematic;
        particalManager.GlobalManager.CreateRoundPartical(initPosition, 1f, SansColor, true);
        foreach(AnimeCountroler a in Items)
        {
            a.Apha.Delay = 1.3f;
            a.StartAnime();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OnSkill)
        {
            count += Time.deltaTime;
            if (count > CallbackTime)
            {
                if (Callback != null)
                {
                    Callback();
                    Callback = null;
                    SoundManager.Play("sansC2");
                    particalManager.GlobalManager.CreateRoundPartical(MainBallR.transform.position, 3, SansColor, true);
                    particalManager.GlobalManager.BoomParticalBust(30, MainBallR.transform.position, SansColor, 3, true);
                }
            }
            if (count > SkillTime)
            {
                count = SkillTime;
                OnSkill = false;
               
                MainBallSprite.color = Color.white;
                MainBallSprite2.color = Color.white;
                MainBallR.bodyType = RigidbodyType2D.Dynamic;
            }

            MainBallR.transform.position = Vector2.Lerp(initPosition, EndPosition, curve.Evaluate(count / SkillTime));



            ParticalCount += Time.deltaTime;
            if (ParticalCount > ParticalTime)
            {
                ParticalCount = 0;
                CreatePartical();
            }

        }
    }
    public void CreatePartical()
    {
        particalManager.GlobalManager.BoomParticalBust(1, MainBallSprite.transform.position, SansColor, 0.8f, true);
    }
}
