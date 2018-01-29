using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//状态转换的条件
public enum Transition
{
    NullTransition = 0,
    SawPlayer,//看到主角
    LostPlayer,//跟丢主角
}

//状态id
public enum StateID
{
    NullStateID = 0,
    Patrol,//巡逻
    Chase,//追主角
}

public abstract class FSMState
{
    protected StateID m_stateID; 
    public StateID stateID
    {
        get
        {
            return m_stateID;
        }
    }

    protected Dictionary<Transition, StateID> m_map = new Dictionary<Transition, StateID>();

    //添加状态
    public void AddTransition(Transition trans, StateID id)
    {
        if (m_map.ContainsKey(trans))
        {
            Debug.LogError(trans + "is added");
            return;
        }

        m_map.Add(trans, id);
    }

    //移除状态
    public void RemoveTransition(Transition trans)
    {
        if (m_map.ContainsKey(trans))
        {
            m_map.Remove(trans);
        }
        Debug.LogWarning(trans + "is not exit in map");
    }

    //根据条件  获取状态
    public StateID GetOutputState(Transition trans)
    {
        if (m_map.ContainsKey(trans))
        {
            return m_map[trans];
        }
        return StateID.NullStateID;
    }

    //进入游戏当前状态，需要做的事
    public virtual void DoBeforEntering() { }
    public virtual void DoBeforLeaving() { }
    public abstract void DoUpdate();
}
