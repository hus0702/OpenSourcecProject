using Mirror;
using Mirror.Examples.Common;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : NetworkBehaviour
{
    #region State Variables

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerSitMoveState SitMoveState { get; private set; }
    public PlayerSitState SitState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerinAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }

    public PlayerCarryUpState carryUpState { get; private set; }
    public PlayerC_MoveState c_moveState { get; private set; }
    public PlayerC_IdleState c_idleState { get; private set; }
    public PlayerC_LandState c_LandState { get; private set; }
    public PlayerC_inAirState c_InAirState { get; private set; }
    public PlayerC_ClimbingState c_ClimbingState { get; private set; }
    public PlayerClimbingState climbingState { get; private set; }
    public PlayerClimbState climbState { get; private set; }
    public PlayerPutDownState PutDownState { get; private set; }
    public PlayerThrowState ThrowState { get; private set; }
    public PlayerDataContainer container { get; private set; }

    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform playerTransform { get; private set; }
    public GameObject detectedObject { get; private set; }
    public PlayerObjectController thisController { get; private set; }
    #endregion

    #region Check Variables

    [SerializeField]
    private Transform groundcheck;
    [SerializeField]
    private BoxCollider2D myBoxCollider;

    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    private Vector2 workspace;

    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        container = GameManager.instance.Pdcontainer;
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, container, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, container, "move");
        SitMoveState = new PlayerSitMoveState(this, StateMachine, container, "sitmove");
        SitState = new PlayerSitState(this, StateMachine, container, "sit");
        JumpState = new PlayerJumpState(this, StateMachine, container, "jump");
        InAirState = new PlayerinAirState(this, StateMachine, container, "inAir");
        LandState = new PlayerLandState(this, StateMachine, container, "land");
        carryUpState = new PlayerCarryUpState(this, StateMachine, container, "carryUp");
        c_moveState = new PlayerC_MoveState(this, StateMachine, container, "C_move");
        c_idleState = new PlayerC_IdleState(this, StateMachine, container, "C_Idle");
        c_LandState = new PlayerC_LandState(this, StateMachine, container, "C_Land");
        c_InAirState = new PlayerC_inAirState(this, StateMachine, container, "c_inAir");
        c_ClimbingState = new PlayerC_ClimbingState(this, StateMachine, container, "c_climbing");
        PutDownState = new PlayerPutDownState(this, StateMachine, container, "putdown");
        ThrowState = new PlayerThrowState(this, StateMachine, container, "throw");
        climbingState = new PlayerClimbingState(this, StateMachine, container, "climbing");
        climbState = new PlayerClimbState(this, StateMachine, container, "climb");
    }

    private void Start()
    {
        thisController = this.GetComponent<PlayerObjectController>();
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>(); 
        playerTransform = GetComponent<Transform>();
        FacingDirection = 1;
        StateMachine.PlayerInitialize(IdleState, container);
    }

    private void Update()
    {
        CurrentVelocity = RB.linearVelocity;
        StateMachine.playerCurrentState.LogicUpdate();
        
        //if (isOwned) 
        //{
        //    container.position = transform.position;
        //}
        //else
        //{
        //    transform.position = container.position;
        //}
    }

    private void FixedUpdate()
    {
        StateMachine.playerCurrentState.PhysicsUpdate();
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
    public void SetLaddderPosition(GameObject gameobj)
    {
        Vector3 pos = gameobj.transform.position;
        pos.y = this.transform.position.y;
        pos.z = this.transform.position.z;
        this.transform.position = pos;
    }

    #endregion

    #region Check Functions
    public void CheckifShouldflip(int xinput)
    { 
        if(xinput != 0 && xinput != container.facingdirection) 
        {
            Flip();
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundcheck.position, container.groundCheckRadious, container.whatIsGround);
    }

    public bool CheckIftouchLimb()
    {
        return Physics2D.OverlapCircle(groundcheck.position, container.groundCheckRadious, container.whatIsLimb);

    }
    public bool CheckIftouchLadder()
    {
        return Physics2D.OverlapCircle(groundcheck.position, container.groundCheckRadious, container.whatIsLadder);
    }
    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.playerCurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.playerCurrentState.AnimationFinishTrigger();
    private void Flip()
    {
        if (isServer)
        {
            container.facingdirection *= -1;
        }
        else
        {
            CmdSetFacingDirection(container.facingdirection *= -1);
        }
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.layer == container.whatIsLadder && container.isclimbing)
    //    {
    //        SetLaddderPosition(other.gameObject);
    //    }
    //}
    #endregion

    #region ContainerCommandfunction
    [Command]
    public void CmdSetFacingDirection(float newvalue)
    {
        container.facingdirection = newvalue;
    }

    [Command]
    public void CmdSetIsCarrying(bool newvalue)
    {
        container.iscarrying = newvalue;
    }

    [Command]
    public void CmdSetIsClimbing(bool newvalue)
    {
        container.isclimbing = newvalue;
    }

    [Command]
    public void CmdSetCarryUpCall(bool newvalue)
    {
        container.carryupcall = newvalue;
    }

    [Command]
    public void CmdSetThrowCall(bool newvalue)
    {
        container.throwcall = newvalue;
    }

    [Command]
    public void CmdSetPutDownCall(bool newvalue)
    {
        container.putdowncall = newvalue;
    }

    [Command]
    public void CmdSetThrowInputTime(float newvalue)
    {
        container.throwinputtime = newvalue;
    }
    //[Command]
    //public void CmdSetposition(Vector3 newvalue)
    //{
    //    container.position = newvalue;
    //}
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
    public void CmdSetLadderUp(bool newvalue)
    {
        container.ladderUp = newvalue;
    }

    [Command]
    public void CmdSetLadderDown(bool newvalue)
    {
        container.ladderDown = newvalue;
    }
    #endregion

}
