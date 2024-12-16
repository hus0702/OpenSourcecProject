using Mirror;
using System.Collections;
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

    Vector3 bulletspawnSpot;
    Quaternion bulletrotation;
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
        if (isOwned)
        {
            if (isServer)
            {
                AudioManager.instance.PlaySfx(AudioManager.Sfx.LimpLand);
                GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.LimpLand);
            }
            else
            {
                CmdLimpLandSound();
            }
        }
    }
    public void LimpShotSound()
    {
        if (isOwned)
        {
            if (isServer)
            {
                AudioManager.instance.PlaySfx(AudioManager.Sfx.LimpShot);
                GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.LimpShot);
            }
            else
            {
                CmdLimpShotSound();
            }
        }
    }

    public void LimpDieSound()
    {
        if (isServer)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.LimpDie);
            GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.LimpDie);
        }
        else
        {
            CmdLimpDieSound();
        }
    }

    [Command]
    void CmdLimpLandSound()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LimpLand);
        GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.LimpLand);
    }

    [Command]
    void CmdLimpShotSound()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LimpShot);
        GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.LimpShot);
    }

    [Command]
    void CmdLimpDieSound()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LimpDie);
        GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.LimpDie);
    }
    #endregion

    #region animation clip call Function

    public void LimpLand()
    {
        LimpLandSound();
    }

    public void LimpShot()
    {
        InputHandler.StartCoroutine(InputHandler.stopshotinput(container.ShotDelay));

        bulletrotation = Quaternion.Euler(container.mousePosition - transform.position);

        if ((bulletrotation.x > 0 && GameManager.instance.Pdcontainer.facingdirection < 0) || (bulletrotation.x < 0 && GameManager.instance.Pdcontainer.facingdirection > 0))
            return;

        if (isServer)
        {
            
            Vector2 bulletDirection = new Vector2(bulletrotation.x, bulletrotation.y).normalized;

            if (container.isRiding && container.holdingitem == 1)
            {
                bulletspawnSpot = GameManager.instance.Pdcontainer.position + new Vector3(GameManager.instance.Pdcontainer.facingdirection, 0, 0);
                if (bulletDirection.y > 0.3f)
                    bulletDirection.y = 0.3f;
                else if (bulletDirection.y < -0.3f)
                    bulletDirection.y = -0.3f;

                bulletDirection = bulletDirection.normalized;
            }
            else
            {
                bulletspawnSpot = transform.position + new Vector3(container.FacingDirection, -0.4f, 0);
                bulletDirection.y = 0;
                bulletDirection.x = container.FacingDirection;
            }
            GameObject bullet = Instantiate(BulletPrefab, bulletspawnSpot, bulletrotation);
            // 방향 설정 (Quaternion에서 벡터로 변환)
            
            // 총알 속도 설정
            bullet.GetComponent<Rigidbody2D>().linearVelocity = bulletDirection * container.bulletspeed;
            RpcLimbShot();
        }
        else
        {
            CmdLimbShot();
        }
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
        if (isServer)
        {
            container.InteractInput = false;
        }
        else
        {
            CmdSetInteractInput(false);
        }
    }

    public void TakingDamage(int value)
    {
        if (isServer)
        {
            Debug.Log("Damage받음 " + value);
            container.Hp -= value;
            StartCoroutine(BlinkCoroutine());
            RpcLimbBlink();
        }
        else
        { 
            CmdChangeHp(container.Hp-value);
            StartCoroutine(BlinkCoroutine());
            CmdLimbBlink();
        }
    }

    public void Respawn()
    {
        if (isOwned)
        {
            if (isServer)
            {
                this.transform.position = GameManager.instance.LimpSpawnPositionOnLoad;
            }
            else
            {
                CmdLimbRespawn();
            }
        }
        else
        {
            if (isServer)
            {
                RpcLimbRespawn();
            }
            else
            {
                CmdLimbRespawn();
            }
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
        container.Hp = newvalue;
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
        container.mousePosition.z = 0;
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
    void CmdSetholdingitem(int newvalue)
    { 
        container.holdingitem = newvalue;
    }
    [Command]
    void CmdGetCardkey(bool newvalue)
    {
        container.itemset[2] = newvalue;
    }

    [Command]
    void CmdGetGun(bool newvalue)
    {
        container.itemset[1] = newvalue;
    }

    [Command]
    void CmdSetLimbVisible(bool newvalue)
    {
        InputHandler.enabled = newvalue;
        spriteRenderer.enabled = newvalue;
    }

    [Command]
    void CmdLimbShot()
    {
        bulletrotation = Quaternion.Euler(container.mousePosition - transform.position);

        Vector2 bulletDirection = new Vector2(bulletrotation.x, bulletrotation.y).normalized;

        if (container.isRiding && container.holdingitem == 1)
        {
            bulletspawnSpot = GameManager.instance.Pdcontainer.position + new Vector3(GameManager.instance.Pdcontainer.facingdirection, 0, 0);
            if (bulletDirection.y > 0.3f)
                bulletDirection.y = 0.3f;
            else if (bulletDirection.y < -0.3f)
                bulletDirection.y = -0.3f;

            bulletDirection = bulletDirection.normalized;
        }
        else
        {
            bulletspawnSpot = transform.position + new Vector3(container.FacingDirection, -0.4f, 0);
            bulletDirection.y = 0;
            bulletDirection.x = container.FacingDirection;
        }
        GameObject bullet = Instantiate(BulletPrefab, bulletspawnSpot, bulletrotation);
        // 방향 설정 (Quaternion에서 벡터로 변환)

        // 총알 속도 설정
        bullet.GetComponent<Rigidbody2D>().linearVelocity = bulletDirection * container.bulletspeed;
        RpcLimbShot();
    }

    [Command]
    void CmdLimbBlink()
    {
        StartCoroutine(BlinkCoroutine());
    }

    [Command]
    void CmdLimbRespawn()
    {
        this.transform.position = GameManager.instance.LimpSpawnPositionOnLoad;
    }

    [Command]
    public void CmdSetRespawncall(bool newvalue)
    { 
        container.Respawncall = newvalue;
    }
    [ClientRpc]
    public void RpcSetSpriteRenderer(bool newvalue)
    {
        Debug.Log("총 발사!!");
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = newvalue; 
        }
    }

    [ClientRpc]
    public void RpcLimbShot()
    {
        bulletrotation = Quaternion.Euler(container.mousePosition - transform.position);

        Vector2 bulletDirection = new Vector2(bulletrotation.x, bulletrotation.y).normalized;

        if (container.isRiding && container.holdingitem == 1)
        {
            bulletspawnSpot = GameManager.instance.Pdcontainer.position + new Vector3(GameManager.instance.Pdcontainer.facingdirection, 0, 0);
            if (bulletDirection.y > 0.3f)
                bulletDirection.y = 0.3f;
            else if (bulletDirection.y < -0.3f)
                bulletDirection.y = -0.3f;

            bulletDirection = bulletDirection.normalized;
        }
        else
        {
            bulletspawnSpot = transform.position + new Vector3(container.FacingDirection, -0.4f, 0);
            bulletDirection.y = 0;
            bulletDirection.x = container.FacingDirection;
        }
        GameObject bullet = Instantiate(BulletPrefab, bulletspawnSpot, bulletrotation);
        // 방향 설정 (Quaternion에서 벡터로 변환)

        // 총알 속도 설정
        bullet.GetComponent<Rigidbody2D>().linearVelocity = bulletDirection * container.bulletspeed;
    }

    [ClientRpc]
    public void RpcLimbBlink()
    {
        StartCoroutine(BlinkCoroutine());
    }

    [ClientRpc]
    public void RpcLimbRespawn()
    {
        this.transform.position = GameManager.instance.LimpSpawnPositionOnLoad;
    }
    #endregion
}
