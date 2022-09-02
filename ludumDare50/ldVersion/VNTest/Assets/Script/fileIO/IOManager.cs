using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class IOManager : MonoBehaviour
{

    public TextAsset[] dataList;

    void Start()
    {
        //testFunction();
    }


    void Update()
    {
        
    }
    
    public void writeStringToFile(string path,string info, bool AppdataFolder = false)
    {
        string subPath = "";
        string fileName = path;
        //string root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        for (int i = path.Length - 1; i >= 0; i--)
        {
            if(path[i] == '/')
            {
                subPath = path.Substring(0, i);
                fileName = path.Substring(i, path.Length - i);
                break;
            }
        }
        //Debug.Log(subPath);
        if (subPath != "")
        {
            if (!Directory.Exists(Application.dataPath + '/' + subPath))
            {
                Directory.CreateDirectory(Application.dataPath + '/' + subPath);
            }
        }
        FileIO.createFileWithString(Application.dataPath + '/'+subPath  + fileName, info);
    }

    public string readStringFromFile(string path,bool AppdataFolder = false)
    {
        FileIOReturn rt = FileIO.readStringFromFile(Application.dataPath + '/' + path);
        if (rt.isExists)
        {
            return rt.stringData;
        }
        else
        {
            return "";
        }
    }

    public string readStringFromData(int n)
    {
        if (n < dataList.Length)
        {
            return dataList[n].text;
        }
        return null;
    }

    public void testFunction()
    {
        writeStringToFile("text/test.json","helloWorld");

        Debug.Log(readStringFromFile("text/test.json"));
    }
}
