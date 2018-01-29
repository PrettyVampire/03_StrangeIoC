using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理所有的资源池  继承ScriptableObject变成可自定义的资源配置文件
/// </summary>
public class GameObjectPoolList: ScriptableObject
{
    [SerializeField]
    private List<GameObjectPool> m_poolList = new List<GameObjectPool>();
    public List<GameObjectPool> poolList
    {
        get
        {
            if(m_poolList == null)
            {
                m_poolList = new List<GameObjectPool>();
            }
            return m_poolList;
        }
    }
}
