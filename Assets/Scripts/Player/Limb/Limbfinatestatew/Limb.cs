using Mirror;
using Mirror.BouncyCastle.Asn1.TeleTrust;
using Steamworks;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Limb : NetworkBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public LimbIdleState IdleState { get; private set; }
    public LimbMoveState MoveState { get; private set; }
    public LimbRidingState RidingState { get; private set; }
    public LimbRideState RideState { get; private set; }
    public LimbShotState ShotState { get; private set; }
    public LimbinAirState inAirState { get; private set; }
    public LimbPutDownState PutDownState { get; private set; }
    public LimbThrowState ThrowState { get; private set; }

    public LimbRidingShotState RidingShotState { get; private set; }

    public LimbDataContainer container;

    #endregion

    #region Components
    public LimbInputHandler InputHandler { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform limbtransform { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public PlayerObjectController thisController { get; private set; }

    [SerializeField] private GameObject bulletprefab;
    public GameObject BulletPrefab { get; private set; }


    #endregion

    #region Check Variables

    [SerializeField]
    private Transform groundcheck;

    [SerializeField]
    private BoxCollider2D myBoxCollider;


    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 workspace;

    public bool IsCarrying { get; private set; }

    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        container = GameManager.instance.Ldcontainer;

        StateMachine = new PlayerStateMachine();

        IdleState = new LimbIdleState(this, StateMachine, container, "Idle");
        MoveState = new LimbMoveState(this, StateMachine, container, "move");
        RideState = new LimbRideState(this, StateMachine, container, "Ride");
        RidingState = new LimbRidingState(this, StateMachine, container, "Riding");
        ShotState = new LimbShotState(this, StateMachine, container, "shot");
        PutDownState = new LimbPutDownState(this, StateMachine, container, "putdown");
        ThrowState = new LimbThrowState(this, StateMachine, container, "throw");
        inAirState = new LimbinAirState(this, StateMachine, container, "inair");
        RidingShotState = new LimbRidingShotState(this, StateMachine, container, "RidingShot");
    }

    private void Start()
    {
        thisController = this.GetComponent<PlayerObjectController>();
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        InputHandler = GetComponent<LimbInputHandler>();
        limbtransform = GetComponent<Transform>();
        StateMachine.LimbInitialize(IdleState, container);
        BulletPrefab = bulletprefab;
    }

    private void Update()
    {
        CurrentVelocity = RB.linearVelocity;
        StateMachine.LimbCurrentState.LogicUpdate();

        if (container.isRiding)
        {
            this.limbtransform.position = (container.position + new Vector3(0, 1f, 0));
        }
    }

    private void FixedUpdate()
    {
        StateMachine.LimbCurrentState.PhysicsUpdate();
    }

    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.linearVelocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.linearVelocity = workspace;
        CurrentVelocity = workspace;
    }

    public void changestate(LimbState newstate)
    {
        StateMachine.LimbChangeState(newstate);
    }
    #endregion

    #region Check Functions
    public void CheckifShouldflip(int xinput)
    {
        if (xinput != 0 && xinput != container.FacingDirection)
        {
            Flip();
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundcheck.position, container.groundCheckRadious, container.whatIsGround);
    }

    public bool CheckIftouchBlind()
    {
        //Collider2D[] results = new Collider2D[10];
        //ContactFilter2D contactFilter = new ContactFilter2D();
        //contactFilter.SetLayerMask(limbData.whitIsBlind);
        //contactFilter.useLayerMask = true;
        return Physics2D.OverlapCircle(groundcheck.position, container.groundCheckRadious, container.whatIsBlind);
        //if (Physics2D.OverlapCollider(Collider, contactFilter, results) == 0)
        //{
        //    return false;
        //}
        //else
        //{
        //    return true;
        //}
    }

    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.LimbCurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.LimbCurrentState.AnimationFinishTrigger();
    public void Flip()
    {
        if (isServer)
        {
            container.FacingDirection *= -1;
        }
        else
        {
            CmdSetFacingDirection(container.FacingDirection *= -1);
        }
        base.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion

    #region ContainerSetFunction
    [Command]
    public void CmdSetFacingDirection(int newvlaue)
    {
        container.FacingDirection = newvlaue;
    }
    [Command]
    public void CmdSetposition(Vector3 newvalue)
    {
        container.position = newvalue;
    }
    [Command]
    public void CmdSetisRiding(bool newvalue)
    {
        container.isRiding = newvalue;
    }

    [Command]
    public void CmdSetishavingGun(bool newvalue)
    {
        container.ishavingGun = newvalue;
    }

    [Command]
    public void CmdSetHoldingGun(bool newvalue)
    {
        container.HoldingGun = newvalue;
    }

    [Command]
    public void CmdSetshotDelay(float newvalue)
    {
        container.ShotDelay = newvalue;
    }

    [Command]
    public void CmdSetNormInputX(int newvalue)
    {
        container.NormInputX = newvalue;
    }

    [Command]
    public void CmdSetNormInputY(int newvalue)
    {
        container.NormInputY = newvalue;
    }

    [Command]
    public void CmdSetJumpInput(bool newvalue)
    {
        container.JumpInput = newvalue;
    }

    [Command]
    public void CmdSetSitInput(bool newvalue)
    {
        container.SitInput = newvalue;
    }

    [Command]
    public void CmdSetattackInput(bool newvalue)
    {
        container.attackInput = newvalue;
    }

    [Command]
    public void CmdSetmousePosition(Vector3 newvalue)
    {
        container.mousePosition = newvalue;
    }

    [Command]
    public void CmdSetThrowCall(bool newvalue)
    { 
        GameManager.instance.Pdcontainer.throwcall = newvalue;
    }

    [Command]
    public void CmdSetCarryUpCall(bool newvalue)
    {
        GameManager.instance.Pdcontainer.carryupcall = newvalue;
    }

    [Command]
    public void CmdSetPutDownCall(bool newvalue)
    {
        GameManager.instance.Pdcontainer.carryupcall = newvalue;
    }
    #endregion
}
