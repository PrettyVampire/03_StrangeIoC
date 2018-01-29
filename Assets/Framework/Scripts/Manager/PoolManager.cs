using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager {

    private static PoolManager m_instance = null;
    public static PoolManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = new PoolManager();
            }
            return m_instance;
        }
    }

    //文件路径
    private const string m_pathPrefix = "Assets/FrameWork/Resources/";
    private const string m_fileName = "Gameobjectpool";
    private const string m_pathPostfix = ".asset";
    public static string PoolConfigPath
    {
        get
        {
            return m_pathPrefix + m_fileName + m_pathPostfix;
        }
    }
    //字典  根据name查找对应的pool
    private Dictionary<string, GameObjectPool> m_poolDic;

    //构造函数
    PoolManager()
    {
        GameObjectPoolList poolList = Resources.Load<GameObjectPoolList>(m_fileName);//获取文件类

        m_poolDic = new Dictionary<string, GameObjectPool>();
        foreach(GameObjectPool pool in poolList.poolList)
        {
            m_poolDic.Add(pool.name, pool);
        }
    }

    public void Init()
    {
        //do nothing
    }

    public GameObject GetObjectFromPool(string poolName)
    {
        GameObjectPool pool;
        if(m_poolDic.TryGetValue(poolName, out pool))
        {
            return pool.GetObject();
        }
        else
        {
            Debug.LogWarning("poolName: " + poolName + "is not exit");
            return null;
        }
    }
}
