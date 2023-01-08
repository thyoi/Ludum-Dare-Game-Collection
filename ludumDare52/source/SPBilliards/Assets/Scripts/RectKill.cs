using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectKill : MonoBehaviour
{
    public AnimeCountroler bl;
    public AnimeCountroler wl;

    private Bullet wlc;

    // Start is called before the first frame update
    void Start()
    {
        bl.StartAnime(() =>
        {
            wl.StartAnime();
            Destroy(bl.gameObject);
        });
        wlc = wl.transform.GetComponent<Bullet>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
