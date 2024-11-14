using UnityEngine;

[CreateAssetMenu(fileName = "newLimbData", menuName = "Data/Limb Data/Base Data")]

public class LimbData : ScriptableObject
{
    [Header("Move state")]
    public float movementVelocity = 3f;
    [Header("bullet speed")]
    public float bulletspeed = 20f;

    [Header("Check Variables")]
    public float groundCheckRadious;
    public LayerMask whatIsGround;
    public LayerMask whitIsBlind;

    [Header("isRiding")]
    public bool isRiding;

    [Header("havingGun")]
    public bool ishavingGun;

    [Header("HoldingGun")]
    public bool HoldingGun;
    
}
