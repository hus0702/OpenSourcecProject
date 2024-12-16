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

    [SyncVar] public bool[] itemset; // 0번은 빈손, 1번은 카드키예정
    [SyncVar] public int holdingitem;

    private void Awake()
    {
        movementVelocity = 8f;
        sitmovementVelocity = 4f;
        facingdirection = 1;
        climbVelocity = 3;
        C_movementVelocity = 5f;
        jumpVelocity = 15f;
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
        itemset[0] = true;
        holdingitem = 0;

    }


}
