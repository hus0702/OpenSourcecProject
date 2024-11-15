using Mirror;
using NUnit.Framework.Constraints;
using UnityEngine;

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

    [SerializeField]
    private PlayerData playerData;

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
        playerData.iscarrying = false; // 초기 설정이 안됨. ㅠ
        playerData.carryupcall = false;
        playerData.putdowncall = false;
        playerData.facingdirection = 1;

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        SitMoveState = new PlayerSitMoveState(this, StateMachine, playerData, "sitmove");
        SitState = new PlayerSitState(this, StateMachine, playerData, "sit");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerinAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        carryUpState = new PlayerCarryUpState(this, StateMachine, playerData, "carryUp");
        c_moveState = new PlayerC_MoveState(this, StateMachine, playerData, "C_move");
        c_idleState = new PlayerC_IdleState(this, StateMachine, playerData, "C_Idle");
        c_LandState = new PlayerC_LandState(this, StateMachine, playerData, "C_Land");
        c_InAirState = new PlayerC_inAirState(this, StateMachine, playerData, "c_inAir");
        c_ClimbingState = new PlayerC_ClimbingState(this, StateMachine, playerData, "c_climbing");
        PutDownState = new PlayerPutDownState(this, StateMachine, playerData, "putdown");
        ThrowState = new PlayerThrowState(this, StateMachine, playerData, "throw");
        climbingState = new PlayerClimbingState(this, StateMachine, playerData, "climbing");
        climbState = new PlayerClimbState(this, StateMachine, playerData, "climb");
    }

    private void Start()
    {
        thisController = this.GetComponent<PlayerObjectController>();
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>(); 
        playerTransform = GetComponent<Transform>();
        FacingDirection = 1;
        StateMachine.PlayerInitialize(IdleState, playerData);
    }

    private void Update()
    {
        CurrentVelocity = RB.linearVelocity;
        StateMachine.playerCurrentState.LogicUpdate();
        playerData.blindtransform = this.transform;
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
        if(xinput != 0 && xinput != FacingDirection) 
        {
            playerData.facingdirection *= -1;
            Flip();
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundcheck.position, playerData.groundCheckRadious, playerData.whatIsGround);
    }

    public bool CheckIftouchLimb()
    {
        return Physics2D.OverlapCircle(groundcheck.position, playerData.groundCheckRadious, playerData.whatIsLimb);

    }
    public bool CheckIftouchLadder()
    {
        return Physics2D.OverlapCircle(groundcheck.position, playerData.groundCheckRadious, playerData.whatIsLadder);
    }
    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.playerCurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.playerCurrentState.AnimationFinishTrigger();
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == playerData.whatIsLadder && playerData.isclimbing)
        {
            SetLaddderPosition(other.gameObject);
        }
    }
    #endregion
}
