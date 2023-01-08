using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour
{
    public AnimeCountroler size;
    public Rigidbody2D rb;
    public float pt;
    public Color pc;
    private float count;
    // Start is called before the first frame update
    void Start()
    {
        size.StartAnime();    
    }

    // Update is called once per frame
    void Update()
    {
        rb.mass = transform.localScale.x * transform.localScale.x;
        count += Time.deltaTime;
        if (count > pt)
        {
            count = 0;
            particalManager.GlobalManager.BoomParticalBust(1, transform.position, pc, 0.5f, true);
        }
    }
}
