using Mirror.BouncyCastle.Asn1.TeleTrust;
using Steamworks;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Limb : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public LimbIdleState IdleState { get; private set; }
    public LimbRidingState RidingState { get; private set; }
    public LimbRideState RideState { get; private set; }

    public LimbinAirState inAirState { get; private set; }

    public LimbPutDownState PutDownState { get; private set; }
    public LimbThrowState ThrowState { get; private set; }

    [SerializeField]
    public LimbData limbData;

    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform transform { get; private set; }
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
        StateMachine = new PlayerStateMachine();
        IdleState = new LimbIdleState(this, StateMachine, limbData, "Idle");
        RideState = new LimbRideState(this, StateMachine, limbData, "Ride");
        RidingState = new LimbRidingState(this, StateMachine, limbData, "Riding");
        PutDownState = new LimbPutDownState(this, StateMachine, limbData, "putdown");
        ThrowState = new LimbThrowState(this, StateMachine, limbData, "throw");
        inAirState = new LimbinAirState(this, StateMachine, limbData, "inair");

        limbData.isRiding = false;
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        FacingDirection = 1;
        StateMachine.LimbInitialize(IdleState,limbData);
    }

    private void Update()
    {
        CurrentVelocity = RB.linearVelocity;
        StateMachine.LimbCurrentState.LogicUpdate();

        if (limbData.isRiding)
        {
            this.transform.position = (GameManager.instance.PlayerData.blindtransform.position + new Vector3(0, 1f, 0));
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
        if (xinput != 0 && xinput != FacingDirection)
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
        return Physics2D.OverlapCircle(groundcheck.position, limbData.groundCheckRadious, limbData.whitIsBlind);

    }

    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.LimbCurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.LimbCurrentState.AnimationFinishTrigger();
    private void Flip()
    {
        FacingDirection *= -1;
        base.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion
}
