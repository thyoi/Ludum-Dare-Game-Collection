using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCountroler : MonoBehaviour
{
    public SpriteRenderer border;
    public SpriteRenderer back;
    public Transform Mask;
    public Vector2 endSIze;




    private void SetSize(Vector2 s)
    {
        border.size = s;
        back.size = s;
        Mask.transform.localScale = s / 4.02f;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetSize(endSIze);
    }

    // Update is called once per frame
    void Update()
    {
        SetSize(endSIze);
    }
}
