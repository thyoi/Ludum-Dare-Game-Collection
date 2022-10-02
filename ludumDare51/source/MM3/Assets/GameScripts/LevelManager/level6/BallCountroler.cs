using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCountroler : MonoBehaviour
{
    public Vector2 Speed;
    public float v;
    public BrickCountroler[] bricks;
    public int timeLayer = 3;
    public GameObject Boom;
    public bool bigBall;

    public bool active;

    private Transform myTransform;
    private SpriteRenderer spriteRenderer;
    private float dt;
    // Start is called before the first frame update
    void Awake()
    {
        myTransform = transform;
        spriteRenderer = myTransform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame

    public void CreateBoom()
    {
        Boomable tem = Instantiate(Boom, transform).transform.GetComponent<Boomable>();
        tem.spriteRenderer = spriteRenderer;
        tem.StartBoom();
    }
    void Update()
    {
        if (active)
        {
            dt = TimeManager.DeltaTime(timeLayer);
            Vector2 next = (Vector2)myTransform.position + Speed * dt;
            foreach (BrickCountroler b in bricks)
            {
                if (!(!b.mainBrick && !b.wall && bigBall))
                {
                    if (b.Active && b.InRange(next))
                    {
                        BrickCountroler.Line tem = b.Collection(next);


                        if (tem == BrickCountroler.Line.up && b.mainBrick)
                        {

                            Vector2 temSpeed = new Vector2(myTransform.position.x - b.transform.position.x, 0.4f);
                            temSpeed.Normalize();
                            temSpeed *= v;
                            Speed = temSpeed;
                            next = (Vector2)myTransform.position + Speed * dt;
                            CreateBoom();
                            break;
                        }
                        if (tem == BrickCountroler.Line.up || tem == BrickCountroler.Line.down)
                        {
                            Speed.y = -Speed.y;
                            next = (Vector2)myTransform.position + Speed * dt;
                            CreateBoom();
                            b.Hit();
                            break;
                        }
                        if (tem == BrickCountroler.Line.left || tem == BrickCountroler.Line.right)
                        {
                            Speed.x = -Speed.x;
                            next = (Vector2)myTransform.position + Speed * dt;
                            CreateBoom();
                            b.Hit();
                            break;
                        }
                    }

                }
                else
                {
                    if (b.Active)
                    {
                        Vector2 dis = b.transform.position - transform.position;
                        if ((dis.x * dis.x + dis.y * dis.y) < 0.6f)
                        {
                            b.Hit();
                            SoundManager.PlaySound("bigball");
                        }
                    }
                }

            }
            UF.SetPosition(myTransform, next);
        }
    }
}
