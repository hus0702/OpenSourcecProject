using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]

public class D_Entity : ScriptableObject
{
    public float wallCheckDistance = 0.4f;
    public float ledgeCheckDistance = 0.6f;

    public float minAgroDistance = 3f;
    public float maxAgroDistance = 5f;

    public float attackRange = 0.5f;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
