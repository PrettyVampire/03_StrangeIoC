using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateScoreCommand : EventCommand {

    [Inject]
    public ScoreModel m_scoreModel { get; set; }
    [Inject]
    public IScoreService m_scoreService { get; set; }

    public override void Execute()  
    {
        m_scoreModel.m_score++;

        m_scoreService.UpdateScore("http://xxxxxxx", m_scoreModel.m_score);

        //EventCommand有定义全局的dispatcher
        dispatcher.Dispatch(Demo1MediatorEvent.ScoreChange, m_scoreModel.m_score);    
    }   
}
