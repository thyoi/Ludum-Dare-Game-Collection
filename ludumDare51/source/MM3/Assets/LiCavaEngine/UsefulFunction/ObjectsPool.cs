using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool<T> where T : class, IPoolableGameObject
{
    private int poolSize;
    private int top;
    private List<T> pool;

    public ObjectsPool(int size)
    {
        pool = new List<T>(size);
        poolSize = size;
        top = -1;
    }

    public bool CanGetItem()
    {
        return top >= 0;
    }

    public T GetItem()
    {
        if (CanGetItem())
        {
            pool[top].OutPool();
            return pool[top--];
        }
        else
        {
            return null;
        }
    }

    public void returnItem(T item)
    {
        if(top>= poolSize - 1)
        {
            top++;
            poolSize++;
            pool.Add(item);
        }
        else
        {
            pool[++top] = item;
        }
        item.InPool();
    }

}
