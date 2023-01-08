using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RoundPartical : MonoBehaviour
{

    public AnimeCountroler Round;
    public AnimeCountroler Mask;
    public SortingGroup sg;
    
    public void SetTimeScaleRecive(bool b)
    {
        Round.SetTimeScaleRecive(b);
        Mask.SetTimeScaleRecive(b);
    }
    
    public void SetLayer(int t)
    {
        sg.sortingOrder = t;
    }

    public void SetColor(Color c)
    {
        Round.transform.GetComponent<SpriteRenderer>().color = c;
    }

    
    public void StartAnime(UF.AnimeCallback cb = null)
    {
        Round.StartAnime();
        if (cb != null)
        {
            Mask.StartAnime(() => {  Destroy(gameObject); cb(); });
        }
        else
        {
            Mask.StartAnime(() => { Destroy(gameObject); });
        }
    }
}
