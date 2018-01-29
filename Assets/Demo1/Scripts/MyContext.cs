using strange.extensions.context.api;
using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 启动MVCS的框架，各种绑定
/// </summary>
public class MyContext : MVCSContext
{
    public MyContext(MonoBehaviour view):base(view)
    {

    }

    /// <summary>
    /// 绑定映射
    /// </summary>
    protected override void mapBindings()
    {
        //manager
        injectionBinder.Bind<AudioManager>().To<AudioManager>().ToSingleton();

        //model
        injectionBinder.Bind<ScoreModel>().To<ScoreModel>().ToSingleton();

        //serivce  ToSingleton表示这个对象在工程中只生成一次
        injectionBinder.Bind<IScoreService>().To<ScoreService>().ToSingleton();

        //command
        commandBinder.Bind(Demo1CommandEvent.ReauestScore).To<RequestScoreCommand>();
        commandBinder.Bind(Demo1CommandEvent.UpdateScore).To<UpdateScoreCommand>();

        //mediator
        mediationBinder.Bind<CubeView>().To<CubeMediator>(); //完成view和mediator的绑定

        //绑定开始事件  只触发一次
        commandBinder.Bind(ContextEvent.START).To<StartCommand>().Once();
    }
}
