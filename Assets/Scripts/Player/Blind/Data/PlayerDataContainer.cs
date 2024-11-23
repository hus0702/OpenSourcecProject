using JetBrains.Annotations;
using Mirror;
using Mirror.BouncyCastle.Asn1.BC;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDataContainer : NetworkBehaviour
{
    [SyncVar] public float movementVelocity = 10f;
    [SyncVar] public float sitmovementVelocity = 5f;
    [SyncVar] public float facingdirection = 1;
    [SyncVar] public float climbVelocity = 3;
    [SyncVar] public float C_movementVelocity = 5f;
    [SyncVar] public float jumpVelocity = 15f;
    [SyncVar] public float groundCheckRadious = 0.5f;
    [SyncVar] public LayerMask whatIsGround;
    [SyncVar] public LayerMask whatIsLimb;
    [SyncVar] public LayerMask whatIsLadder;
    [SyncVar] public bool iscarrying = false;
    [SyncVar] public bool isclimbing = false;
    [SyncVar] public bool carryupcall = false;
    [SyncVar] public bool throwcall = false;
    [SyncVar] public bool putdowncall = false;
    [SyncVar] public float throwinputtime = 0;
    [SyncVar] public Vector3 position;

    [SyncVar]public int NormInputX = 0;
    [SyncVar]public int NormInputY = 0;
    [SyncVar]public bool JumpInput = false;
    [SyncVar]public bool SitInput = false;
    [SyncVar]public bool ladderUp = false;
    [SyncVar]public bool ladderDown = false;


    [Command]
    public void CmdSetmovementVelocity(float newvalue)
    {
        movementVelocity = newvalue;
    }

    [Command]
    public void CmdSetSitMovementVelocity(float newvalue)
    {
        sitmovementVelocity = newvalue;
    }

    [Command]
    public void CmdSetFacingDirection(float newvalue)
    {
        facingdirection = newvalue;
    }

    [Command]
    public void CmdSetClimbVelocity(float newvalue)
    {
        climbVelocity = newvalue;
    }

    [Command]
    public void CmdSetC_MovementVelocity(float newvalue)
    {
        C_movementVelocity = newvalue;
    }

    [Command]
    public void CmdSetJumpVelocity(float newvalue)
    {
        jumpVelocity = newvalue;
    }

    [Command]
    public void CmdSetGroundCheckRadious(float newvalue)
    {
        groundCheckRadious = newvalue;
    }

    [Command]
    public void CmdSetWhatIsGround(LayerMask newvalue)
    {
        whatIsGround = newvalue;
    }

    [Command]
    public void CmdSetWhatIsLimb(LayerMask newvalue)
    {
        whatIsLimb = newvalue;
    }

    [Command]
    public void CmdSetWhatIsLadder(LayerMask newvalue)
    {
        whatIsLadder = newvalue;
    }

    [Command]
    public void CmdSetIsCarrying(bool newvalue)
    {
        iscarrying = newvalue;
    }

    [Command]
    public void CmdSetIsClimbing(bool newvalue)
    {
        isclimbing = newvalue;
    }

    [Command]
    public void CmdSetCarryUpCall(bool newvalue)
    {
        carryupcall = newvalue;
    }

    [Command]
    public void CmdSetThrowCall(bool newvalue)
    {
        throwcall = newvalue;
    }

    [Command]
    public void CmdSetPutDownCall(bool newvalue)
    {
        putdowncall = newvalue;
    }

    [Command]
    public void CmdSetThrowInputTime(float newvalue)
    {
        throwinputtime = newvalue;
    }
    [Command]
    public void CmdSetposition(Vector3 newvalue)
    {
        position = newvalue;
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
    public void CmdSetLadderUp(bool newvalue)
    {
        ladderUp = newvalue;
    }

    [Command]
    public void CmdSetLadderDown(bool newvalue)
    {
        ladderDown = newvalue;
    }

}
