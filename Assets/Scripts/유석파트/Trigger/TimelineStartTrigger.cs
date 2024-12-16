using Mirror;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineStartTrigger : Trigger
{
    public bool isForBlind;

    public PlayableDirector myDirector;

    public override void ActiveTrigger()
    {
        if(NetworkClient.localPlayer.GetComponent<PlayerObjectController>().Role == PlayerObjectController.Blind)
        {
            // 장님일 때
            if(!isForBlind) return;
        }
        else
        {
            // 절름발이일 때
            if(isForBlind) return;
        }

        myDirector.Play();
    }
}