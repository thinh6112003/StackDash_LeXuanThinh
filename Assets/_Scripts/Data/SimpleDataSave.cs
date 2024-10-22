using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using Assets.SimpleZip;

public static class SimpleDataSave
{
    public static T LoadData<T>(string filePath)
    {
        T t = default;
        if (File.Exists(filePath))
        {
            byte[] text = File.ReadAllBytes(filePath);
            string json = Zip.Decompress(text);
            t = JsonHelper.FromJson<T>(json);
        }
        return t;
    }
    public static T[] LoadArrayData<T>(string filePath)
    {
        T[] t = default;
        if (File.Exists(filePath))
        {
            byte[] text = File.ReadAllBytes(filePath);
            string json = Zip.Decompress(text);
            t = JsonHelper.FromJsonArray<T>(json);
        }
        return t;
    }
    public static bool SaveData<T>(T obj, string fileName, string dictionaryPath)
    {
        if (!Directory.Exists(dictionaryPath))
        {
            Directory.CreateDirectory(dictionaryPath);
        }

        try
        {
            string json = JsonHelper.ToJson<T>(obj);
            string filePath = Path.Combine(dictionaryPath, fileName);
            File.WriteAllBytes(filePath, Zip.Compress(json));
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }
    public static bool SaveArrayData<T>(T[] obj,string fileName, string dictionaryPath)
    {
        if (!Directory.Exists(dictionaryPath))
        {
            Directory.CreateDirectory(dictionaryPath);
        }

        try
        {
            string json = JsonHelper.ToJsonArray<T>(obj);
            Debug.Log(json);
            string filePath = Path.Combine(dictionaryPath, fileName);
            File.WriteAllBytes(filePath, Zip.Compress(json));
            //File.WriteAllText(filePath, json);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }
    public static bool DeleteData<T>(string fileName,string dictionaryName)
    {
        string filePath = Path.Combine(dictionaryName, fileName);

        try
        {
            if (!File.Exists(filePath))
            {
                return true;
            }
            else
            {
                FileInfo fileInfo = new FileInfo(filePath);
                fileInfo.Delete();
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }
}
