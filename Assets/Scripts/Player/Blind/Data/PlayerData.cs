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

    [Header("InputCall")]
    public bool carryupcall;
    public bool throwcall;
    public bool putdowncall;
    public float throwinputtime;

    [Header("BlindTransform")]
    public Transform blindtransform;


}
