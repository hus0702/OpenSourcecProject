using Mirror;
using Mirror.BouncyCastle.Asn1.TeleTrust;
using Steamworks;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
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
    public LimbDieState DieState { get; private set; }
    public LimbholdinggunidleState holdinggunidleState { get; private set; }

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
        DieState = new LimbDieState(this, StateMachine, container, "die");
        holdinggunidleState = new LimbholdinggunidleState(this, StateMachine, container, "holdinggunidle");
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
        groundcheck = transform.GetChild(0);
        myBoxCollider = GetComponent<BoxCollider2D>();


    }

    private void Update()
    {
        if (!isOwned)
        {
            return;
        }
        CurrentVelocity = RB.linearVelocity;
        StateMachine.LimbCurrentState.LogicUpdate();

        if (container.isRiding)
        {
            this.limbtransform.position = (GameManager.instance.Pdcontainer.position + new Vector3(0, 0.1f, 0));
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

    public void changeitem(bool newvalue)
    {
        if (newvalue)
        {
            int k = 0;
            while (k < 4)
            {
                if (isServer)
                {
                    if (container.holdingitem != 2)
                        container.holdingitem++;
                    else
                        container.holdingitem = 0;

                    if (container.itemset[container.holdingitem])
                        break;
                }
                else 
                {
                    if (container.holdingitem != 2)
                        CmdSetholdingitem(container.holdingitem + 1);
                    else
                        CmdSetholdingitem(0);


                    if (container.itemset[container.holdingitem])
                    {
                        break;
                    }
                    else
                    {
                        k++;
                    }
                       
                }
            }
        }
        else 
        {
            int k = 0;
            while (k < 4)
            {
                if (isServer)
                {
                    if (container.holdingitem != 0)
                        container.holdingitem--;
                    else
                        container.holdingitem = 2;

                    if (container.itemset[container.holdingitem])
                        break;
                }
                else
                {
                    if (container.holdingitem != 0)
                        CmdSetholdingitem(container.holdingitem - 1);
                    else
                        CmdSetholdingitem(2);

                    if (container.itemset[container.holdingitem])
                        break;
                    else { k++; }
                }
            }
        }
    }

    public void SetLimbVisible(bool newvalue)
    {
        if (isServer)
        {
            InputHandler.enabled = newvalue;
            spriteRenderer.enabled = newvalue;
        }
        else
        {
            CmdSetLimbVisible(newvalue);
        }
    }
    public void GetCardKey(bool newvalue)
    {
        if (isServer)
        {
            container.itemset[2] = newvalue;
        }
        else
        {
            CmdGetCardkey(newvalue);
        }
    }

    public void GetGun(bool newvalue)
    {
        if (isServer)
        {
            container.itemset[1] = newvalue;
        }
        else
        {
            CmdGetGun(newvalue);
        }
    }

    
    #endregion

    #region Check Functions
    public void CheckifShouldflip(int xinput)
    {
        if (xinput != 0 && xinput != container.FacingDirection && isOwned)
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
        return Physics2D.OverlapCircle(groundcheck.position, container.groundCheckRadious, container.whatIsBlind);
    }

    public bool ishaveCardKey()
    {
        return container.itemset[2];
    }

    public bool ishaveGun()
    {
        return container.itemset[1];
    }
    #endregion

    #region SoundMaking Function
    public void LimpLandSound()
    {
        GameManager.instance.CmdPlaySoundOnClient(AudioManager.Sfx.LimpLand);
    }
    public void LimpShotSound()
    {
        GameManager.instance.CmdPlaySoundOnClient(AudioManager.Sfx.LimpShot);
    }

    public void LimpDieSound()
    {
        GameManager.instance.CmdPlaySoundOnClient(AudioManager.Sfx.LimpDie);
    }

    #endregion

    #region animation clip call Function

    public void LimpLand()
    {
        LimpLandSound();
    }

    public void LimpShot()
    {
        LimpShotSound();
    }
    public void LimpDie()
    {
        LimpDieSound();
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

    public void Interact()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(myBoxCollider.bounds.center, myBoxCollider.bounds.size, 0);

        foreach (Collider2D colliderItem in colliders)
        {
            if (colliderItem != myBoxCollider)
            {
                IInteracted interacted = colliderItem.GetComponent<InteractableObject>();
                if (interacted == null)
                {
                    Debug.Log(colliderItem.gameObject.name + " 에는 상호작용 가능한 컴포넌트가 없음.");
                }
                else
                {
                    interacted.Interact(gameObject);
                }
            }
        }
    }

    public void TakingDamage(int value)
    {
        if (isServer)
        {
            container.Hp -= value;
        }
        else
        { 
            CmdChangeHp(-value);
        }
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
    public void CmdChangeHp(int newvalue)
    {
        container.Hp += newvalue;
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

    [Command]
    public void CmdSetSpriteRenderer(bool newvalue)
    { 
        this.GetComponent<SpriteRenderer>().enabled = newvalue;
        RpcSetSpriteRenderer(newvalue);
    }
    [Command]
    public void CmdSetInteractable(bool newvalue)
    {
        container.Interactable = newvalue;
    }
    [Command]
    public void CmdSetInteractInput(bool newvalue)
    {
        container.InteractInput = newvalue;
    }

    [Command]
    public void CmdSetholdingitem(int newvalue)
    { 
        container.holdingitem = newvalue;
    }
    [Command]
    public void CmdGetCardkey(bool newvalue)
    {
        container.itemset[2] = newvalue;
    }

    [Command]
    public void CmdGetGun(bool newvalue)
    {
        container.itemset[1] = newvalue;
    }

    [Command]
    public void CmdSetLimbVisible(bool newvalue)
    {
        InputHandler.enabled = newvalue;
        spriteRenderer.enabled = newvalue;
    }
    [ClientRpc]
    public void RpcSetSpriteRenderer(bool newvalue)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = newvalue; 
        }
    }
    #endregion
}
