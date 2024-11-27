using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    public void EnterState(EnemyBase enemy)
    {
        Debug.Log("Entering Idle State");
        enemy.animator.SetTrigger("Idle");
    }

    public void UpdateState(EnemyBase enemy)
    {
        if (enemy.isScout) 
        {
            enemy.SetState(enemy.GetStateByName("Patrol"));
        }
        else if (Vector3.Distance(enemy.transform.position, enemy.player.position) < enemy.VisionRange)
        {
            enemy.SetState(enemy.GetStateByName("Chase"));
        }
    }

    public void ExitState(EnemyBase enemy)
    {
        Debug.Log("Exiting Idle State");
    }
}
