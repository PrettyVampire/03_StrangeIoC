using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 视图显示、逻辑
/// </summary>
public class CubeView : View {
    [Inject]
    public IEventDispatcher dispatcher { get; set; }

    //注入的方式比较耗性能，频繁调用这个对象建议使用单例
    [Inject]
    public AudioManager audioManager { get; set; }

    private Text m_scoreText;
    private Transform m_bulletParent;//子弹父节点

    public void Init()
    {
        m_scoreText = GetComponentInChildren<Text>();
    }
    
    private void Awake()
    {
        //m_bulletParent = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        m_bulletParent = new GameObject().transform;
        m_bulletParent.name = "BulletNode";

    }

    private void Update()
    {
        transform.Translate(new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), Random.Range(-1, 2)) * Time.deltaTime * 5);
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = PoolManager.Instance.GetObjectFromPool("Sphere");
            obj.transform.SetParent(m_bulletParent, false);
        }
    }

    private void OnMouseDown()
    {
        audioManager.Play("hit", Vector3.zero);
        dispatcher.Dispatch(Demo1MediatorEvent.ScoreUpdate);
    }

    public void UpdateScore(int score)
    {
        m_scoreText.text = score.ToString();
    }
    
}
