using UnityEngine;
public class EnemyAttackState : IEnemyState
{
    public void EnterState(EnemyBase enemy)
    {
        Debug.Log("Entering Attack State");
        enemy.agent.isStopped = true;
        enemy.animator.SetTrigger("Attack");
    }

    public void UpdateState(EnemyBase enemy)
    {
        var player = GameObject.FindWithTag("Player");

        // Attack logic
        if (Vector3.Distance(enemy.transform.position, player.transform.position) > 2f)
        {
            enemy.SetState(new EnemyChaseState());
        }
    }

    public void ExitState(EnemyBase enemy)
    {
        Debug.Log("Exiting Attack State");
    }
}
