using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对接视图的数据
/// </summary>
public class CubeMediator : Mediator
{
    [Inject]//注入 自动给cubeView赋值
    public CubeView cubeView { get; set; }

    [Inject(ContextKeys.CONTEXT_DISPATCHER)] //得到全局的despatcher，因为绑定命令时候是全局绑定
    public IEventDispatcher dispatcher { get; set; }

    
    //当绑定、注入完成后
    public override void OnRegister()
    {
        cubeView.Init();
        
        dispatcher.AddListener(Demo1MediatorEvent.ScoreChange, OnScoreChange);
        cubeView.dispatcher.AddListener(Demo1MediatorEvent.ScoreUpdate, OnScoreUpdate);
        //通过dispatcher发起请求分数的命令
        dispatcher.Dispatch(Demo1CommandEvent.ReauestScore);
    }

    //游戏物体被销毁后
    public override void OnRemove()
    {
        dispatcher.RemoveListener(Demo1MediatorEvent.ScoreChange, OnScoreChange);
        cubeView.dispatcher.RemoveListener(Demo1MediatorEvent.ScoreUpdate, OnScoreUpdate);

    }

    //更新分数
    public void OnScoreChange(IEvent evt)
    {
        cubeView.UpdateScore((int)evt.data);
    }

    public void OnScoreUpdate()
    {
        dispatcher.Dispatch(Demo1CommandEvent.UpdateScore);
    }
}
