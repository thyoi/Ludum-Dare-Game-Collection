using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KillManager : MonoBehaviour
{
    public static KillManager GlobalManager;
    
    public static void CRK(Vector2 p,float s, Color c)
    {
        GlobalManager.CreateRoundKill(p, s, c);

    }

    public static void CPK(Vector2 p, float s, Color c, float Center = 0)
    {
        GlobalManager.CreatePushKill(p, s, c,Center);
    }

    public KillManager()
    {
        GlobalManager = this;
    }



    public GameObject RoundKill;
    public GameObject RectKill;
    public GameObject BulletKill;
    public GameObject PushKill;



    public void CreateRoundKill(Vector2 p, float size,Color c)
    {
        Transform tem = particalManager.GlobalManager.CreateShinePartical(p, size, c, true);
        GameObject ttem = Instantiate(RoundKill);
        ttem.transform.GetComponent<RoundKill1>().round = tem;
        ttem.transform.GetComponent<RoundKill1>().Scale = size;
        BackEffect.BoomAt(p, 0);
        SoundManager.Play("b3");
    }

    public void CreatePushKill(Vector2 p, float size, Color c, float centerRange = 0)
    {
        Transform tem = particalManager.GlobalManager.CreateRoundPartical(p, size, c, true);
        particalManager.GlobalManager.BoomParticalBust(10, p, c, size / 2, true);
        GameObject ttem = Instantiate(PushKill);
        ttem.transform.GetComponent<PushKill>().round = tem;
        ttem.transform.GetComponent<PushKill>().Scale = size;
        ttem.transform.GetComponent<PushKill>().CenterRaange = centerRange;
        BackEffect.BoomAt(p, 1);
        SoundManager.Play("push");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
