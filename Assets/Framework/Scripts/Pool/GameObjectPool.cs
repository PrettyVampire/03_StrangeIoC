using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameObjectPool {
    [SerializeField]
    private string m_name;
    [SerializeField]
    private GameObject m_perfab;
    [SerializeField]
    private int m_maxAmount;

    public string name
    {
        get
        {
            return m_name;
        }
    }
    public GameObject gameobject
    {
        get
        {
            return m_perfab;
        }
    }
    public int maxAmount
    {
        get
        {
            return m_maxAmount;
        }
    }

    //
    [NonSerialized]
    private List<GameObject> m_gameObjList = new List<GameObject>();
    public List<GameObject> gameObjList
    {
        get
        {
            if(m_gameObjList == null)
            {
                m_gameObjList = new List<GameObject>();
            }
            return m_gameObjList;
        }
    }

    //如果要优化，可以在创建一个list用来存放Active为false的物体，需要的时候直接获取就不用遍历了
    public GameObject GetObject()
    {
        foreach(GameObject obj in m_gameObjList)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        if(m_gameObjList.Count >= m_maxAmount)
        {
            GameObject.Destroy(m_gameObjList[0]);
            m_gameObjList.RemoveAt(0);
        }

        GameObject prefab = GameObject.Instantiate(m_perfab, new Vector3(0, 3, 0), Quaternion.identity);
        m_gameObjList.Add(prefab);
        return prefab;
    }
}
