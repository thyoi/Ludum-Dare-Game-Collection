using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolableGameObject
{
    public void InPool();
    public void OutPool();
}
