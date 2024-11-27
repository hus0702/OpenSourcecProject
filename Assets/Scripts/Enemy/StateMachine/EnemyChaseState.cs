using UnityEngine;

public class ChaseState : IEnemyState
{
    public void EnterState(EnemyBase enemy)
    {
        Debug.Log("Entering Chase State");
        if (enemy.agent.isOnNavMesh)
        {
            enemy.agent.isStopped = false; // 이동 활성화
        }
    }

    public void UpdateState(EnemyBase enemy)
    {
        if (enemy.player == null || !enemy.agent.isOnNavMesh)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (distanceToPlayer > enemy.ChaseRange)
        {
            enemy.SetState(new ReturnState()); // 범위 벗어나면 ReturnState로 전환
        }
        else if (distanceToPlayer <= enemy.agent.stoppingDistance)
        {
            enemy.SetState(new AttackState()); // 공격 범위에 들어가면 AttackState로 전환
        }
        else
        {
            // 플레이어를 따라가도록 경로 설정
            enemy.agent.SetDestination(enemy.player.position);

            // 애니메이션 속도 업데이트
            enemy.animator.SetFloat("MoveSpeed", enemy.agent.velocity.magnitude);
        }
    }

    public void ExitState(EnemyBase enemy)
    {
        Debug.Log("Exiting Chase State");
        if (enemy.agent.isOnNavMesh)
        {
            enemy.agent.isStopped = true; // 이동 정지
            enemy.agent.ResetPath(); // 경로 초기화
        }
    }
}
