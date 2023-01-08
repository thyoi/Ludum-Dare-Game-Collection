using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGrountUnit : MonoBehaviour
{

    public Transform MyTransform;
    public SpriteRenderer MySpriteRenderer;
    public BackGroundUnitTransformData[] TransformDatas;
    public Vector2Int index;
    public bool Active;
    public AnimationCurve DownCurve;
    public float DownSpeed;

    private float curDown;
    private float toDown;
    private float pushDown;
    private float setDown;
    private float setTime;
    private BackGroundUnitTransformData curTransform;
    private float transformCount;
    private bool onTransform;
    private UF.AnimeCallback transformCallback;
    private float[] SizeList;
    private float[] RotateList;
    private float dt;


    // Start is called before the first frame update
    void Start()
    {
        SizeList = new float[2] { 0,0};
        RotateList = new float[2] { 0,0};

    }

    // Update is called once per frame
    
    public void DownBySet(float f, float t)
    {
        setDown = f;
        setTime = t;

    }

    public void DownByPush(float f)
    {
        pushDown = f;

    }

    public void Transform(int i)
    {
        curTransform = TransformDatas[i];
        StartTransform();
    }

    private void StartTransform()
    {
        onTransform = true;
        transformCount = 0;
        MySpriteRenderer.sprite = curTransform.S;
    }

    private void UpdateTransform(float t)
    {
        transformCount += t;
        if(transformCount>= curTransform.Time)
        {
            transformCount = curTransform.Time;
            onTransform = false;
            if (transformCallback != null)
            {
                transformCallback();
                transformCallback = null;
            }

        }
        SizeList[0] = Mathf.Lerp(curTransform.InitSize, curTransform.EndSize
            ,curTransform.SizeCurve.Evaluate(transformCount / curTransform.Time));
        RotateList[0] = Mathf.Lerp(curTransform.InitRotation, curTransform.EndRotation
            , curTransform.RotationCurve.Evaluate(transformCount / curTransform.Time));


    }
    
    public float GetSize()
    {
        float res = 0;
        foreach(float f in SizeList)
        {
            res += f;
        }
        return res;
    }

    public float GetRotate()
    {
        float res = 0;
        foreach(float f in RotateList)
        {
            res += f;
        }
        return res;
    }

    public void UpdateSizeAndRotate()
    {
        SizeList[1] = DownCurve.Evaluate(curDown);

        float temSize = GetSize();
        if (temSize < 0)
        {
            temSize = 0;
        }
        MyTransform.localScale = new Vector2(temSize, temSize);
        UF.SetRotationZ(MyTransform, GetRotate());
        pushDown = 0;
    }

    public bool Turned()
    {
        return ((index.x % 2 == 0) ^ (index.y % 2==0));
    }

    public void UpdateDown()
    {
        if (setTime > 0)
        {
            setTime -= dt;
        }
        else
        {
            setDown = 0;
        }
        toDown = (setDown > pushDown) ? setDown : pushDown;
    }
    public void UpdateCurDown()
    {
        curDown = UF.Lerp(curDown, toDown, DownSpeed, dt);
    }


    void Update()
    {
        dt = Time.deltaTime;
        if (onTransform)
        {
            UpdateTransform(dt);
        }
        if (Active)
        {
            UpdateDown();
            UpdateCurDown();
            UpdateSizeAndRotate();
        }

    }

}

[System.Serializable]
public class BackGroundUnitTransformData
{
    public float Time;
    public Sprite S;

    public float InitSize;
    public float EndSize;
    public AnimationCurve SizeCurve;
    public float InitRotation;
    public float EndRotation;
    public AnimationCurve RotationCurve;
}

