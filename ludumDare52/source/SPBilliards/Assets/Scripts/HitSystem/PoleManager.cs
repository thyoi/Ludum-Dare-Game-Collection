using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleManager : MonoBehaviour
{
    public SpriteRenderer Bord;
    public AnimeCountroler Mask;



    private void SetApha(float a)
    {
        UF.SetApha(Bord, a);
    }

    public void Show(UF.AnimeCallback cb = null)
    {
        Mask.StartAnime(cb);
        SetApha(1);
    }

    public void Hide()
    {
        transform.position = new Vector2(100, 100);
    }



}
