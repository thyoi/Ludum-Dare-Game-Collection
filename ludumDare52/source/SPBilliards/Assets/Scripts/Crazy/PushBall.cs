using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBall : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer bord;
    public SpriteRenderer back;
    public AnimeCountroler color;
    public bool Inf;

    public int N;
    public Color[] colorList;

    private float count;

    public void Boom()
    {

        if (N > 0)
        {
            KillManager.CPK(transform.position, 2, colorList[N], 0.01f);
            if (!Inf)
            {
                N--;
            }
            bord.color = colorList[N];
            back.color = colorList[N];
            UF.SetApha(back, 0);
            if (N <= 0)
            {
                UF.SetApha(back, 1);
            }
            else
            {
                color.StartAnime(Boom);
            }
        }
    }

    void Start()
    {
        bord.color = colorList[N];
        back.color = colorList[N];
        UF.SetApha(back, 0);
        count = 0;
        color.StartAnime(Boom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
