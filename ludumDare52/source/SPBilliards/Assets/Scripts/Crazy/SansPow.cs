using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SansPow : MonoBehaviour
{
    // Start is called before the first frame update
    public RectKill KillAera;
    public bool StartSound;
    public Color ShineColor;
    public AnimeCountroler Eye;
    public Vector2 StartPosition;
    public float StartZ;
    public float PowZ;
    public float EndZ;
    public Vector2 PowPosition;
    public Vector2 EndPosition;
    public AnimeCountroler myCountroler;
    public float PowDelay;
    public bool onStart;
    public AnimationCurve Ecurve;
    public AnimeCountroler[] Face;


    public void SetAnime(Vector2 init, float initZ,Vector2 end, float endZ,bool e,UF.AnimeCallback cb)
    {
        transform.position = init;
        if (e)
        {
            myCountroler.positionX.Curve = Ecurve;
            myCountroler.positionY.Curve = Ecurve;
            myCountroler.RotateZ.Curve = Ecurve;
            myCountroler.positionX.Delay = 0f;
            myCountroler.positionY.Delay = 0f;
            myCountroler.RotateZ.Delay = 0f;
            foreach (AnimeCountroler a in Face)
            {
                a.StartAnime();
            }
        }
        UF.SetRotationZ(transform, initZ);
        myCountroler.SetPosition(init, end);
        myCountroler.RotateZ.Init = initZ;
        myCountroler.RotateZ.End = endZ;
        myCountroler.StartAnime(cb);

    }


    public void DestoryThis()
    {
        Destroy(gameObject);
    }


    public void Fire()
    {
        KillAera.endCallback = DestoryThis;
        Eye.StartAnime();
        if (StartSound)
        {
            SoundManager.Play("sansP0");
        }
            KillAera.StartPow(StartKill);
    }

    public void StartKill()
    {
        particalManager.GlobalManager.CreateShinePartical(new Vector2(-0.31f,0.34f), 2.5f, ShineColor, true, transform, true);
        particalManager.GlobalManager.CreateShinePartical(new Vector2(0.31f, 0.34f), 2.5f, ShineColor, true, transform, true);
        SetAnime(PowPosition, PowZ, EndPosition, EndZ, true, null);

    }
    void Start()
    {
        SetAnime(StartPosition, StartZ, PowPosition, PowZ,false,null);
        onStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (onStart)
        {
            PowDelay -= Time.deltaTime;
            if (PowDelay <= 0)
            {
                onStart = false;
                Fire();
            }
        }
    }
}
