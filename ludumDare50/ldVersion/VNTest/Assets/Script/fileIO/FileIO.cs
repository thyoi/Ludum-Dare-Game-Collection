using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class FileIO
{

    public static FileIOReturn createFileWithString(string path, string info)
    {
        return (createFileWithBytes(path, new UTF8Encoding().GetBytes(info)));
    }


    public static FileIOReturn createFileWithBytes(string path, byte[] info)
    {
        FileIOReturn rt = new FileIOReturn();
        rt.isExists = File.Exists(path);

        FileStream F = new FileStream(path, FileMode.Create, FileAccess.Write);
        F.Write(info, 0, info.Length);
        F.Close();

        return rt;
    }

    public static FileIOReturn readBytesFromFile(string path)
    {
        FileIOReturn rt = new FileIOReturn();
        rt.isExists = File.Exists(path);
        if (rt.isExists)
        {
            FileStream F = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] res = new byte[F.Length+2];
            if (F.Length < int.MaxValue)
            {
                F.Read(res, 0, (int)F.Length);
            }
            F.Close();
            rt.bytesData = res;
        }
        return rt;
    }


    public static FileIOReturn readStringFromFile(string path)
    {
        FileIOReturn rt = readBytesFromFile(path);
        if (rt.isExists)
        {
            rt.stringData = new UTF8Encoding().GetString(rt.bytesData);
        }
        return rt;
    }

}

public class FileIOReturn
{
    public bool isExists;
    public byte[] bytesData;
    public string stringData;
}
