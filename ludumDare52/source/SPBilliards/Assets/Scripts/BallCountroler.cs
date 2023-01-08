using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class BallCountroler : MonoBehaviour
{

    public SpriteRenderer BallArt;
    public SpriteRenderer BallArt2;
    public SpriteRenderer BallBorade;
    public SpriteRenderer BallBack;
    public Transform MyTransform;
    public Rigidbody2D MyRigidbody;
    public bool Keep;
    public UF.AnimeCallback HoleCallBack;
    public Color MainColor;
    public int ParticalNum;
    public float ParticalSize;
    public bool MainBall;
    public int ColParticalNum;
    public float ColParticalSize;
    public string Sound;
    public string SoundKill;
    public int Scroe;
    public bool MoveBall;
    public bool PushBall;


    private Vector2 ballArtOffset;
    private Vector2 ballArtOffset2;
    private Vector2 lastPosition;
    private Vector2 lastSpeed;
    private Vector2 size;

    // Start is called before the first frame update
    

    private void ModPosition()
    {
        ballArtOffset = UF.Vector2Mod(ballArtOffset+size/2, size)-size/2;
        ballArtOffset2 = UF.Vector2Mod(ballArtOffset2 + size / 2, size) - size / 2;
    }

    private void SetOffset(Vector2 o)
    {
        BallArt.transform.localPosition = o;
        if (BallArt2 != null)
        {
            BallArt2.transform.localPosition = ballArtOffset2;
        }
    }
    private Vector2 GetDistant()
    {
       
       return UF.RotateVector2((Vector2)MyTransform.position - lastPosition, -MyTransform.rotation.eulerAngles.z/180*Mathf.PI);
    }



    void Start()
    {
        ballArtOffset = BallArt.transform.localPosition;
        lastPosition = MyTransform.position;
        lastSpeed = MyRigidbody.velocity;
        size = UF.GetSpriteSize(BallArt.sprite);
    }

    // Update is called once per frame
    void Update()
    {
        ballArtOffset += GetDistant();
        ballArtOffset2 -= GetDistant();
        lastPosition = MyTransform.position;
        ModPosition();
        SetOffset(ballArtOffset);
        Vector2 tem = HoleManager.CheckHole(transform.position);
        if (tem!=Vector2.zero)
        {
            particalManager.GlobalManager.BoomParticalBust(ParticalNum, tem, MainColor, ParticalSize*0.7f,true);

            particalManager.GlobalManager.CreateShinePartical(tem, ParticalSize, MainColor, true) ;
            BallCounter.NB(this);

            if (MainBall)
            {
                BackEffect.BoomAt(transform.position, 2,0);
            }
            else
            {
                BackEffect.BoomAt(transform.position, 1,1);
            }
            transform.position = new Vector2(100, 100);
            SoundManager.Play(Sound);
            if (!Keep)
            {
                Destroy(gameObject);
            }
            if(HoleCallBack != null)
            {
                HoleCallBack();

            }
            
            
        }
    }

    public void Killed()
    {
        particalManager.GlobalManager.CreateShinePartical(transform.position, ParticalSize, MainColor, true);
        transform.position = new Vector2(100, 100);
        SoundManager.Play(SoundKill);
        if (HoleCallBack != null)
        {
            HoleCallBack();

        }
        if (!Keep)
        {
            Destroy(gameObject);
        }
    }

    public void ResetSprite()
    {
        lastPosition = MyTransform.position;
        ballArtOffset = new Vector2(0.448f, 0.145f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManager.Play("hit", Mathf.Lerp(0.05f, 0.08f, collision.relativeVelocity.magnitude / 8));
        particalManager.GlobalManager.SideParticalBust(ColParticalNum, collision.contacts[0].point,MainColor
            , ColParticalSize * Mathf.Lerp(0.6f,2,collision.relativeVelocity.magnitude/8), collision.relativeVelocity,true);
    }

}
