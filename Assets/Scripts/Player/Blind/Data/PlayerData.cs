using Mirror;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject
{
    [Header("Move state")]
    public float movementVelocity = 10f;
    public float sitmovementVelocity = 5f;
    public float facingdirection = 1;
    public float climbVelocity = 3;

    [Header("C_Move state")]
    public float C_movementVelocity = 5f;

    [Header("Jump state")]
    public float jumpVelocity = 15f;

    [Header("Check Variables")]
    public float groundCheckRadious;
    public LayerMask whatIsGround;
    public LayerMask whatIsLimb;
    public LayerMask whatIsLadder;

    [Header("Statement")]
    public bool iscarrying;
    public bool isclimbing;

    [Header("Input")]
    public bool carryupcall = false;
    public bool throwcall = false;
    public bool putdowncall = false;
    public float throwinputtime = 0f;

    public int NormInputX;
    public int NormInputY;
    public bool JumpInput;
    public bool SitInput;
    public bool ladderUp;
    public bool ladderDown;

    [Header("BlindTransform")]
    public Transform blindtransform;
}
