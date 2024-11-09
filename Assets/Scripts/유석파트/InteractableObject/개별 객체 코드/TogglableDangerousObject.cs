using UnityEngine;

public class TogglableDangerousObject : DangerousObject
{
    private Animator myAnimator;
    
    protected virtual void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public override void OnKeyInserted()
    {
        myAnimator.SetBool("isInactive", false);

        base.OnKeyInserted();
    }
}
