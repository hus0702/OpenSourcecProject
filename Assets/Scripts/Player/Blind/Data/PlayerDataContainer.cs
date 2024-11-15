using JetBrains.Annotations;
using Mirror;
using Mirror.BouncyCastle.Asn1.BC;
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
    [SyncVar(hook = nameof(Setblindtransform))] public Transform blindtransform;

    private void Update()
    {
        if (movementVelocity != playerData.movementVelocity)
        { 
            movementVelocity = playerData.movementVelocity;
        }
        if (sitmovementVelocity != playerData.sitmovementVelocity)
        {
            sitmovementVelocity = playerData.sitmovementVelocity;
        }
        if (facingdirection != playerData.facingdirection)
        {
            facingdirection = playerData.facingdirection;
        }
        if (climbVelocity != playerData.climbVelocity)
        {
            climbVelocity = playerData.climbVelocity;
        }
        if (C_movementVelocity != playerData.C_movementVelocity)
        {
            C_movementVelocity = playerData.C_movementVelocity;
        }
        if (jumpVelocity != playerData.jumpVelocity)
        {
            jumpVelocity = playerData.jumpVelocity;
        }
        if (groundCheckRadious != playerData.groundCheckRadious)
        {
            groundCheckRadious = playerData.groundCheckRadious;
        }
        if (whatIsGround != playerData.whatIsGround)
        {
            whatIsGround = playerData.whatIsGround;
        }
        if (whatIsLimb != playerData.whatIsLimb)
        {
            whatIsLimb = playerData.whatIsLimb;
        }
        if (whatIsLadder != playerData.whatIsLadder)
        {
            whatIsLadder = playerData.whatIsLadder;
        }
        if (iscarrying != playerData.iscarrying)
        {
            iscarrying = playerData.iscarrying;
        }
        if (isclimbing != playerData.isclimbing)
        {
            isclimbing = playerData.isclimbing;
        }
        if (carryupcall != playerData.carryupcall)
        {
            carryupcall = playerData.carryupcall;
        }
        if (throwcall != playerData.throwcall)
        {
            throwcall = playerData.throwcall;
        }
        if (putdowncall != playerData.putdowncall)
        {
            putdowncall = playerData.putdowncall;
        }
        if (throwinputtime != playerData.throwinputtime)
        {
            throwinputtime = playerData.throwinputtime;
        }
        if (blindtransform != playerData.blindtransform)
        { 
            blindtransform = playerData.blindtransform;
        }
    }

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
    void Setblindtransform(Transform oldvalue, Transform newvalue)
    {
        playerData.blindtransform = newvalue;
    }
}
