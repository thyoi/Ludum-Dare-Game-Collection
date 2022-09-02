using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textReader : MonoBehaviour
{
    public string content;
    public textManager tm;
    private int count;
    public float interTime;
    private float timeCount;
    public bool start;
    private float dt;
    void Start()
    {
        //readText("aaaaaaaaa]bbbbbbb]asdasdasdasd");
    }

    public void readText(string s)
    {
        content = s;
        //content.Replace('\n', ']');
        start = true;
        tm.setContent("", true);
        count = 0;
        timeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        if (start)
        {
            timeCount += dt;
            if(timeCount>= interTime)
            {
                timeCount = 0;
                count++;
                if(count >= content.Length)
                {
                    count = content.Length;
                    start = false;
                }
                tm.setContent(content.Substring(0, count));
            }
        }
    }

    public void end()
    {
        timeCount = interTime;
        tm.setContent(content.Substring(0, count));
        start = false;
    }
}
