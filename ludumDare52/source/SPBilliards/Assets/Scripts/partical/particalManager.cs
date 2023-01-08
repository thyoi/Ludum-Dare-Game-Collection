using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEditor.Progress;

public class particalManager : MonoBehaviour
{
    public static particalManager GlobalManager;
    public static void Return(AnimeCountroler a)
    {
        GlobalManager.ReturnPartical(a);
    }




    public GameObject BaseParticals;
    public GameObject RoundParticalPrefab;
    public GameObject ShineParticalPrefab;
    public AnimeCountrolerData[] Datas;
    public Sprite[] Sprites;



    private AnimeCountroler[] BaseParticalPool;
    private int poolCount; 

    private void CreateBaseParticalAtPositionAndDriction(AnimeCountrolerData data
        ,Sprite s,Vector2 initPosition, Vector2 endPosition, float Size, float vSpeed
        ,float zSpeed,float aSpeed,Color c,bool TimeScale,Transform t = null,bool layer = false)
    {
        if (poolCount > 0)
        {
            AnimeCountrolerData tem = new AnimeCountrolerData();
            tem.Copy(data);
            tem.positionX.Init = initPosition.x;
            tem.positionY.Init = initPosition.y;
            tem.positionX.End = endPosition.x;
            tem.positionY.End = endPosition.y;
            tem.ScaleX.Init *= Size;
            tem.ScaleY.Init *= Size;
            tem.ScaleX.End *= Size;
            tem.ScaleY.End *= Size;
            tem.positionX.Time *= vSpeed;
            tem.positionY.Time *= vSpeed;
            tem.Apha.Time *= aSpeed;
            tem.RotateZ.Time *= zSpeed;
            poolCount--;
            BaseParticalPool[poolCount].returnPartical = true;
            BaseParticalPool[poolCount].SetSprite(s);
            BaseParticalPool[poolCount].SetColor(c);
            BaseParticalPool[poolCount].StartAnime(tem);
            BaseParticalPool[poolCount].TimeScaleRecive = TimeScale;
            if (t != null)
            {
                BaseParticalPool[poolCount].transform.parent = t;
            }
            if (layer)
            {
                BaseParticalPool[poolCount].SetLayer(22);
            }
        }

    }

    private void RandomParticalBust(int n, AnimeCountrolerData[] datas, Sprite[] sprites,
        Vector2 initPosition, float initRange,float initBase, float endRange, float endBase, bool sameDri,
        float scaleBase, float scaleRange,float speedBase, float speedRange,bool speedBaseOnDis, 
        bool aSpeedBaseOnDis,Color c, Vector2 AngleRange,bool ScaleRalate,bool TimeScale,Transform t = null, bool layer = false)
    {
        for(int i = 0; i < n; i++)
        {
            int type = Random.Range(0, datas.Length);
            float initAngle, initDis;
            initAngle = Random.Range(AngleRange.x, AngleRange.y);
            float endAngle, endDis;
            float asp = 1;
            float zsp = 1;
            if (initRange != 0)
            {
                
                initDis = initBase * (1 - initRange + Random.Range(0, 2 * initRange));
                initPosition += new Vector2(Mathf.Sin(initAngle) * initDis, Mathf.Cos(initAngle) * initDis);

            }
            if (sameDri)
            {
                endAngle = initAngle;
            }
            else
            {
                endAngle = Random.Range(AngleRange.x, AngleRange.y);
            }
            endDis = endBase * (1 - endRange + Random.Range(0, 2 * endRange));
            Vector2 endPosition = initPosition+ new Vector2(Mathf.Sin(endAngle) * endDis, Mathf.Cos(endAngle) * endDis);
            float temSpeed;
            temSpeed = speedBase * (1 - speedRange + Random.Range(0, speedRange * 2));
            if (speedBaseOnDis)
            {
                temSpeed *= endDis/endBase;
            }
            if (aSpeedBaseOnDis)
            {
                float tt = (endDis / endBase) * 0.3f + 0.7f;
                asp *= tt;
                zsp *= tt;
            }
            float tScale = scaleBase * (1 - scaleRange + Random.Range(0, scaleRange * 2));
            if (ScaleRalate)
            {
                tScale *= ((endBase*2-endDis) / endBase) * 0.4f + 0.6f;
            }
            CreateBaseParticalAtPositionAndDriction(datas[type], sprites[type], initPosition, endPosition
                , tScale
                , temSpeed, zsp,asp,c, TimeScale, t,layer);
        }
    }

