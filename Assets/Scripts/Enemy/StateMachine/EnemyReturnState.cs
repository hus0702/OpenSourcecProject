using UnityEngine;
public class EnemyReturnState : IEnemyState
{
    public void EnterState(EnemyBase enemy)
    {
        enemy.agent.isStopped = false;
        enemy.animator.SetTrigger("Walk");
        enemy.agent.SetDestination(enemy.transform.position); // Spawn position
    }

    public void UpdateState(EnemyBase enemy)
    {
        if (Vector3.Distance(enemy.transform.position, enemy.agent.destination) > enemy.ChaseRange)
        {
            enemy.SetState(enemy.GetStateByName("Idle"));
        }
    }

    public void ExitState(EnemyBase enemy)
    {
        Debug.Log("Exiting Return State");
    }
}
