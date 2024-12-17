using JetBrains.Annotations;
using Mirror;
using Mirror.BouncyCastle.Asn1.BC;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDataContainer : NetworkBehaviour
{
     public float movementVelocity;
     public float sitmovementVelocity;
    [SyncVar] public float facingdirection;
     public float climbVelocity;
     public float C_movementVelocity;
     public float jumpVelocity;
     public float groundCheckRadious;

     public LayerMask whatIsGround;
     public LayerMask whatIsLimb;
     public LayerMask whatIsLadder;

    [SyncVar] public bool iscarrying;
    [SyncVar] public bool isclimbing;
    [SyncVar] public bool carryupcall;
    [SyncVar] public bool throwcall;
    [SyncVar] public bool putdowncall;
    [SyncVar] public float throwinputtime;
    [SyncVar] public Vector3 position;
    [SyncVar] public bool Interactable;
    [SyncVar] public int Hp;

    [SyncVar]public int NormInputX;
    [SyncVar]public int NormInputY;
    [SyncVar]public bool JumpInput;
    [SyncVar]public bool SitInput;
    [SyncVar]public bool ladderUp;
    [SyncVar] public bool ladderDown;
    [SyncVar] public bool InteractInput;

    public bool[] itemset; // 0번은 빈손, 1번은 카드키예정

    [SyncVar] public bool item0;
    [SyncVar] public bool item1;

    [SyncVar] public int holdingitem;
    [SyncVar] public bool Respawncall;

    [SyncVar] public bool Shotparticle;
    private void Awake()
    {
        movementVelocity = 6f;
        sitmovementVelocity = 3f;
        facingdirection = 1;
        climbVelocity = 1.5f;
        C_movementVelocity = 4f;
        jumpVelocity = 14f;
        groundCheckRadious = 0.5f;
        iscarrying = false;
        isclimbing = false;
        carryupcall = false;
        throwcall = false;
        putdowncall = false;
        throwinputtime = 0;
        Interactable = true;
        Hp = 10;
        NormInputX = 0;
        NormInputY = 0;
        JumpInput = false;
        SitInput = false;
        ladderUp = false;
        ladderDown = false;
        InteractInput = false;

        itemset = new bool[2];
        item0 = true;
        item1 = false;
        itemset[0] = item0;
        itemset[1] = item1;
        holdingitem = 0;

        Respawncall = false;
        Shotparticle = false;
    }

    private void Update()
    {
        itemset[0] = item0;
        itemset[1] = item1;
    }

}
