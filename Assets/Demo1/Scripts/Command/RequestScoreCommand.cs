using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestScoreCommand : EventCommand {

    [Inject] //注入的值必须get set
    public IScoreService m_scoreService { get; set; }

    [Inject]
    public ScoreModel m_scoreModel { get; set; }

    public override void Execute()
    {
        Retain();
        m_scoreService.dispatcher.AddListener(Demo1ServiceEvent.ReauestScore, OnComplete);//添加监听  请求数据结束后回调

        m_scoreService.RequestScore("http://xxx/xxx");//访问期间，防止对象销毁而出现的意外情况，retain
    }

    private void OnComplete(IEvent evt)
    {
        Debug.Log("OnComplete evt: " + evt.data);

        dispatcher.Dispatch(Demo1MediatorEvent.ScoreChange, evt.data);
        m_scoreModel.m_score = (int)evt.data;

        m_scoreService.dispatcher.RemoveListener(Demo1CommandEvent.ReauestScore, OnComplete);//移除监听
        Release(); 
         
           
    }
}
