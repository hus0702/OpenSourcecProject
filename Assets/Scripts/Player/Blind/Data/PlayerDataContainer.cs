using JetBrains.Annotations;
using Mirror;
using Mirror.BouncyCastle.Asn1.BC;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDataContainer : NetworkBehaviour
{
     public float movementVelocity = 10f;
     public float sitmovementVelocity = 5f;
    [SyncVar] public float facingdirection = 1;
     public float climbVelocity = 3;
     public float C_movementVelocity = 5f;
     public float jumpVelocity = 15f;
     public float groundCheckRadious = 0.5f;
     public LayerMask whatIsGround;
     public LayerMask whatIsLimb;
     public LayerMask whatIsLadder;
    [SyncVar] public bool iscarrying = false;
    [SyncVar] public bool isclimbing = false;
    [SyncVar] public bool carryupcall = false;
    [SyncVar] public bool throwcall = false;
    [SyncVar] public bool putdowncall = false;
    [SyncVar] public float throwinputtime = 0;
    [SyncVar] public Vector3 position;

    [SyncVar]public int NormInputX = 0;
    [SyncVar]public int NormInputY = 0;
    [SyncVar]public bool JumpInput = false;
    [SyncVar]public bool SitInput = false;
    [SyncVar]public bool ladderUp = false;
    [SyncVar]public bool ladderDown = false;



   

}
