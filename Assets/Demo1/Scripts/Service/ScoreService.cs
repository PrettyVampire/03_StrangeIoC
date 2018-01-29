using System.Collections;
using System.Collections.Generic;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

public class ScoreService : IScoreService
{
    [Inject]
    public IEventDispatcher dispatcher { get; set; }

    public void RequestScore(string url)
    {
        Debug.Log("request score frome url: " + url);
        OnReceiveScore();
    }

    public void OnReceiveScore()
    {
        int score = Random.Range(0, 100);
        dispatcher.Dispatch(Demo1ServiceEvent.ReauestScore, score);
    }
    
    public void UpdateScore(string url, int score)
    {
        Debug.Log("url: " + url + " score: " + score);
    }
}
