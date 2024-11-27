using Mirror;
using UnityEngine;
public abstract class EnemyBase : NetworkBehaviour
{
    [Header("Base Properties")]
    public float HP = 100f;
    public float Speed = 3.5f;
    public float ChaseRange = 10f;
    public float VisionRange = 8f;
    public bool isScout = false;
    public string currentStateName;

    [HideInInspector] public Transform player;
    protected internal Animator animator;

    protected IEnemyState currentState;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player")?.transform;

        SetState(GetStateByName("Idle")); 
    }

    protected virtual void Update()
    {
        currentState?.UpdateState(this);
    }


    public void SetState(IEnemyState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentStateName = newState.GetType().Name;
        currentState.EnterState(this);
    }

    public IEnemyState GetStateByName(string stateName)
    {
        switch (stateName)
        {
            case "Idle": return new EnemyIdleState();
            case "Chase": return new EnemyChaseState();
            case "Return": return new EnemyReturnState();
            case "Attack": return new EnemyAttackState();
            case "Patrol" : return new EnemyPatrolState();
            default: return null;
        }
    }
}
