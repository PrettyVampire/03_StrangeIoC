using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 追击主角类
/// </summary>
public class ChaseState : FSMState
{
    private GameObject m_NPC;
    private Rigidbody m_rigidbody;
    private GameObject m_player;

    public ChaseState(GameObject npc, GameObject player)
    {
        m_stateID = StateID.Chase;
        m_NPC = npc;
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
        ChaseMove();
        CheckTransition();
    }

    private void CheckTransition()
    {
        if (Mathf.Abs(Vector3.Distance(m_player.transform.position, m_NPC.transform.position)) > 10)
        {
            FSMStateManager.Instance.PerformTransition(Transition.LostPlayer);
        }
    }

    private void ChaseMove()
    {
        m_rigidbody.velocity = m_NPC.transform.forward * 5;
        Vector3 targetPos = m_player.transform.position;
        targetPos.y = m_NPC.transform.position.y;
        m_NPC.transform.LookAt(targetPos);
    }
}
