using UnityEngine;
using UnityEngine.Playables;

public class TimelineStartTrigger : Trigger
{
    public PlayableDirector myDirector;

    public override void ActiveTrigger()
    {
        myDirector.Play();
    }
}