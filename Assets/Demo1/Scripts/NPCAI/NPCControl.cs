using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControl : MonoBehaviour {
    //npc的巡逻点
    public Transform[] m_pointArr;
    public GameObject m_player;
  //  private FSMStateManager m_fsm;
    void Start ()
    {
        InitFSM();
	}
	
    /// <summary>
    /// 初始化状态机
    /// </summary>
	void InitFSM()
    {
        FSMStateManager fsm = FSMStateManager.Instance;

        PatrolState patrolstate = new PatrolState(m_pointArr, this.gameObject, m_player);
        patrolstate.AddTransition(Transition.SawPlayer, StateID.Chase);

        ChaseState chasestate = new ChaseState(this.gameObject, m_player);
        chasestate.AddTransition(Transition.LostPlayer, StateID.Patrol);

        fsm.AddState(patrolstate);
        fsm.AddState(chasestate);

        fsm.Start(patrolstate.stateID);

    }

    private void Update()
    {
        FSMStateManager.Instance.currentState.DoUpdate();
    }
}
