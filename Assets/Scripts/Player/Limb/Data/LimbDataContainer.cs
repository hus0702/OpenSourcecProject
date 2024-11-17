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

        if (movementVelocity != limbData.movementVelocity)
        {
            movementVelocity = limbData.movementVelocity;
        }
        if (bulletspeed != limbData.bulletspeed)
        {
            bulletspeed = limbData.bulletspeed;
        }
        if (groundCheckRadious != limbData.groundCheckRadious)
        {
            groundCheckRadious = limbData.groundCheckRadious;
        }
        if (whatIsGround != limbData.whatIsGround)
        {
            whatIsGround = limbData.whatIsGround;
        }
        if (whatIsBlind != limbData.whatIsBlind)
        {
            whatIsBlind = limbData.whatIsBlind;
        }
        if (isRiding != limbData.isRiding)
        {
            isRiding = limbData.isRiding;
        }
        if (ishavingGun != limbData.ishavingGun)
        {
            ishavingGun = limbData.ishavingGun;
        }
        if (HoldingGun != limbData.HoldingGun)
        {
            HoldingGun = limbData.HoldingGun;
        }
        if (ShotDelay != limbData.ShotDelay)
        {
            ShotDelay = limbData.ShotDelay;
        }
        if (NormInputX != limbData.NormInputX)
        {
            NormInputX = limbData.NormInputX;
        }
        if (NormInputY != limbData.NormInputY)
        {
            NormInputY = limbData.NormInputY;
        }
        if (JumpInput != limbData.JumpInput)
        {
            JumpInput = limbData.JumpInput;
        }
        if (SitInput != limbData.SitInput)
        {
            SitInput = limbData.SitInput;
        }
        if (attackInput != limbData.attackInput)
        {
            attackInput = limbData.attackInput;
        }
        if (mousePosition != limbData.mousePosition)
        {
            mousePosition = limbData.mousePosition;
        }
    }
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

}
