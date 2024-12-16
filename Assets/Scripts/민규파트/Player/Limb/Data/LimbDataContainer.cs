using Mirror;
using UnityEngine;

public class LimbDataContainer : NetworkBehaviour
{
    public LimbData limbData;

    public float movementVelocity;
    public float bulletspeed;
    public float groundCheckRadious; 
    public LayerMask whatIsGround;
    public LayerMask whatIsBlind;

    [SyncVar] public int FacingDirection;
    [SyncVar] public Vector3 position;
    [SyncVar] public bool isRiding;
    [SyncVar] public float ShotDelay;
    [SyncVar] public int NormInputX;
    [SyncVar] public int NormInputY;
    [SyncVar] public bool JumpInput;
    [SyncVar] public bool SitInput;
    [SyncVar] public bool attackInput;
    [SyncVar] public Vector3 mousePosition;
    [SyncVar] public int Hp;
    [SyncVar] public bool InteractInput;
    [SyncVar] public bool Interactable;

    [SyncVar] public bool[] itemset; // 0번은 빈손, 1번은 총, 2번은 카드키 예정
    [SyncVar] public int holdingitem;

    [SyncVar] public bool Respawncall;

    private void Awake()
    {
        movementVelocity = 3f;
        bulletspeed = 20f;
        groundCheckRadious = 0.5f;
        FacingDirection = 1;
        isRiding = false;
        ShotDelay = 0.3f;
        NormInputX = 0;
        NormInputY = 0;
        JumpInput = false;
        SitInput = false;
        attackInput = false;
        Hp = 10;
        InteractInput = false;
        Interactable = true;

        itemset = new bool[3];
        itemset[0] = true;
        itemset[1] = true;
        holdingitem = 0;

        Respawncall = false;
    }

}
