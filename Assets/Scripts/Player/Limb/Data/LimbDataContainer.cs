using Mirror;
using UnityEngine;

public class LimbDataContainer : NetworkBehaviour
{
    public LimbData limbData;

    [SyncVar(hook = nameof(SetmovementVelocity))] public float movementVelocity;
    [SyncVar(hook = nameof(Setbulletspeed))] public float bulletspeed;
    [SyncVar(hook = nameof(SetgroundCheckRadious))] public float groundCheckRadious;
    [SyncVar(hook = nameof(SetwhatIsGround))] public LayerMask whatIsGround;
    [SyncVar(hook = nameof(SetwhatIsBlind))] public LayerMask whatIsBlind;
    [SyncVar(hook = nameof(SetisRiding))] public bool isRiding;
    [SyncVar(hook = nameof(SetishavingGun))] public bool ishavingGun;
    [SyncVar(hook = nameof(SetHoldingGun))] public bool HoldingGun;
    [SyncVar(hook = nameof(SetshotDelay))] public float ShotDelay;
    [SyncVar(hook = nameof(SetNormInputX))] public int NormInputX;
    [SyncVar(hook = nameof(SetNormInputY))] public int NormInputY;
    [SyncVar(hook = nameof(SetJumpInput))] public bool JumpInput;
    [SyncVar(hook = nameof(SetSitInput))] public bool SitInput;
    [SyncVar(hook = nameof(SetattackInput))] public bool attackInput;
    [SyncVar(hook = nameof(SetmousePosition))] public Vector3 mousePosition;
    private void Update()
    {
        if (!isLocalPlayer || !isOwned) return;

        if (movementVelocity != limbData.movementVelocity)
        {
            CmdSetmovementVelocity(limbData.movementVelocity);
        }
        if (bulletspeed != limbData.bulletspeed)
        {
            CmdSetbulletspeed(limbData.bulletspeed);
        }
        if (groundCheckRadious != limbData.groundCheckRadious)
        {
            CmdSetgroundCheckRadious(limbData.groundCheckRadious);
        }
        if (whatIsGround != limbData.whatIsGround)
        {
            CmdSetwhatIsGround(limbData.whatIsGround);
        }
        if (whatIsBlind != limbData.whatIsBlind)
        {
            CmdSetwhatIsBlind(limbData.whatIsBlind);
        }
        if (isRiding != limbData.isRiding)
        {
            CmdSetisRiding(limbData.isRiding);
        }
        if (ishavingGun != limbData.ishavingGun)
        {
            CmdSetishavingGun(limbData.ishavingGun);
        }
        if (HoldingGun != limbData.HoldingGun)
        {
            CmdSetHoldingGun(limbData.HoldingGun);
        }
        if (ShotDelay != limbData.ShotDelay)
        {
            CmdSetshotDelay(limbData.ShotDelay);
        }

        if (NormInputX != limbData.NormInputX)
        {
            CmdSetNormInputX(limbData.NormInputX);
        }

        if (NormInputY != limbData.NormInputY)
        {
            CmdSetNormInputY(limbData.NormInputY);
        }

        if (JumpInput != limbData.JumpInput)
        {
            CmdSetJumpInput(limbData.JumpInput);
        }

        if (SitInput != limbData.SitInput)
        {
            CmdSetSitInput(limbData.SitInput);
        }

        if (attackInput != limbData.attackInput)
        {
            CmdSetattackInput(limbData.attackInput);
        }

        if (mousePosition != limbData.mousePosition)
        {
            CmdSetmousePosition(limbData.mousePosition);
        }
    }
    #region Setfunction
    void SetmovementVelocity(float oldvalue, float newvalue)
    {
        limbData.movementVelocity = newvalue;
    }
    void Setbulletspeed(float oldvalue, float newvalue)
    {
        limbData.bulletspeed = newvalue;
    }
    void SetgroundCheckRadious(float oldvalue, float newvalue)
    {
        limbData.groundCheckRadious = newvalue;
    }
    void SetwhatIsGround(LayerMask oldvalue, LayerMask newvalue)
    {
        limbData.whatIsGround = newvalue;
    }
    void SetwhatIsBlind(LayerMask oldvalue, LayerMask newvalue)
    {
        limbData.whatIsBlind = newvalue;
    }

    void SetisRiding(bool oldvalue, bool newvalue)
    {
        limbData.isRiding = newvalue;
    }
    void SetishavingGun(bool oldvalue, bool newvalue)
    {
        limbData.ishavingGun = newvalue;
    }
    void SetHoldingGun(bool oldvalue, bool newvalue)
    {
        limbData.HoldingGun = newvalue;
    }
    void SetshotDelay(float oldvalue, float newvalue)
    {
        limbData.ShotDelay = newvalue;
    }

    void SetNormInputX(int oldvalue, int newvalue)
    {
        limbData.NormInputX = newvalue;
    }

    void SetNormInputY(int oldvalue, int newvalue)
    {
        limbData.NormInputY = newvalue;
    }

    void SetJumpInput(bool oldvalue, bool newvalue)
    {
        limbData.JumpInput = newvalue;
    }

    void SetSitInput(bool oldvalue, bool newvalue)
    {
        limbData.SitInput = newvalue;
    }

    void SetattackInput(bool oldvalue, bool newvalue)
    {
        limbData.attackInput = newvalue;
    }

    void SetmousePosition(Vector3 oldvalue, Vector3 newvalue)
    {
        limbData.mousePosition = newvalue;
    }
    #endregion
    #region CmdSetfunction
    [Command]
    void CmdSetmovementVelocity(float newvalue)
    {
        movementVelocity = newvalue;
    }

    [Command]
    void CmdSetbulletspeed(float newvalue)
    {
        bulletspeed = newvalue;
    }

    [Command]
    void CmdSetgroundCheckRadious(float newvalue)
    {
        groundCheckRadious = newvalue;
    }

    [Command]
    void CmdSetwhatIsGround(LayerMask newvalue)
    {
        whatIsGround = newvalue;
    }

    [Command]
    void CmdSetwhatIsBlind(LayerMask newvalue)
    {
        whatIsBlind = newvalue;
    }


    [Command]
    void CmdSetisRiding(bool newvalue)
    {
        isRiding = newvalue;
    }

    [Command]
    void CmdSetishavingGun(bool newvalue)
    {
        ishavingGun = newvalue;
    }

    [Command]
    void CmdSetHoldingGun(bool newvalue)
    {
        HoldingGun = newvalue;
    }

    [Command]
    void CmdSetshotDelay(float newvalue)
    {
        ShotDelay = newvalue;
    }

    [Command]
    void CmdSetNormInputX(int newvalue)
    {
        NormInputX = newvalue;
    }

    [Command]
    void CmdSetNormInputY(int newvalue)
    {
        NormInputY = newvalue;
    }

    [Command]
    void CmdSetJumpInput(bool newvalue)
    {
        JumpInput = newvalue;
    }

    [Command]
    void CmdSetSitInput(bool newvalue)
    {
        SitInput = newvalue;
    }

    [Command]
    void CmdSetattackInput(bool newvalue)
    {
        attackInput = newvalue;
    }

    [Command]
    void CmdSetmousePosition(Vector3 newvalue)
    {
        mousePosition = newvalue;
    }
    #endregion
}
