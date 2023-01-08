using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SansCountroler : MonoBehaviour
{
    public SpriteRenderer Eyes;
    public AnimeCountroler EyesC;
    public SpriteRenderer MySp;
    // Start is called before the first frame update
    public Color SansColor;
    public Sprite SansSprite;
    public bool OnPartical;
    public float ParticalTime;
    


    private float priticalCount;




    public void SansActive()
    {
        UF.SetApha(Eyes, 1);
        particalManager.GlobalManager.CreateShinePartical(new Vector2(0.071f, 0.700f), 2f, SansColor, true, transform, true);
        MySp.sprite = SansSprite;
        OnPartical = true;
        SoundManager.Play("sans1");
    }

    public void SansDeactive()
    {
        EyesC.StartAnime();
        OnPartical = false;


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OnPartical)
        {
            priticalCount += Time.deltaTime;
            if (priticalCount > ParticalTime)
            {
                priticalCount = 0;
                CreatePartical();
            }

        }
    }

    public void CreatePartical()
    {
        particalManager.GlobalManager.BoomParticalBust(1, new Vector2(0.071f, 0.700f), SansColor, 0.5f, true, transform, true);
    }
}
