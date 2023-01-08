using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShinePartical : MonoBehaviour
{
    public SpriteRenderer shine;
    public AnimeCountroler shineA;
    public RoundPartical roundPartical;


    public void SetTimeScaleRecive(bool b)
    {
        shineA.SetTimeScaleRecive(b);
        roundPartical.SetTimeScaleRecive(b);
    }
    public void SetColor(Color c)
    {
        shine.color = c;
        roundPartical.SetColor(c);
    }

    public void SetLayer(int l)
    {
        roundPartical.SetLayer(l);
        shine.sortingOrder = l;
    }


    public void StartAnime(UF.AnimeCallback cb = null)
    {
        shineA.StartAnime();
        roundPartical.StartAnime(() => { Destroy(gameObject); });
            
    }

}
