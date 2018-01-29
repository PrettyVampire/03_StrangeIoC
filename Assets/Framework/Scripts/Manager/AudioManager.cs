using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioManager
{
    private bool m_bMute = false;
    private static string m_audioTextPathPrefix = Application.dataPath + "/FrameWork/Resources/";//const有毛病？
    private const string m_audioTextPathPostfix = "Audiolist.txt"; 

    public static string AudioTextPath
    {
        get
        {
            return m_audioTextPathPrefix + m_audioTextPathPostfix;
        }
    }

    //存放音效的字典
    private Dictionary<string, AudioClip> m_audioClipDict = new Dictionary<string, AudioClip>();

    public AudioManager()
    {
        
    }

    public void Init()
    {
        LoadAudioClip();
    }

    private void LoadAudioClip()
    {
        m_audioClipDict = new Dictionary<string, AudioClip>();
        if (File.Exists(AudioTextPath))//目录必须为全目录
        {
            string[] lines = File.ReadAllLines(AudioTextPath);
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;//防止文件有空行或者null
                string[] keyValueArr = line.Split(',');
                string key = keyValueArr[0];
                AudioClip value = Resources.Load<AudioClip>(keyValueArr[1]);
                m_audioClipDict.Add(key, value);
            }
        }
    }

    public void Play(string name)
    {
        if (m_bMute) return;
        AudioClip audioClip;
        m_audioClipDict.TryGetValue(name, out audioClip);

        if (audioClip)
        {
            AudioSource.PlayClipAtPoint(audioClip, Vector3.zero);
        }
    }

    public void Play(string name, Vector3 pos)
    {
        if (m_bMute) return;

        AudioClip audioClip;
        m_audioClipDict.TryGetValue(name, out audioClip);

        if (audioClip)
        {
            AudioSource.PlayClipAtPoint(audioClip, pos);
        }
    }


}
