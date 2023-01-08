using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeCountroler : MonoBehaviour
{
    public bool TimeScaleRecive;
    public AnimeFloatProperty positionX;
    public AnimeFloatProperty positionY;
    public AnimeFloatProperty ScaleX;
    public AnimeFloatProperty ScaleY;
    public AnimeFloatProperty Apha;
    public AnimeFloatProperty RotateZ;
    public int CallBackProperty;

    public bool returnPartical = false;
    public bool active = true;
   

    private AnimeFloatProperty[] AllAnimeProperty;
    private Transform myTransform;
    private SpriteRenderer mySpriteRenderer;
    private float dt;

    private void UpdateDT()
    {
        if (TimeScaleRecive)
        {
            dt = Time.deltaTime;
        }
        else
        {
            dt = TimeManager.DT();
        }
    }

    private void UpdatePosition()
    {
        bool UpdateFlag = false;
        Vector3 tem = myTransform.localPosition;
        if (!positionX.Ignore)
        {
            if (positionX.Update(dt))
            {
                tem.x = positionX.Cur;
                UpdateFlag = true;
            }
        }
        if (!positionY.Ignore)
        {
            if (positionY.Update(dt))
            {
                tem.y = positionY.Cur;
                UpdateFlag = true;
            }
        }
        if (UpdateFlag)
        {
            myTransform.localPosition = tem;
        }
    }

    private void UpdateScale()
    {
        bool UpdateFlag = false;
        Vector3 tem = myTransform.localScale;
        if (!ScaleX.Ignore)
        {
            if (ScaleX.Update(dt))
            {
                tem.x = ScaleX.Cur;
                UpdateFlag = true;
            }
        }
        if (!ScaleY.Ignore)
        {
            if (ScaleY.Update(dt))
            {
                tem.y = ScaleY.Cur;
                UpdateFlag = true;
            }
        }
        if (UpdateFlag)
        {
            myTransform.localScale = tem;
        }
    }

    private void UpdateApha()
    {
        if (!Apha.Ignore)
        {
            if (Apha.Update(dt))
            {
                UF.SetApha(mySpriteRenderer, Apha.Cur);
            }
        }
    }
    private void UpdateRotateZ()
    {
        if (!RotateZ.Ignore)
        {
            if (RotateZ.Update(dt))
            {
                UF.SetRotationZ(myTransform, RotateZ.Cur);
            }
        }
    }

    private void UpdateAll()
    {
        UpdatePosition();
        UpdateScale();
        UpdateApha();
        UpdateRotateZ();
    }

    private void StartAnimeProperties(AnimeFloatProperty[] af)
    {
        for(int i = 0; i < af.Length; i++)
        {
            if (!af[i].Ignore)
            {
                af[i].StartAnime();
            }
        }
    }

    public void StartAnime(UF.AnimeCallback cb = null)
    {
        active = true;
        StartAnimeProperties(AllAnimeProperty);
        AllAnimeProperty[CallBackProperty].Callback = cb;
        if (returnPartical)
        {
            Apha.Callback = () => { particalManager.Return(this); };
        }

    }

    public void StartAnime(AnimeCountrolerData data, UF.AnimeCallback cb = null)
    {
        positionX = data.positionX;
        positionY = data.positionY;
        ScaleX = data.ScaleX;
        ScaleY = data.ScaleY;
        Apha = data.Apha;
        RotateZ = data.RotateZ;
        CallBackProperty = data.CallBackProperty;
        InitProperList();
        StartAnime(cb);    
    }

    private void InitProperList()
    {
        AllAnimeProperty = new AnimeFloatProperty[6] { positionX, positionY, ScaleX, ScaleY, Apha, RotateZ };
    }

    private void Awake()
    {
        myTransform = transform;
            mySpriteRenderer = myTransform.GetComponent<SpriteRenderer>();
        InitProperList();
    }

    public void SetSprite(Sprite s)
    {
        mySpriteRenderer.sprite = s;
    }

    public void SetColor(Color c)
    {
        Color tem = mySpriteRenderer.color;
        tem.r = c.r;
        tem.g = c.g;
        tem.b = c.b;
        mySpriteRenderer.color = tem;
    }

    public void SetTimeScaleRecive(bool b)
    {
        TimeScaleRecive = b;
    }

    public void SetLayer(int i)
    {
        mySpriteRenderer.sortingOrder = i;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            UpdateDT();
            UpdateAll();
        }
    }
}
