using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectKill : MonoBehaviour
{
    public AnimeCountroler bl;
    public AnimeCountroler wl;

    public float SoundTime;
    public bool onStart;
    public bool onEnd;
    private Bullet wlc;
    public UF.AnimeCallback endCallback;

    public float KillTime = 0.1f;

    // Start is called before the first frame update
    public void StartPow(UF.AnimeCallback cb)
    {
        if (cb != null)
        {
            bl.StartAnime(() =>
            {
                wl.StartAnime(endCallback);
                Destroy(bl.gameObject);
                cb();
            });
        }
        else
        {
            bl.StartAnime(() =>
            {
                wl.StartAnime(endCallback);
                Destroy(bl.gameObject);
            });
        }
        onStart = true;
        
        wlc = wl.transform.GetComponent<Bullet>();

    }



    // Update is called once per frame
    void Update()
    {
        if (onStart)
        {
            SoundTime -= Time.deltaTime;
            if (SoundTime <= 0)
            {
                SoundManager.Play("sansP1");
                onStart = false;
                onEnd = true;
            }

        }
        if (onEnd)
        {
            KillTime -= Time.deltaTime;
            if (KillTime <= 0)
            {
                onEnd = false;
                wlc.Active = false;
            }
        }
    }
}
