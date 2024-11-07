using Mirror;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerinAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }

    public PlayerCarryUpState carryUpState { get; private set; }
    public PlayerC_MoveState c_moveState { get; private set; }
    public PlayerC_IdleState c_idleState { get; private set; }
    public PlayerC_LandState c_LandState { get; private set; }
    public PlayerC_inAirState c_InAirState { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform playerTransform { get; private set; }

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
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerinAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        carryUpState = new PlayerCarryUpState(this, StateMachine, playerData, "carryUp");
        c_moveState = new PlayerC_MoveState(this, StateMachine, playerData, "c_move");
        c_idleState = new PlayerC_IdleState(this, StateMachine, playerData, "c_idle");
        c_LandState = new PlayerC_LandState(this, StateMachine, playerData, "c_land");
        c_InAirState = new PlayerC_inAirState(this, StateMachine, playerData, "c_inAir");
    }

    private void Start()
    {
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
    #endregion

    #region Check Functions
    public void CheckifShouldflip(int xinput)
    { 
        if(xinput != 0 && xinput != FacingDirection) 
        {
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
    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.playerCurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.playerCurrentState.AnimationFinishTrigger();
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }


    #endregion
}
