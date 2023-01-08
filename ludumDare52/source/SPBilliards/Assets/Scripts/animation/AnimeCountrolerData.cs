using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnimeCountrolerData
{
    public AnimeFloatProperty positionX;
    public AnimeFloatProperty positionY;
    public AnimeFloatProperty ScaleX;
    public AnimeFloatProperty ScaleY;
    public AnimeFloatProperty Apha;
    public AnimeFloatProperty RotateZ;
    public int CallBackProperty;

    public AnimeCountrolerData()
    {
        positionX = new AnimeFloatProperty();
        positionY = new AnimeFloatProperty();
        ScaleX = new AnimeFloatProperty();
        ScaleY = new AnimeFloatProperty();
        Apha = new AnimeFloatProperty();
        RotateZ = new AnimeFloatProperty();
    }
    public void Copy(AnimeCountrolerData a)
    {
        positionX.Copy(a.positionX);
        positionY.Copy(a.positionY);
        ScaleX.Copy(a.ScaleX);
        ScaleY.Copy(a.ScaleY);
        Apha.Copy(a.Apha);
        RotateZ.Copy(a.RotateZ);
        CallBackProperty = a.CallBackProperty;
    }

}
