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
    private SpriteRenderer ShotParticle;
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
        ShotParticle.enabled = false;

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
        
        if (isServer)
        {
            if (container.holdingitem == 0)
            {
                if (container.itemset[1])
                    container.holdingitem = 1;
            }
            else
            {
                container.holdingitem = 0;
            }
        }
        else 
        {
            if (container.holdingitem == 0)
            {
                if (container.itemset[1])
                {
                    CmdSetholdingitem(1);
                }
            }
            else
                CmdSetholdingitem(0);
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

    public void LimbchangeColor(UnityEngine.Color color)
    {
        spriteRenderer.color = color;
        if (isServer)
        {
            RpcLimbchangeColor(color);
        }
        else
        {
            CmdLimbchangeColor(color);
        }
    }

    IEnumerator BlinkCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < 1.0f)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // ��������Ʈ ON/OFF ��ȯ
            yield return new WaitForSeconds(0.2f); // ������ �ð� ���� ���
            elapsed += 0.2f;
        }

        spriteRenderer.enabled = true; // ���������� ��������Ʈ�� �ѵ�
    }

    IEnumerator ShotspriteCoroutine()
    {
        float elapsed = 0f;
        if (container.isRiding)
        {
            if (isServer)
            {
                GameManager.instance.Pdcontainer.Shotparticle = true;
            }
            else
            {
                CmdSetPdShotParticle(true);
            }
            while (elapsed < 0.1f)
            {
                yield return new WaitForSeconds(0.1f);
                elapsed += 0.1f;
            }
            if (isServer)
            {
                GameManager.instance.Pdcontainer.Shotparticle = false;
            }
            else
            {
                CmdSetPdShotParticle(false);
            }
        }
        else
        {
            while (elapsed < 0.2f)
            {
                ShotParticle.enabled = true;
                yield return new WaitForSeconds(0.1f);
                elapsed += 0.1f;
            }
        }



        ShotParticle.enabled = false;

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

        SWM.Instance.MakeSoundwave((int)AudioManager.Sfx.LimpLand, true, groundcheck.gameObject, 5f, 0.8f);
        return;


        
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


        SWM.Instance.MakeSoundwave((int)AudioManager.Sfx.LimpShot, true, gameObject, 6f, 0.8f);
        return;


        if (isOwned)
        {
            if (isServer)
            {
                AudioManager.instance.PlaySfx(AudioManager.Sfx.LimpShot); // 일단 내꺼 소리내고
                GameManager.instance.RpcPlaySoundOnClient(AudioManager.Sfx.LimpShot); // 모든 클라이언트에 소리내라.
            }
            else
            {
                CmdLimpShotSound();
            }
        }
    }

    public void LimpDieSound()
    {


        SWM.Instance.MakeSoundwave((int)AudioManager.Sfx.LimpDie, true, gameObject, 5f, 0.8f);
        return;


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
            if (container.isRiding)
                return;
        StartCoroutine(ShotspriteCoroutine());
        if (isServer)
        {
            Vector2 bulletDirection = new Vector2(bulletrotation.x, bulletrotation.y).normalized;

            if (container.isRiding && container.holdingitem == 1)
            {
                bulletspawnSpot = GameManager.instance.Pdcontainer.position + new Vector3(GameManager.instance.Pdcontainer.facingdirection * 0.49f, 0.282f, 0);
                if (bulletDirection.y > 0.3f)
                    bulletDirection.y = 0.3f;
                else if (bulletDirection.y < -0.3f)
                    bulletDirection.y = -0.3f;

                bulletDirection = bulletDirection.normalized;
            }
            else
            {
                bulletspawnSpot = transform.position + new Vector3(container.FacingDirection*0.72f, -0.333f, 0);
                bulletDirection = new Vector2(container.FacingDirection, 0);
            }
            GameObject bullet = Instantiate(BulletPrefab, bulletspawnSpot, bulletrotation);
            // ���� ���� (Quaternion���� ���ͷ� ��ȯ)
            
            // �Ѿ� �ӵ� ����
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
        if (isServer)
        {
            LimpDieSound();
        }
        StartCoroutine(ChangeColorOverTime(Color.white, Color.gray, 0.2f));
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

    /*
####################################################################################################
        \  \  \      \        \\\\\\      \\  \     \      \\  \       \\\ 
        \  \  \     \\\       \\\\\\      \ \ \     \      \ \ \     \\   \\\\
         \\\\\\     \   \     \    \      \  \\     \      \  \\       \\\\  \

        황유석 팀원이 해당 코드 부분에 기생했습니다. 건들지도 않은 코드가 바뀌어 있을 위험이 있습니다.
    */
    private IInteracted objectHandlingMe = null;
    public void SetObjectHandlingMe(IInteracted interacted) 
    {
        Debug.Log("플레이어의 상호작용 주도권이 다른 객체로 넘어갔습니다.");
        objectHandlingMe = interacted; 
    }
    public void Interact()
    {
        if (isServer)
        {
            container.InteractInput = false;
        }
        else
        {
            CmdSetInteractInput(false);
        }

        Debug.Log("플레이어가 상호작용을 시도 : E ");
        /*
            캐비닛의 경우 플레이어의 Collider 를 비활성화시킵니다. 이러면 더 이상 상호작용이 불가능해지므로
            이런 객체들을 위해

            "플레이어의 상호작용 권리를 쥐고 있는 녀석" 을 하나 만들겠습니다.
            콜라이더가 없어도 이 객체에 대해서는 항상 상호작용을 할 수 있게 됩니다.
        */
        if (objectHandlingMe != null)
        {
            objectHandlingMe.Interact(gameObject);
            objectHandlingMe = null;
            return; // 이게 그 코드입니다!
        }


        Collider2D[] colliders = Physics2D.OverlapBoxAll(myBoxCollider.bounds.center, myBoxCollider.bounds.size, 0);

        Debug.Log("감지된 콜라이더 개수 : " + colliders.Length);

        foreach (Collider2D colliderItem in colliders)
        {
            if (colliderItem != myBoxCollider)
            {
                IInteracted interacted = colliderItem.GetComponent<InteractableObject>();
                if (interacted == null)
                {
                    //Debug.Log(colliderItem.gameObject.name + " ���� ��ȣ�ۿ� ������ ������Ʈ�� ����.");
                    Debug.Log(colliderItem.gameObject.name + "은 상호작용 가능한 객체가 아닙니다.");
                }
                else
                {
                    Debug.Log(colliderItem.gameObject.name + " 과 플레이어가 상호작용합니다.");
                    interacted.Interact(gameObject);
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
            Debug.Log("Damage���� " + value);
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
        StartCoroutine(ChangeColorOverTime(Color.gray, Color.white, 0.2f));
        if (isOwned)
        {
            if (isServer)
            {
                this.transform.position = GameManager.instance.LimpSpawnPositionOnLoad;
                RpcLimbRespawn();
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

    IEnumerator ChangeColorOverTime(Color fromColor, Color toColor, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            spriteRenderer.color = Color.Lerp(fromColor, toColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime; // 시간 누적
            yield return null; // 다음 프레임까지 대기
        }
        spriteRenderer.color = toColor; // 최종 색상 적용
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
        StartCoroutine(ShotspriteCoroutine());
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
        // ���� ���� (Quaternion���� ���ͷ� ��ȯ)

        // �Ѿ� �ӵ� ����
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
        StartCoroutine(ChangeColorOverTime(Color.gray, Color.white, 0.2f));
        this.transform.position = GameManager.instance.LimpSpawnPositionOnLoad;
    }

    [Command]
    public void CmdSetRespawncall(bool newvalue)
    { 
        container.Respawncall = newvalue;
        GameManager.instance.Pdcontainer.Hp = 10000;
    }

    [Command]
    public void CmdLimbchangeColor(UnityEngine.Color color)
    {
        spriteRenderer.color = color;
    }

    [Command]
    public void CmdSetPdShotParticle(bool newvalue)
    { 
        GameManager.instance.Pdcontainer.Shotparticle = newvalue;
    }

    [ClientRpc]
    public void RpcSetSpriteRenderer(bool newvalue)
    {
        Debug.Log("�� �߻�!!");
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
        StartCoroutine(ShotspriteCoroutine());
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
        // ���� ���� (Quaternion���� ���ͷ� ��ȯ)

        // �Ѿ� �ӵ� ����
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
        StartCoroutine(ChangeColorOverTime(Color.gray, Color.white, 0.2f));
    }

    [ClientRpc]
    public void RpcLimbchangeColor(UnityEngine.Color color)
    {
        spriteRenderer.color = color;
    }
    #endregion
}
