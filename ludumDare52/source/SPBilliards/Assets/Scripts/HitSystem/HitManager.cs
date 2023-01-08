using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HitManager : MonoBehaviour
{
    public bool active;
    public bool tur;
    public bool PushAbility;
    public bool MoveAbility;
    public bool powerEnough;
    public PoleManager Pole;
    public Transform PoleTransform;
    public MouseBox HitBox;
    public Transform BollPosition;
    public BallCountroler BallMain;
    public Rigidbody2D BallRigidbudy;
    public Wiggle WiggleOffset;
    public BullteTimeManager BulletTime;
    public float WiggleScale;
    public float WiggleInter;
    public float PowerTime;
    public bool PowerUp;
    public float PowerParticalTime;
    public bool MaskIsOn;
    public MaskEffectManager MaskManager;
    public GameObject PolePartical;
    public Color MouseColor;
    public Color PowerColor;
    public HPManager hpM;
    public bool BallIsDead;
    public Vector2 BallInitPosition;
    public AnimeCountroler[] Nodes;
    public bool NodeLine;
    public MainManager mainManager;
    public UF.AnimeCallback BallDeadCallBack;



    private float forceCountDown;
    private Vector2 forceValue;
    private bool forceH;
    private float PowerParticalCount;
    private float powerCount;
    private bool lastPower;
    private float WiggleCount;

    private bool MouseIsDown = false;



    public void BallDie()
    {
        BallIsDead = true;
        hpM.HPDown(50);
        if (BallDeadCallBack != null)
        {
            BallDeadCallBack();
        }

    }

    public void BallLive()
    {
        if (BallIsDead)
        {
            BollPosition.position = BallInitPosition;
            particalManager.GlobalManager.CreateShinePartical(BallInitPosition, 1.2f, Color.white, true);
            particalManager.GlobalManager.BoomParticalBust(100, BallInitPosition,  Color.white, 2, true);
            BallIsDead = false;
            BallRigidbudy.velocity =Vector3.zero;
            BallMain.ResetSprite();
            BackEffect.BoomAt(BallInitPosition, 2);
            SoundManager.Play("live");
        }
    }

    private void MouseUp()
    {
        if (MouseIsDown) { 
        CreatePolePartical(PowerUp);
        hidePole();
        MouseIsDown = false;
        PowerUp = false;
        HideMask();
       mainManager.HitPole();
        }

    }

    public void DeActive()
    {
        if(active == true)
        {
            active = false;
            if (MouseIsDown)
            {
                HideMask();
                MouseIsDown = false;
                PowerUp = false;
                hidePole();
            }
        }
        
    }

    private void ReActive()
    {
        active = true;
    }


    private void MouseDown()
    {
        if (active && GetPoleDis()<3f && ! MouseIsDown && powerEnough && !BallIsDead  &&!(tur&&(BallRigidbudy.velocity.sqrMagnitude>0.01)))
        {
            UpdatePolePosition();
            ShowPole();
            MouseIsDown = true;
            lastPower = false;
            PowerUp = false;
            powerCount = 0;
            particalManager.GlobalManager.DefaultParticalBust(10, MouseManager.MousePosition(),MouseColor, true);

            if (!tur)
            {
                StartBulletTime(); 
                ShowMask();
            }
            //particalManager.GlobalManager.CreateShinePartical(MouseManager.MousePosition(), 1f);
            //BackEffect.BoomAt(MouseManager.MousePosition(),0);
        }
    }

    private void StartBulletTime()
    {
        BulletTime.speed = 300;
        BulletTime.BulletTimeScale(0.01f);
    }
    private void EndBulletTime()
    {
        BulletTime.speed = 2000;
        BulletTime.BulletTimeScale(1);
    }

    private void ShowNode()
    {
        for(int i = 0; i < Nodes.Length; i++)
        {
            Nodes[i].Apha.Delay = 0.12f * (i + 1);
            Nodes[i].StartAnime();
            UF.SetApha(Nodes[i].transform.GetComponent<SpriteRenderer>(), 0);
        }
    }

    private void HideNode()
    {
        for(int i = 0; i < Nodes.Length; i++)
        {
            Nodes[i].transform.position = new Vector2(100, 100);
        }
    }

    private void ShowPole()
    {
        Pole.Show();
        ShowNode();
    }
    private void hidePole()
    {
        Pole.Hide();
        HideNode();
    }

    private void CreatePolePartical(bool heavy)
    {
        Vector2 a = GetPoleAngle();
        Transform tem = Instantiate(PolePartical).transform;
        UF.SetRotationZ(tem, Vector2.Angle(Vector2.right, a) * ((a.y > 0) ? 1 : -1));
        Vector2 initP = a * (1.34f + GetPoleDis() * 0.4f) + (Vector2)BollPosition.position;
        Vector2 endP = a*1.1f+ (Vector2)BollPosition.position;
        AnimeCountroler ta = tem.GetComponent<AnimeCountroler>();
        ta.positionX.Init = initP.x;
        ta.positionY.Init = initP.y;
        ta.positionX.End = endP.x;
        ta.positionY.End = endP.y;
        ta.StartAnime(() => { Destroy(ta.gameObject); });
        CreateBallForce(heavy);
        if (!tur) { hpM.HPDown(Mathf.Lerp(10, 30, forceValue.magnitude / 8)); }
        if(hpM.hpState == 1)
        {
            powerEnough = false;
        }
    }


    private void CreateBallForce(bool heavy)
    {
        float forceS = Mathf.Lerp(0.1f, 5, GetPoleDis() / 4)+((heavy)?3:0);
        forceH = heavy;
        forceValue = forceS * -GetPoleAngle();
        forceCountDown = 0.1f;
    }

    private void UpdateForce()
    {
        if (forceCountDown > 0)
        {
            forceCountDown -= TimeManager.DT();
            if (forceCountDown <= 0)
            {
                DeployeeForce();
                EndBulletTime();
            }
        }
    }

    private void DeployeeForce()
    {
        BallRigidbudy.velocity = Vector3.zero;
        BallRigidbudy.AddForce(forceValue,ForceMode2D.Impulse);
        Vector2 fp = (forceValue.normalized * 0.128f) + (Vector2)BollPosition.position;
        SoundManager.Play("pole");
        if (forceH)
        {
            particalManager.GlobalManager.BoomParticalBust(20, fp, PowerColor,1,true);
        }
        else
        {
            particalManager.GlobalManager.BoomParticalBust(20, fp, Color.white
                , Mathf.Lerp(0.2f,0.6f,forceValue.magnitude/8),true);
        }
        
    }
    private void ShowMask()
    {
        MaskIsOn = true;
    }

    private void HideMask()
    {
        MaskIsOn = false;
    }
    private void UpdateNodesPosition()
    {
        SetNodePosition(-GetPoleAngle(), Mathf.Lerp(0.1f, 0.6f, GetPoleDis() / 7));
    }

    private void SetNodePosition(Vector2 a,float d)
    {
        for(int i = 0; i < Nodes.Length; i++)
        {
            Nodes[i].transform.position = (Vector2)BollPosition.position + (a * (d * (i+1)));
        }
    }

    private void UpdatePolePosition()
    {
        SetPolePosition(GetPoleAngle(),1.34f + GetPoleDis() * 0.4f);
    }

    private void SetPolePosition(Vector2 a,float d)
    {

        UF.SetRotationZ(PoleTransform, Vector2.Angle(Vector2.right,a)*((a.y>0)?1:-1));
        PoleTransform.localPosition = a * d + (Vector2)BollPosition.position +(WiggleScale* WiggleOffset.GetValue());
    }
    private float GetPoleA()
    {
        Vector2 a = GetPoleAngle();
        return Vector2.Angle(Vector2.right, a) * ((a.y > 0) ? 1 : -1)*Mathf.PI/180;
    }
    private Vector2 GetPoleAngle()
    {
        return (MouseManager.MousePosition() - ((Vector2)BollPosition.position)).normalized;
    }

    private float GetPoleDis()
    {
        return (MouseManager.MousePosition() - ((Vector2)BollPosition.position)).magnitude;
    }

    private void UpdateWiggleScale()
    {
        if (PowerUp)
        {
            WiggleScale = 2;
        }
        else
        {
            WiggleScale = Mathf.Lerp(0, 1.4f, (GetPoleDis()-2f) / 1.5f);
        }
    }

    private void CreatePowerPartical()
    {
        float a = GetPoleA();
        float d = Random.Range(-1.3f, 1.3f);
        Vector2 tem = new Vector2(Mathf.Cos(a)*d, Mathf.Sin(a)*d);

        particalManager.GlobalManager.DefaultParticalBust(1, tem + (Vector2)Pole.transform.position
            , PowerColor, false, null, true);
        
    }

    private void UpdatePower()
    {
        if (!tur)
        {
            if (PowerUp)
            {
                PowerParticalCount += TimeManager.DT();
                if (PowerParticalCount >= PowerParticalTime)
                {
                    PowerParticalCount = 0;
                    CreatePowerPartical();
                }
            }
            else
            {
                bool onPower = (lastPower) ? (GetPoleDis() > 3.2f) : (GetPoleDis() > 3.7f);

                if (onPower)
                {
                    if (!lastPower)
                    {
                        SoundManager.Play("powerUp1");
                    }
                    powerCount += TimeManager.DT();
                    if (powerCount >= PowerTime)
                    {
                        PowerUp = true;
                        PowerUpEffect();
                    }
                }
                else
                {
                    if (lastPower)
                    {
                        SoundManager.Stop("powerUp1");
                    }
                    powerCount -= TimeManager.DT();
                    if (powerCount <= 0)
                    {
                        powerCount = 0;
                    }
                }


                lastPower = onPower;
            }
        }
    }

    private void PowerUpEffect()
    {
        SoundManager.Play("powerUp2");
        particalManager.GlobalManager.CreateShinePartical(new Vector2(-1.3f, 0), 1, PowerColor,false,Pole.transform, true);
    }

    private void UpdateMask()
    {
        if (MaskIsOn)
        {
            //MaskManager.Size = Mathf.Lerp(15, 1, (GetPoleDis() - 2) / 1.5f);
            MaskManager.Size = 0.8f;
            MaskManager.transform.position = BollPosition.position;
        }
        else
        {
            MaskManager.Size = 15;
        }
    }

    public void PowerRecover()
    {
        powerEnough = true;
    }

    public void MouseRightDown()
    {
        if (PushAbility && powerEnough)
        {
            KillManager.CPK(MouseManager.MousePosition(), 1.5f, PowerColor);
            
            hpM.HPDown(3);
            if (hpM.hpState == 1)
            {
                powerEnough = false;
            }
        }
    }

    public float moveParticalTime;
    private float moveParticalCount;
    private Vector2 MoveDir;
    private Color MoveParticalColor;
    private float MoveParticalSize;
    public void UpdateMove()
    {
            MoveDir = Vector2.zero;
        if(active && hpM.hp > 1)
        {
            if (Input.GetKey(KeyCode.A))
            {
                MoveDir += Vector2.left;
            }
            if (Input.GetKey(KeyCode.W))
            {
                MoveDir += Vector2.up;
            }
            if (Input.GetKey(KeyCode.D))
            {
                MoveDir += Vector2.right;
            }
            if (Input.GetKey(KeyCode.S))
            {
                MoveDir += Vector2.down;
            }
            
            if (hpM.hp < hpM.hpState1)
            {
                MoveParticalColor = hpM.c1;
                BallRigidbudy.AddForce(MoveDir * Time.deltaTime, ForceMode2D.Impulse);
                MoveParticalSize = 0.4f;
            }
            else
            {
                MoveParticalColor = hpM.c2;
                BallRigidbudy.AddForce(MoveDir * Time.deltaTime * 1.3f, ForceMode2D.Impulse);
                MoveParticalSize = 0.6f;
            }
        }

    }
    public void UpdateMovePartical()
    {
        if (MoveDir!= Vector2.zero)
        {
            moveParticalCount += Time.deltaTime;
            if (moveParticalCount >= moveParticalTime)
            {
                moveParticalCount = 0;
                particalManager.GlobalManager.BoomParticalBust(1, BollPosition.position, MoveParticalColor, MoveParticalSize,true);
            }
            hpM.HPDown(8 * Time.deltaTime);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        WiggleOffset.Init();
        BallMain.HoleCallBack = () => { BallDie(); };
        HitBox.ClickFunction = (Vector2 v) => { MouseDown(); return true; };
        HitBox.UpFunction = (Vector2) => { MouseUp(); return true; };
        hpM.hp1Callback = () => { PowerRecover(); BallLive(); };
        for(int i = 0; i < Nodes.Length; i++)
        {
            float tt = Mathf.Lerp(0, 0.015f, (Nodes.Length - i) *1.0f/ Nodes.Length);
            Nodes[i].transform.localScale = new Vector2(tt, tt);
        }
    }

    


    // Update is called once per frame
    void Update()
    {
        if(active && MouseIsDown)
        {
            UpdatePolePosition();
            UpdatePower();
            if (!tur)
            {
                UpdateNodesPosition();
            }
        }
        WiggleCount += TimeManager.DT();
        if (WiggleCount >= WiggleInter)
        {
            WiggleCount = 0;
            WiggleOffset.RandomWiggle();
        }
        UpdateWiggleScale();
        WiggleOffset.Update(TimeManager.DT());
        UpdateMask();
        UpdateForce();
        if (Input.GetMouseButtonDown(1))
        {
            MouseRightDown();
        }
        if (MoveAbility)
        {
            UpdateMove();
            UpdateMovePartical();
        }
        
    }
}