    public void DefaultParticalBust(int n,Vector2 p,Color c,bool TimeScale, Transform t = null, bool layer = false)
    {
        RandomParticalBust(n, Datas, Sprites, p, 0.3f, 0.01f, 0.8f, 0.3f, true, 1, 0.2f, 1, 0, true, true
            ,c,new Vector2(0,Mathf.PI*2),true, TimeScale, t,layer);
    }
    public void BoomParticalBust(int n, Vector2 p, Color c, float size, bool TimeScale, Transform t = null, bool layer = false)
    {
        RandomParticalBust(n, Datas, Sprites, p, 0.3f, 0.01f, 0.8f, 0.8f*size, true, 1*size, 0.5f, 1, 0, false, false
            , c, new Vector2(0, Mathf.PI * 2), true, TimeScale, t, layer);
    }
    public void SideParticalBust(int n, Vector2 p, Color c, float size,Vector2 dir, bool TimeScale, Transform t = null, bool layer = false)
    {
        float ang = UF.AngleVector(dir);
        RandomParticalBust(n/2, Datas, Sprites, p, 0.3f, 0.01f, 0.8f, 0.8f * size, true, 1 * size, 0.5f, 1, 0, false, false
            , c, new Vector2(ang+Mathf.PI/4- Mathf.PI / 6, ang + Mathf.PI / 4 + Mathf.PI / 6), true, TimeScale, t, layer);
        //RandomParticalBust(n / 2, Datas, Sprites, p, 0.3f, 0.01f, 0.8f, 0.8f * size, true, 1 * size, 0.5f, 1, 0, false, false
        //    , c, new Vector2(ang + Mathf.PI / 4 - Mathf.PI / 12, ang + Mathf.PI / 4 + Mathf.PI / 12), true, t, layer);
    }

    public particalManager()
    {
        GlobalManager = this;
    }

    public void ReturnPartical(AnimeCountroler p)
    {
        p.transform.parent = this.transform;
        p.transform.position = new Vector2(100, 100);
        BaseParticalPool[poolCount] = p;
        poolCount++;
        p.active = false;
    }


    public Transform CreateRoundPartical(Vector2 p, float size,Color c, bool TimeScale, Transform t = null,bool layer = false)
    {
        if(t == null)
        {
            t = this.transform;
        }
        Transform tem = Instantiate(RoundParticalPrefab, t).transform;
        tem.localPosition = p;
        tem.localScale = new Vector2(size, size);
        RoundPartical ttem = tem.GetComponent<RoundPartical>();
        if (layer)
        {
            ttem.SetLayer(22);
        }
        ttem.GetComponent<RoundPartical>().StartAnime();
        ttem.SetColor(c);
        ttem.SetTimeScaleRecive(TimeScale);
        return ttem.Round.transform;
    }

    public Transform CreateShinePartical(Vector2 p, float size,Color c ,bool TimeScale,Transform t = null, bool layer = false)
    {
        if (t == null)
        {
            t = this.transform;
        }
        Transform tem = Instantiate(ShineParticalPrefab, t).transform;
        tem.localPosition = p;
        tem.localScale = new Vector2(size, size);
        ShinePartical ttem = tem.GetComponent<ShinePartical>();
        if (layer)
        {
            ttem.GetComponent<ShinePartical>().SetLayer(22);
        }
        ttem.SetColor(c);
        ttem.GetComponent<ShinePartical>().StartAnime();
        ttem.SetTimeScaleRecive(TimeScale);
        return ttem.roundPartical.Round.transform;
    }


    // Start is called before the first frame update
    void Start()
    {
        poolCount = 1000;
        BaseParticalPool = new AnimeCountroler[1000];
        for(int i = 0; i < 1000; i++)
        {
            BaseParticalPool[i] = Instantiate(BaseParticals).transform.GetComponent<AnimeCountroler>();
            BaseParticalPool[i].active = true;
            BaseParticalPool[i].transform.parent = this.transform;
            BaseParticalPool[i].transform.position = new Vector2(100, 100);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
