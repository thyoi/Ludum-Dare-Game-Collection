using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DArea : MonoBehaviour

{
    public SpriteRenderer bord;
    public SpriteRenderer back;
    public AnimeCountroler bordA;
    public AnimeCountroler backA;
    public float Size;
    public Color c;
    // Start is called before the first frame update
    void Start()
    {
        Color tem = c;
        tem.a = 0;
        bord.color = c;
        back.color = c;
        bordA.ScaleX.End *= Size;
        bordA.ScaleY.End *= Size;
        bordA.StartAnime(StartBoom);
        backA.StartAnime();
    }

    public void StartBoom()
    {
        KillManager.CRK(transform.position, Size, c);
        bordA.Apha.Ignore = false;
        bordA.Apha.StartAnime(() =>
        {
            Destroy(this.gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
