using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager  {

    private static LocalizationManager m_instance = null;
    public static LocalizationManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = new LocalizationManager();
            }
            return m_instance;
        }
    }

    private const string chinese = "Locallization/Chinese";
    private const string english = "Locallization/English";
    private const string languageFile = english;
    private Dictionary<string, string> m_languageDic = new Dictionary<string, string>();

    public LocalizationManager()
    {
        ////读取文本内容存入字典  languageFile必须为完整目录
        //if (File.Exists(languageFile))
        //{
        //    string[] lines = File.ReadAllLines(languageFile);
        //    foreach (string line in lines)
        //    {
        //        if (string.IsNullOrEmpty(line)) continue;//防止文件有空行或者null
        //        string[] keyValueArr = line.Split('=');
        //        m_languageDic.Add(keyValueArr[0], keyValueArr[1]);
        //    }
        //}
        TextAsset file = Resources.Load<TextAsset>(languageFile);
        string[] lines = file.text.Split('\n');
        foreach(string line in lines)
        {
            if (string.IsNullOrEmpty(line)) continue;//防止文件有空行或者null
            string[] keyValues = line.Split('=');
            m_languageDic.Add(keyValues[0], keyValues[1]);
        }
    }

    public void Init()
    {

    }

    //key不存在返回NULL
    public string GetValue(string key)
    {
        string value;
        m_languageDic.TryGetValue(key, out value);
        Debug.Log("value = " + value);
        return value;
    }
}
