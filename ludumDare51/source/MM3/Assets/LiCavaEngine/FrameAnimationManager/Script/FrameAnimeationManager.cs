using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameAnimeationManager : MonoBehaviour
{
    static FrameAnimeationManager mainManager;
    static public void Register(FrameAnimation item)
    {
        mainManager.RegisterItem(item);
    }

    static public void Delete(FrameAnimation item)
    {
        mainManager.DeleteItem(item);
    }

    public int timeLayer = 3;

    private float dt;
    private Dictionary<float, FrameAnimationCollection> itemCollectionDic;

    public void RegisterItem(FrameAnimation item)
    {
        if (itemCollectionDic.ContainsKey(item.interTime))
        {
            itemCollectionDic[item.interTime].Add(item);
        }
        else
        {
            itemCollectionDic[item.interTime] = new FrameAnimationCollection(item.interTime);
            itemCollectionDic[item.interTime].Add(item);
        }
    }

    public void DeleteItem(FrameAnimation item)
    {
        if (itemCollectionDic.ContainsKey(item.interTime))
        {
            itemCollectionDic[item.interTime].Del(item);
        }
    }

    public void Awake()
    {
        if(!(mainManager is null))
        {
            Debug.LogError("Repetitive FrameAnimationManager");
        }
        mainManager = this;
        itemCollectionDic = new Dictionary<float, FrameAnimationCollection>();
    }

    // Update is called once per frame
    void Update()
    {
        dt = TimeManager.DeltaTime(timeLayer);
        if (dt > 0)
        {
            foreach (FrameAnimationCollection f in itemCollectionDic.Values)
            {
                f.Update(dt);
            }
        }
    }
}

public class FrameAnimationCollection
{
    public float interTime;
    public float count;
    public int idCounter;
    private List<FrameAnimation> updateList;

    public FrameAnimationCollection(float interTime)
    {
        this.interTime = interTime;
        updateList = new List<FrameAnimation>();
        count = 0;
        idCounter = 0;
    }
    public void Add(FrameAnimation item)
    {
        item.id = idCounter++;
        updateList.Add(item);
    }

    public void Del(FrameAnimation item)
    {
        for(int i = 0; i < updateList.Count; i++)
        {
            if(updateList[i].id == item.id)
            {
                updateList.RemoveAt(i);
                break;
            }
        }
    }

    public void Update(float dt)
    {
        count += dt;
        while (count >= interTime)
        {
            foreach(FrameAnimation f in updateList)
            {
                f.NextFrame();
            }
            count -= interTime;
        }
    }

}
