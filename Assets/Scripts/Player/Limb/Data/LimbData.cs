using UnityEngine;

[CreateAssetMenu(fileName = "newLimbData", menuName = "Data/Limb Data/Base Data")]

public class LimbData : ScriptableObject
{
    [Header("Constants")]
    [Header("Move state")]
    public float movementVelocity = 3f;
    [Header("bullet speed")]
    public float bulletspeed = 20f;

    [Header("Check Variables")]
    public float groundCheckRadious;
    public LayerMask whatIsGround;
    public LayerMask whatIsBlind;

    [Header("SyncVar From Now")]
    [Header("transform")]
    public int FacingDirection = 1;
    public Vector3 position;

    [Header("isRiding")]
    public bool isRiding;

    [Header("havingGun")]
    public bool ishavingGun;
    public bool HoldingGun;
    public float ShotDelay = 0.5f;

    [Header("Input")]
    public int NormInputX;
    public int NormInputY;
    public bool JumpInput;
    public bool SitInput;
    public bool attackInput = false;
    public Vector3 mousePosition;

}
