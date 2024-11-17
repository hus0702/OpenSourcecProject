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
    public LayerMask whatIsBlind;

    [Header("isRiding")]
    public bool isRiding;

    [Header("havingGun")]
    public bool ishavingGun;
    public bool HoldingGun;
    public float ShotDelay = 0.1f;

    [Header("Input")]
    public int NormInputX;
    public int NormInputY;
    public bool JumpInput;
    public bool SitInput;
    public bool attackInput;
    public Vector3 mousePosition;

}
