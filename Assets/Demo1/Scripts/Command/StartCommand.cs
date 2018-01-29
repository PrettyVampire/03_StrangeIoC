using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有绑定后的一些初始化工作
/// </summary>
public class StartCommand : Command {

    [Inject]
    public AudioManager audioManager { get; set; }
    /// <summary>
    /// 命令执行的时候默认调用Excute方法
    /// </summary>
    public override void Execute()
    {
        audioManager.Init();
        PoolManager.Instance.Init();
    }
}
