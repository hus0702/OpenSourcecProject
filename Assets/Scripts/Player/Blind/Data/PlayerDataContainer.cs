using JetBrains.Annotations;
using Mirror;
using Mirror.BouncyCastle.Asn1.BC;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDataContainer : NetworkBehaviour
{
    public PlayerData playerData;

    [SyncVar(hook = nameof(SetmovementVelocity))] public float movementVelocity;
    [SyncVar(hook = nameof(SetsitmovementVelocity))] public float sitmovementVelocity;
    [SyncVar(hook = nameof(Setfacingdirection))] public float facingdirection;
    [SyncVar(hook = nameof(SetclimbVelocity))] public float climbVelocity;
    [SyncVar(hook = nameof(SetC_movementVelocity))] public float C_movementVelocity;
    [SyncVar(hook = nameof(SetjumpVelocity))] public float jumpVelocity;
    [SyncVar(hook = nameof(SetgroundCheckRadious))] public float groundCheckRadious;
    [SyncVar(hook = nameof(SetwhatIsGround))] public LayerMask whatIsGround;
    [SyncVar(hook = nameof(SetwhatIsLimb))] public LayerMask whatIsLimb;
    [SyncVar(hook = nameof(SetwhatIsLadder))] public LayerMask whatIsLadder;
    [SyncVar(hook = nameof(Setiscarrying))] public bool iscarrying;
    [SyncVar(hook = nameof(Setisclimbing))] public bool isclimbing;
    [SyncVar(hook = nameof(Setcarryupcall))] public bool carryupcall;
    [SyncVar(hook = nameof(Setthrowcall))] public bool throwcall;
    [SyncVar(hook = nameof(Setputdowncall))] public bool putdowncall;
    [SyncVar(hook = nameof(Setthrowinputtime))] public float throwinputtime;

    [SyncVar(hook = nameof(SetNormInputX))]public int NormInputX;
    [SyncVar(hook = nameof(SetNormInputY))]public int NormInputY;
    [SyncVar(hook = nameof(SetJumpInput))]public bool JumpInput;
    [SyncVar(hook = nameof(SetSitInput))]public bool SitInput;
    [SyncVar(hook = nameof(SetladderUp))]public bool ladderUp;
    [SyncVar(hook = nameof(SetladderDown))]public bool ladderDown;

    [SyncVar(hook = nameof(Setblindtransform))] public Transform blindtransform;

    private void Update()
    {

        if (movementVelocity != playerData.movementVelocity)
        {
            CmdSetmovementVelocity(playerData.movementVelocity);
        }
        if (sitmovementVelocity != playerData.sitmovementVelocity)
        {
            CmdSetSitMovementVelocity(playerData.sitmovementVelocity);
        }
        if (facingdirection != playerData.facingdirection)
        {
            CmdSetFacingDirection(playerData.facingdirection);
        }
        if (climbVelocity != playerData.climbVelocity)
        {
            CmdSetClimbVelocity(playerData.climbVelocity);
        }
        if (C_movementVelocity != playerData.C_movementVelocity)
        {
            CmdSetC_MovementVelocity(playerData.C_movementVelocity);
        }
        if (jumpVelocity != playerData.jumpVelocity)
        {
            CmdSetJumpVelocity(playerData.jumpVelocity);
        }
        if (groundCheckRadious != playerData.groundCheckRadious)
        {
            CmdSetGroundCheckRadious(playerData.groundCheckRadious);
        }
        if (whatIsGround != playerData.whatIsGround)
        {
            CmdSetWhatIsGround(playerData.whatIsGround);
        }
        if (whatIsLimb != playerData.whatIsLimb)
        {
            CmdSetWhatIsLimb(playerData.whatIsLimb);
        }
        if (whatIsLadder != playerData.whatIsLadder)
        {
            CmdSetWhatIsLadder(playerData.whatIsLadder);
        }
        if (iscarrying != playerData.iscarrying)
        {
            CmdSetIsCarrying(playerData.iscarrying);
        }
        if (isclimbing != playerData.isclimbing)
        {
            CmdSetIsClimbing(playerData.isclimbing);
        }
        if (carryupcall != playerData.carryupcall)
        {
            CmdSetCarryUpCall(playerData.carryupcall);
        }
        if (throwcall != playerData.throwcall)
        {
            CmdSetThrowCall(playerData.throwcall);
        }
        if (putdowncall != playerData.putdowncall)
        {
            CmdSetPutDownCall(playerData.putdowncall);
        }
        if (throwinputtime != playerData.throwinputtime)
        {
            CmdSetThrowInputTime(playerData.throwinputtime);
        }
        if (NormInputX != playerData.NormInputX)
        {
            CmdSetNormInputX(playerData.NormInputX);
        }
        if (NormInputY != playerData.NormInputY)
        {
            CmdSetNormInputY(playerData.NormInputY);
        }
        if (JumpInput != playerData.JumpInput)
        {
            CmdSetJumpInput(playerData.JumpInput);
        }
        if (SitInput != playerData.SitInput)
        {
            CmdSetSitInput(playerData.SitInput);
        }
        if (ladderUp != playerData.ladderUp)
        {
            CmdSetLadderUp(playerData.ladderUp);
        }
        if (ladderDown != playerData.ladderDown)
        {
            CmdSetLadderDown(playerData.ladderDown);
        }
        if (blindtransform != playerData.blindtransform)
        {
            CmdSetBlindTransform(playerData.blindtransform);
        }
    }
    #region Setfunction
    void SetmovementVelocity(float oldvalue, float newvalue)
    { 
        playerData.movementVelocity = newvalue;
    }
    void SetsitmovementVelocity(float oldvalue, float newvalue)
    {
        playerData.sitmovementVelocity = newvalue;
    }
    void Setfacingdirection(float oldvalue, float newvalue)
    {
        playerData.facingdirection = newvalue;
    }
    void SetclimbVelocity(float oldvalue, float newvalue)
    {
        playerData.climbVelocity = newvalue;
    }
    void SetC_movementVelocity(float oldvalue, float newvalue)
    {
        playerData.C_movementVelocity = newvalue;
    }
    void SetjumpVelocity(float oldvalue, float newvalue)
    {
        playerData.jumpVelocity = newvalue;
    }
    void SetgroundCheckRadious(float oldvalue, float newvalue)
    {
        playerData.groundCheckRadious = newvalue;
    }
    void SetwhatIsGround(LayerMask oldvalue, LayerMask newvalue)
    {
        playerData.whatIsGround = newvalue;
    }
    void SetwhatIsLimb(LayerMask oldvalue, LayerMask newvalue)
    {
        playerData.whatIsLimb = newvalue;
    }
    void SetwhatIsLadder(LayerMask oldvalue, LayerMask newvalue)
    {
        playerData.whatIsLadder = newvalue;
    }
    void Setiscarrying(bool oldvalue, bool newvalue)
    {
        playerData.iscarrying = newvalue;
    }
    void Setisclimbing(bool oldvalue, bool newvalue)
    {
        playerData.isclimbing = newvalue;
    }
    void Setcarryupcall(bool oldvalue, bool newvalue)
    {
        playerData.carryupcall = newvalue;
    }
    void Setthrowcall(bool oldvalue, bool newvalue)
    {
        playerData.throwcall = newvalue;
    }
    void Setputdowncall(bool oldvalue, bool newvalue)
    {
        playerData.putdowncall = newvalue;
    }
    void Setthrowinputtime(float oldvalue, float newvalue)
    {
        playerData.throwinputtime = newvalue;
    }
    void SetNormInputX(int oldvalue, int newvalue)
    { 
        playerData.NormInputX = newvalue;
    }
    void SetNormInputY(int oldvalue, int newvalue)
    {
        playerData.NormInputY = newvalue;
    }
    void SetJumpInput(bool oldvalue, bool newvalue)
    {
        playerData.JumpInput = newvalue;
    }
    void SetSitInput(bool oldvalue, bool newvalue)
    { 
        playerData.SitInput = newvalue;
    }
    void SetladderUp(bool oldvalue, bool newvalue)
    { 
        playerData.ladderUp = newvalue;
    }
    void SetladderDown(bool oldvalue, bool newvalue)
    {
        playerData.ladderDown = newvalue;
    }
    void Setblindtransform(Transform oldvalue, Transform newvalue)
    {
        playerData.blindtransform = newvalue;
    }
    #endregion
    [Command]
    void CmdSetmovementVelocity(float newvalue)
    {
        movementVelocity = newvalue;
    }

    [Command]
    void CmdSetSitMovementVelocity(float newvalue)
    {
        sitmovementVelocity = newvalue;
    }

    [Command]
    void CmdSetFacingDirection(float newvalue)
    {
        facingdirection = newvalue;
    }

    [Command]
    void CmdSetClimbVelocity(float newvalue)
    {
        climbVelocity = newvalue;
    }

    [Command]
    void CmdSetC_MovementVelocity(float newvalue)
    {
        C_movementVelocity = newvalue;
    }

    [Command]
    void CmdSetJumpVelocity(float newvalue)
    {
        jumpVelocity = newvalue;
    }

    [Command]
    void CmdSetGroundCheckRadious(float newvalue)
    {
        groundCheckRadious = newvalue;
    }

    [Command]
    void CmdSetWhatIsGround(LayerMask newvalue)
    {
        whatIsGround = newvalue;
    }

    [Command]
    void CmdSetWhatIsLimb(LayerMask newvalue)
    {
        whatIsLimb = newvalue;
    }

    [Command]
    void CmdSetWhatIsLadder(LayerMask newvalue)
    {
        whatIsLadder = newvalue;
    }

    [Command]
    void CmdSetIsCarrying(bool newvalue)
    {
        iscarrying = newvalue;
    }

    [Command]
    void CmdSetIsClimbing(bool newvalue)
    {
        isclimbing = newvalue;
    }

    [Command]
    void CmdSetCarryUpCall(bool newvalue)
    {
        carryupcall = newvalue;
    }

    [Command]
    void CmdSetThrowCall(bool newvalue)
    {
        throwcall = newvalue;
    }

    [Command]
    void CmdSetPutDownCall(bool newvalue)
    {
        putdowncall = newvalue;
    }

    [Command]
    void CmdSetThrowInputTime(float newvalue)
    {
        throwinputtime = newvalue;
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
    void CmdSetLadderUp(bool newvalue)
    {
        ladderUp = newvalue;
    }

    [Command]
    void CmdSetLadderDown(bool newvalue)
    {
        ladderDown = newvalue;
    }

    [Command]
    void CmdSetBlindTransform(Transform newvalue)
    {
        blindtransform = newvalue;
    }
}
