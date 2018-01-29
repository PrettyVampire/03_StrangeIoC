using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 巡逻状态类
/// </summary>
public class PatrolState : FSMState
{
    //巡逻的范围
    private int m_pointIndex;
    private Transform[] m_pointArr;
    private GameObject m_NPC;
    private Rigidbody m_rigidbody;
    private GameObject m_player;
    public PatrolState(Transform[] points, GameObject npc, GameObject player)
    {
        m_stateID = StateID.Patrol;
        m_pointArr = points;
        m_NPC = npc;
        m_pointIndex = 0;
        m_rigidbody = m_NPC.GetComponent<Rigidbody>();
        m_player = player;
    }

    public override void DoBeforEntering()
    {
        Debug.Log("Entering state: " + m_stateID);
    }

    public override void DoBeforLeaving()
    {
        Debug.Log("Leaving state: " + m_stateID);

    }

    public override void DoUpdate()
    {
        PatrolMove();
        CheckTransition();
    }

    private void CheckTransition()
    {
        if (Mathf.Abs(Vector3.Distance(m_player.transform.position, m_NPC.transform.position)) < 5)
        {
            FSMStateManager.Instance.PerformTransition(Transition.SawPlayer);
        }
    }

    private void PatrolMove()
    {
        m_rigidbody.velocity = m_NPC.transform.forward * 3;
        Vector3 targetPos = m_pointArr[m_pointIndex].position;
       // targetPos.y = m_NPC.transform.position.y;
        m_NPC.transform.LookAt(targetPos);
        if (Mathf.Abs(Vector3.Distance(targetPos, m_NPC.transform.position)) < 1)
        {
            m_pointIndex++;
            m_pointIndex %= m_pointArr.Length;
        }


    }
}
