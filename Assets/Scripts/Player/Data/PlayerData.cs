using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject
{
    [Header("Move state")]
    public float movementVelocity = 10f;

    [Header("C_Move state")]
    public float C_movementVelocity = 5f;

    [Header("Jump state")]
    public float jumpVelocity = 15f;

    [Header("Check Variables")]
    public float groundCheckRadious;
    public LayerMask whatIsGround;
    public LayerMask whatIsLimb;

    [Header("isCarrying")]
    public bool iscarrying;

    [Header("InputCall")]
    public bool carryupcall;
    public bool throwcall;
    public bool putdowncall;

    [Header("BlindTransform")]
    public Transform blindtransform;


}
