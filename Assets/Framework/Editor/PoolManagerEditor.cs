using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PoolManagerEditor
{
    [MenuItem("Manager/Create GameObjectPoolConfig")]
    static void GameObjectPoolList()
    {
        GameObjectPoolList poolList = ScriptableObject.CreateInstance<GameObjectPoolList>();

        //创建类为可编辑的配置文件
        AssetDatabase.CreateAsset(poolList, PoolManager.PoolConfigPath);
        AssetDatabase.SaveAssets();
    }
}
