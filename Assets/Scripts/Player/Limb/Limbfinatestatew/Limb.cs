using Mirror.BouncyCastle.Asn1.TeleTrust;
using Steamworks;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Limb : PlayerObjectController
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

    [SerializeField]
    public LimbData limbData;

    #endregion

    #region Components
    public LimbInputHandler InputHandler { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform limbtransform { get; private set; }

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
    public int FacingDirection { get; private set; }

    private Vector2 workspace;

    public bool IsCarrying { get; private set; }

    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        Flip(); // 초기 오프셋 세팅
        StateMachine = new PlayerStateMachine();
        IdleState = new LimbIdleState(this, StateMachine, limbData, "Idle");
        MoveState = new LimbMoveState(this, StateMachine, limbData, "move");
        RideState = new LimbRideState(this, StateMachine, limbData, "Ride");
        RidingState = new LimbRidingState(this, StateMachine, limbData, "Riding");
        ShotState = new LimbShotState(this, StateMachine, limbData, "shot");
        PutDownState = new LimbPutDownState(this, StateMachine, limbData, "putdown");
        ThrowState = new LimbThrowState(this, StateMachine, limbData, "throw");
        inAirState = new LimbinAirState(this, StateMachine, limbData, "inair");

        limbData.isRiding = false;
    }

    private void Start()
    {
        thisController = this.GetComponent<PlayerObjectController>();
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        InputHandler = GetComponent<LimbInputHandler>();
        limbtransform = GetComponent<Transform>();
        FacingDirection = -1;
        StateMachine.LimbInitialize(IdleState,limbData);
        BulletPrefab = bulletprefab;
    }

    private void Update()
    {
        CurrentVelocity = RB.linearVelocity;
        StateMachine.LimbCurrentState.LogicUpdate();

        if (limbData.isRiding)
        {
            this.limbtransform.position = (GameManager.instance.PlayerData.blindtransform.position + new Vector3(0, 1f, 0));
            this.RB.gravityScale = 0f;
        }
        else
        {
            this.RB.gravityScale = 5f;
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
        if (xinput != 0 && xinput != -FacingDirection)
        {
            Flip();
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundcheck.position, limbData.groundCheckRadious, limbData.whatIsGround);
    }

    public bool CheckIftouchBlind()
    {
        //Collider2D[] results = new Collider2D[10];
        //ContactFilter2D contactFilter = new ContactFilter2D();
        //contactFilter.SetLayerMask(limbData.whitIsBlind);
        //contactFilter.useLayerMask = true;
        return Physics2D.OverlapCircle(groundcheck.position, limbData.groundCheckRadious, limbData.whatIsBlind);
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
        FacingDirection *= -1;
        base.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion
}
