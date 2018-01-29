using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class AudioWindowEditor : EditorWindow {
    [MenuItem("Manager/AudioManager")]
    static void CreateWindow()
    {
        //创建窗口
        //Rect wr = new Rect(0, 0, 500, 500);
        //参数（对象、区域、是否置顶、窗口名称）
        //AudioWindowEditor window = (AudioWindowEditor)EditorWindow.GetWindowWithRect(typeof(AudioWindowEditor), wr, true, "音效编辑");//窗口大小 固定
        AudioWindowEditor window = EditorWindow.GetWindow<AudioWindowEditor>("音效编辑");
        window.Show();
    }

    //输入文字的内容
    private string m_audioName;
    private string m_audioPath;
    private string m_savePath;
    private Dictionary<string, string> m_audioDict = new Dictionary<string, string>();
    //选择贴图的对象
    private Texture m_texture;

    public void Awake()
    {
        Debug.Log("Awake");
        //在资源中读取一张贴图
        m_texture = Resources.Load("iocn_ho") as Texture;
    }

    //绘制窗口时调用
    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("名字");
        GUILayout.Label("路径");
        GUILayout.Label("操作");
        GUILayout.EndHorizontal();
        //显示key value  删除按钮
        foreach (string key in m_audioDict.Keys)
        {
            string value;
            m_audioDict.TryGetValue(key, out value);
            GUILayout.BeginHorizontal();
            GUILayout.Label(key);
            GUILayout.Label(value);
            if (GUILayout.Button("删除"))
            {
                m_audioDict.Remove(key);
                SaveAudioList();
                return;
            }
            GUILayout.EndHorizontal();

        }

        m_audioName = EditorGUILayout.TextField("音效名字:", m_audioName);
        m_audioPath = EditorGUILayout.TextField("音效路径:", m_audioPath);
        if (GUILayout.Button("添加音效"))
        {
            object obj = Resources.Load(m_audioPath);
            if(obj == null)
            {
                Debug.LogWarning("路径m_audioPath：" + m_audioPath + "不存在");
                m_audioPath = "";
            }
            else
            {
                if (!m_audioDict.ContainsKey(m_audioName))
                {
                    m_audioDict.Add(m_audioName, m_audioPath);
                    this.SaveAudioList();
                }
                else
                {
                    Debug.LogWarning("音效名称重复");
                }
            }
        }

        if (GUILayout.Button("打开通知", GUILayout.Width(200)))
        {
            //打开一个通知栏
            this.ShowNotification(new GUIContent("This is a Notification"));
        }

        if (GUILayout.Button("关闭通知", GUILayout.Width(200)))
        {
            //关闭通知栏
            this.RemoveNotification();
        }

        //文本框显示鼠标在窗口的位置
        EditorGUILayout.LabelField("鼠标在窗口的位置", Event.current.mousePosition.ToString());

        //选择贴图
        m_texture = EditorGUILayout.ObjectField("添加贴图", m_texture, typeof(Texture), true) as Texture;

        if (GUILayout.Button("关闭窗口", GUILayout.Width(200)))
        {
            //关闭窗口
            this.Close();
        }
    }

    #region Save and Load 音效数据
    public void SaveAudioList()
    {
        StringBuilder info = new StringBuilder();
        foreach (string key in m_audioDict.Keys)
        {
            string value;
            m_audioDict.TryGetValue(key, out value);
            info.Append(key + "," + value + "\n");
        }
        Debug.Log("info = " + info.ToString());
        
        File.WriteAllText(AudioManager.AudioTextPath, info.ToString());//覆盖
        //File.AppendAllText(m_savePath, info.ToString());//追加
    }

    public void LoadAudioList()
    {
        m_audioDict = new Dictionary<string, string>();
        if (File.Exists(AudioManager.AudioTextPath))
        {
            string[] lines = File.ReadAllLines(AudioManager.AudioTextPath);
            foreach(string line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                string[] keyValueArr = line.Split(',');
                string key = keyValueArr[0];
                string value = keyValueArr[1];
                m_audioDict.Add(key, value);
            }
        }
    }
    #endregion

    //更新
    void Update()
    {

    }

    void OnFocus()
    {
        Debug.Log("当窗口获得焦点时调用一次");
        
    }

    void OnLostFocus()
    {
        Debug.Log("当窗口丢失焦点时调用一次");
    }

    void OnHierarchyChange()
    {
        Debug.Log("当Hierarchy视图中的任何对象发生改变时调用一次");
    }

    void OnProjectChange()
    {
        Debug.Log("当Project视图中的资源发生改变时调用一次");
    }

    //这里开启窗口的重绘，不然窗口信息不会刷新 10次/s
    void OnInspectorUpdate()
    {
       // Debug.Log("窗口面板更新");
       // this.Repaint();
        this.LoadAudioList();
    }

    void OnSelectionChange()
    {
        //当窗口出去开启状态，并且在Hierarchy视图中选择某游戏对象时调用
        foreach (Transform t in Selection.transforms)
        {
            //有可能是多选，这里开启一个循环打印选中游戏对象的名称
            Debug.Log("OnSelectionChange" + t.name);
        }
    }

    void OnDestroy()
    {
        Debug.Log("当窗口关闭时调用");
    }
}
