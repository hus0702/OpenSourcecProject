using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject
{
    [Header("Move state")]
    public float movementVelocity = 10f;

    [Header("Jump state")]
    public float jumpVelocity = 15f;

    [Header("Check Variables")]
    public float groundCheckRadious;
    public LayerMask whatIsGround;
}
