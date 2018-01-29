using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态机管理类
/// </summary>
public class FSMStateManager
{
    private static FSMStateManager m_instance = null;
    public static FSMStateManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = new FSMStateManager();
            }
            return m_instance;
        }
    }

    private Dictionary<StateID, FSMState> m_stateDic;
    private FSMState m_currentState;
    public FSMState currentState
    {
        get
        {
            return m_currentState;
        }
    }

    public FSMStateManager()
    {
        m_stateDic = new Dictionary<StateID, FSMState>();
    }

    //添加状态机
    public void AddState(FSMState state)
    {
        if (m_stateDic.ContainsKey(state.stateID))
        {
            Debug.LogError(state.stateID + "has been added");
        }
        m_stateDic.Add(state.stateID, state);
    }

    //移除状态机
    public void RemoveState(FSMState state)
    {
        if (m_stateDic.ContainsKey(state.stateID))
        {
            m_stateDic.Remove(state.stateID);
        }

        Debug.LogError(state.stateID + "is not exit");
    }

    //控制状态之间的转换
    public void PerformTransition(Transition trans)
    {
        if(trans == Transition.NullTransition)
        {
            Debug.LogError("PreformTransition()  transition is null");
        }
        StateID id = m_currentState.GetOutputState(trans);
        if(id == StateID.NullStateID)
        {
            Debug.Log("没有符合条件的转换 transition = " + trans);
        }

        FSMState state;
        m_stateDic.TryGetValue(id, out state);
        m_currentState.DoBeforLeaving();
        m_currentState = state;
        m_currentState.DoBeforEntering();
    }

    //启动状态机
    public void Start(StateID id)
    {
        FSMState state;
        if(m_stateDic.TryGetValue(id, out state))
        {
            m_currentState = state;
            m_currentState.DoBeforEntering();
        }
        else
        {
            Debug.LogError("失败，启动状态不存在 stateID = " + id);
        }
    }
}
