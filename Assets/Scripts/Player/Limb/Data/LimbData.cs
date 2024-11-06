using UnityEngine;

[CreateAssetMenu(fileName = "newLimbData", menuName = "Data/Limb Data/Base Data")]

public class LimbData : ScriptableObject
{
    [Header("Move state")]
    public float movementVelocity = 3f;

    [Header("Check Variables")]
    public float groundCheckRadious;
    public LayerMask whatIsGround;
    public LayerMask whitIsBlind;

    [Header("isRiding")]
    public bool isRiding;
}
