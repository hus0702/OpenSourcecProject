using Mirror;
using UnityEngine;

public class LimbDataContainer : NetworkBehaviour
{
    public LimbData limbData;

    [SyncVar] public float movementVelocity;
    [SyncVar] public float bulletspeed;
    [SyncVar] public float groundCheckRadious; 
    [SyncVar] public LayerMask whatIsGround;
    [SyncVar] public LayerMask whatIsBlind;
    [SyncVar] public int FacingDirection;
    [SyncVar] public Vector3 position;
    [SyncVar] public bool isRiding;
    [SyncVar] public bool ishavingGun;
    [SyncVar] public bool HoldingGun;
    [SyncVar] public float ShotDelay;
    [SyncVar] public int NormInputX;
    [SyncVar] public int NormInputY;
    [SyncVar] public bool JumpInput;
    [SyncVar] public bool SitInput;
    [SyncVar] public bool attackInput;
    [SyncVar] public Vector3 mousePosition;

    private void Awake()
    {
        movementVelocity = 3f;
        bulletspeed = 20f;
        groundCheckRadious = 0.5f;
        FacingDirection = 1;
        isRiding = false;
        ishavingGun = false;
        HoldingGun = false;
        ShotDelay = 0.3f;
        NormInputX = 0;
        NormInputY = 0;
        JumpInput = false;
        SitInput = false;
        attackInput = false;
    }

    #region CmdSetfunction
    [Command]
    public void CmdSetmovementVelocity(float newvalue)
    {
        movementVelocity = newvalue;
    }

    [Command]
    public void CmdSetbulletspeed(float newvalue)
    {
        bulletspeed = newvalue;
    }

    [Command]
    public void CmdSetgroundCheckRadious(float newvalue)
    {
        groundCheckRadious = newvalue;
    }

    [Command]
    public void CmdSetwhatIsGround(LayerMask newvalue)
    {
        whatIsGround = newvalue;
    }

    [Command]
    public void CmdSetwhatIsBlind(LayerMask newvalue)
    {
        whatIsBlind = newvalue;
    }
    [Command]
    public void CmdSetFacingDirection(int newvlaue)
    {
        FacingDirection = newvlaue;
    }
    [Command]
    public void CmdSetposition(Vector3 newvalue)
    {
        position = newvalue;
    }
    [Command]
    public void CmdSetisRiding(bool newvalue)
    {
        isRiding = newvalue;
    }

    [Command]
    public void CmdSetishavingGun(bool newvalue)
    {
        ishavingGun = newvalue;
    }

    [Command]
    public void CmdSetHoldingGun(bool newvalue)
    {
        HoldingGun = newvalue;
    }

    [Command]
    public void CmdSetshotDelay(float newvalue)
    {
        ShotDelay = newvalue;
    }

    [Command]
    public void CmdSetNormInputX(int newvalue)
    {
        NormInputX = newvalue;
    }

    [Command]
    public void CmdSetNormInputY(int newvalue)
    {
        NormInputY = newvalue;
    }

    [Command]
    public void CmdSetJumpInput(bool newvalue)
    {
        JumpInput = newvalue;
    }

    [Command]
    public void CmdSetSitInput(bool newvalue)
    {
        SitInput = newvalue;
    }

    [Command]
    public void CmdSetattackInput(bool newvalue)
    {
        attackInput = newvalue;
    }

    [Command]
    public void CmdSetmousePosition(Vector3 newvalue)
    {
        mousePosition = newvalue;
    }
    #endregion
}
