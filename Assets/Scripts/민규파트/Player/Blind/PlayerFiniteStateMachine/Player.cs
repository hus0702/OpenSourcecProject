using System.Collections;
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

    public PlayerDieState DieState { get; private set; }

    public PlayerC_holdinggunidleState c_holdinggunidleState { get; private set; }
    public PlayerC_holdinggunmoveState c_holdinggunmoveState { get; private set; }
    public PlayerDataContainer container { get; private set; }



    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform playerTransform { get; private set; }
    public GameObject detectedObject { get; private set; }
    public PlayerObjectController thisController { get; private set; }

    public SpriteRenderer spriteRenderer { get; private set; }
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

    private GameObject RecentLadder;

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
        DieState = new PlayerDieState(this, StateMachine, container, "die");
        c_holdinggunidleState = new PlayerC_holdinggunidleState(this, StateMachine, container, "C_holdinggunidle");
        c_holdinggunmoveState = new PlayerC_holdinggunmoveState(this, StateMachine, container, "C_holdinggunmove");
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
        groundcheck = transform.GetChild(0);
        myBoxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isOwned)
        {
            return;
        }

        CurrentVelocity = RB.linearVelocity;
        StateMachine.playerCurrentState.LogicUpdate();

        if (isServer)
        {
            container.position = transform.position;
        }
        else
        {
            CmdSetposition(transform.position);
        }
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

    public void changeitem(bool newvalue)
    {
        if (newvalue)
        {
            while (true)
            {
                if (isServer)
                {
                    if (container.holdingitem != 1)
                        container.holdingitem++;
                    else
                        container.holdingitem = 0;

                    if (container.itemset[container.holdingitem])
                        break;
                }
                else
                {
                    if (container.holdingitem != 1)
                        CmdSetholdingitem(container.holdingitem + 1);
                    else
                        CmdSetholdingitem(0);


                    if (container.itemset[container.holdingitem])
                        break;
                }
            }
        }
        else
        {
            while (true)
            {
                if (isServer)
                {
                    if (container.holdingitem != 0)
                        container.holdingitem--;
                    else
                        container.holdingitem = 1;

                    if (container.itemset[container.holdingitem])
                        break;
                }
                else
                {
                    if (container.holdingitem != 0)
                        CmdSetholdingitem(container.holdingitem - 1);
                    else
                        CmdSetholdingitem(1);

                    if (container.itemset[container.holdingitem])
                        break;
                }
            }
        }
    }

    public void SetBlindVisible(bool newvalue)
    {
        if (isServer)
        {
            InputHandler.enabled = newvalue;
            spriteRenderer.enabled = newvalue;
        }
        else
        {
            CmdSetBlindVisible(newvalue);
        }
    }
    public void GetCardKey(bool newvalue)
    {
        if (isServer)
        {
            container.itemset[1] = newvalue;
        }
        else
        {
            CmdGetCardkey(newvalue);
        }
    }

    IEnumerator BlinkCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < 1.0f)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // 스프라이트 ON/OFF 전환
            yield return new WaitForSeconds(0.2f); // 지정된 시간 동안 대기
            elapsed += 0.2f;
        }

        spriteRenderer.enabled = true; // 마지막에는 스프라이트를 켜둠
    }

    #endregion

    #region Check Functions
    public void CheckifShouldflip(int xinput)
    {
        if (xinput != 0 && xinput != container.facingdirection && isOwned)
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
    public GameObject CheckIftouchLadder()
    {
        // OverlapCircleAll로 충돌한 모든 콜라이더 가져오기
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            groundcheck.position,
            container.groundCheckRadious,
            container.whatIsLadder
        );

        // 충돌한 오브젝트 중 특정 Layer의 오브젝트 반환
        foreach (Collider2D collider in colliders)
        {
            if ((container.whatIsLadder & (1 << collider.gameObject.layer)) != 0) // 해당 Layer 체크
            {
                return collider.gameObject; // 조건을 만족하는 게임 오브젝트 반환
            }
        }

        return null; // 조건을 만족하는 오브젝트가 없으면 null 반환
    }

    #endregion

    #region SoundMaking Function
    public void BlindLandSound()
    {
        if (isServer)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.BlindLand);
            GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.BlindLand);
        }
        else
        {
            CmdBlindLandSound();
        }
    }

    public void BlindwalkSound()
    {
        if (isServer)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Blindwalk);
            GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.Blindwalk);
        }
        else
        {
            CmdBlindwalkSound();
        }
    }
    public void BlindclimbSound()
    {
        if (isServer)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Blindclimb);
            GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.Blindclimb);
        }
        else
        {
            CmdBlindclimbSound();
        }
    }
    public void BlindDieSound()
    {
        if (isServer)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.BlindDie);
            GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.BlindDie);
        }
        else
        {
            CmdBlindDieSound();
        }
    }

    public void BlindCarryUpSound()
    {
        if (isServer)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.BlindCarryUp);
            GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.BlindCarryUp);
        }
        else
        {
            CmdBlindCarryUpSound();
        }
    }

    public void BlindthrowSound()
    {
        if (isServer)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Blindthrow);
            GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.Blindthrow);
        }
        else
        {
            CmdBlindthrowSound();
        }
    }

    [Command]
    void CmdBlindLandSound()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.BlindLand);
        GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.BlindLand);
    }

    [Command]
    void CmdBlindwalkSound()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Blindwalk);
        GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.Blindwalk);
    }

    [Command]
    void CmdBlindclimbSound()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Blindclimb);
        GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.Blindclimb);
    }

    [Command]
    void CmdBlindDieSound()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.BlindDie);
        GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.BlindDie);
    }

    [Command]
    void CmdBlindCarryUpSound()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.BlindCarryUp);
        GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.BlindCarryUp);
    }

    [Command]
    void CmdBlindthrowSound()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Blindthrow);
        GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.Blindthrow);
    }
    #endregion

    #region animation clip call Function

    public void BlindLand()
    {
        BlindLandSound();
    }
    public void Blindwalk()
    {
        BlindwalkSound();
    }
    public void Blindclimb()
    {
        if (isServer)
        {
            RecentLadder = CheckIftouchLadder();
            CheckIftouchLadder().GetComponent<Ladder>().OnEnterLadder(this.gameObject);
        }
        else
        {
            CmdBlindclimb();
        }
    }
    public void Blindclimboff()
    {
        if (isServer)
        {
            RecentLadder.GetComponent<Ladder>().OnExitLadder(this.gameObject);
        }
        else
        {
            CmdBlindclimboff();
        }
    }

    public void BlindDie()
    {
        BlindDieSound();
    }

    public void BlindCarryUp()
    {
        BlindCarryUpSound();
    }
    public void Blindthrow()
    {
        BlindthrowSound();
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
            CmdSetFacingDirection();
        }
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }



    /*
####################################################################################################
        \  \  \      \        \\\\\\      \\  \     \      \\  \       \\\ 
        \  \  \     \\\       \\\\\\      \ \ \     \      \ \ \     \\   \\\\
         \\\\\\     \   \     \    \      \  \\     \      \  \\       \\\\  \

        황유석 팀원이 해당 코드 부분에 기생했습니다. 건들지도 않은 코드가 바뀌어 있을 위험이 있습니다.
    */
    private IInteracted objectHandlingMe = null;
    public void SetObjectHandlingMe(IInteracted interacted) { objectHandlingMe = interacted; }
    bool isInteractable = true;
    IEnumerator cooldownInteract()
    {
        isInteractable = false;
        yield return new WaitForSeconds(0.3f);
        isInteractable = true;
    }

    public void Interact()
    {
        /*
            캐비닛의 경우 플레이어의 Collider 를 비활성화시킵니다. 이러면 더 이상 상호작용이 불가능해지므로
            이런 객체들을 위해

            "플레이어의 상호작용 권리를 쥐고 있는 녀석" 을 하나 만들겠습니다.
            콜라이더가 없어도 이 객체에 대해서는 항상 상호작용을 할 수 있게 됩니다.
        */
        if (objectHandlingMe != null)
        {
            if (!isInteractable) return;
            objectHandlingMe.Interact(gameObject);
            objectHandlingMe = null;
            StartCoroutine(cooldownInteract());
            return; // 이게 그 코드입니다!
        }


        Collider2D[] colliders = Physics2D.OverlapBoxAll(myBoxCollider.bounds.center, myBoxCollider.bounds.size, 0);

        foreach (Collider2D colliderItem in colliders)
        {
            if (colliderItem != myBoxCollider)
            {
                IInteracted interacted = colliderItem.GetComponent<InteractableObject>();
                if (interacted == null)
                {
                    //Debug.Log(colliderItem.gameObject.name + " ���� ��ȣ�ۿ� ������ ������Ʈ�� ����.");
                }
                else
                {
                    if (isInteractable)
                    {
                        Debug.Log(colliderItem.gameObject.name + " 과 플레이어가 상호작용합니다.");
                        interacted.Interact(gameObject);
                        StartCoroutine(cooldownInteract()); // HUS : 이 부분도 수정됐습니다. 바바바박 상호작용이 눌리는 부분이 있어 쿨타임을 줬습니다.
                    }
                    break;
                }
            }
        }
    }
    // ###############################################################################################


    public void TakingDamage(int value)
    {
        if (isServer)
        {
            container.Hp -= value;
            StartCoroutine(BlinkCoroutine());
            RpcBlindBlink();
            if (container.iscarrying)
            {
                container.throwcall = true;
            }
        }
        else
        {
            CmdChangeHp(container.Hp-value);
            StartCoroutine(BlinkCoroutine());
            CmdBlindBlink();
            if (container.iscarrying)
            {
                CmdSetThrowCall(true);
            }
        }
    }

    public void Respawn()
    {
        if (isOwned)
        {
            if (isServer)
            {
                this.transform.position = GameManager.instance.BlindSpawnPositionOnLoad;
            }
            else
            {
                CmdBlindRespawn();
            }
        }
        else
        {
            if (isServer)
            {
                RpcBlindRespawn();
            }
            else
            {
                CmdBlindRespawn();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == container.whatIsLadder && container.isclimbing)
        {
            SetLaddderPosition(other.gameObject);
        }
    }
    #endregion

    #region ContainerCommandfunction
    [Command]
    public void CmdSetFacingDirection()
    {
        container.facingdirection *= -1;
    }
    [Command]
    public void CmdChangeHp(int newvalue)
    {
        container.Hp = newvalue;
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
    [Command]
    public void CmdSetposition(Vector3 newvalue)
    {
        container.position = newvalue;
    }
    [Command]
    public void CmdSetInteractable(bool newvalue)
    {
        container.Interactable = newvalue;
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
    public void CmdSetInteractInput(bool newvalue)
    { 
        container.InteractInput = newvalue;
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

    [Command]
    public void CmdSetholdingitem(int newvalue)
    {
        container.holdingitem = newvalue;
    }
    [Command]
    public void CmdGetCardkey(bool newvalue)
    {
        container.itemset[1] = newvalue;
    }

    [Command]
    public void CmdSetBlindVisible(bool newvalue)
    {
        InputHandler.enabled = newvalue;
        spriteRenderer.enabled = newvalue;
    }

    [Command]
    public void CmdBlindclimb()
    {
        RecentLadder = CheckIftouchLadder();
        CheckIftouchLadder().GetComponent<Ladder>().OnEnterLadder(this.gameObject);
    }

    [Command]
    public void CmdBlindclimboff()
    {
        RecentLadder.GetComponent<Ladder>().OnEnterLadder(this.gameObject);
    }

    [Command]
    public void CmdBlindBlink()
    {
        StartCoroutine(BlinkCoroutine());
    }

    [Command]
    public void CmdBlindRespawn()
    {
        this.transform.position = GameManager.instance.BlindSpawnPositionOnLoad;
    }

    [Command]
    public void CmdSetRespawncall(bool newvalue)
    {
        container.Respawncall = newvalue;
    }

    [ClientRpc]
    public void RpcBlindBlink()
    {
        StartCoroutine(BlinkCoroutine());
    }

    [ClientRpc]
    public void RpcBlindRespawn()
    {
        this.transform.position = GameManager.instance.BlindSpawnPositionOnLoad;
    }
    #endregion

}
