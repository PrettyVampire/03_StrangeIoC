using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂在物体上 strangeioc框架启动入口
/// </summary>
public class MyRoot : ContextView
{
    private void Awake()
    {
        this.context = new MyContext(this);
    }
}
