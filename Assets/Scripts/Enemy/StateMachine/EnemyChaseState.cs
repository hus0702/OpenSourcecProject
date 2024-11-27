using UnityEngine;

public class ChaseState : IEnemyState
{
    public void EnterState(EnemyBase enemy)
    {
        Debug.Log("Entering Chase State");
        if (enemy.agent.isOnNavMesh)
        {
            enemy.agent.isStopped = false; // �̵� Ȱ��ȭ
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
            enemy.SetState(new ReturnState()); // ���� ����� ReturnState�� ��ȯ
        }
        else if (distanceToPlayer <= enemy.agent.stoppingDistance)
        {
            enemy.SetState(new AttackState()); // ���� ������ ���� AttackState�� ��ȯ
        }
        else
        {
            // �÷��̾ ���󰡵��� ��� ����
            enemy.agent.SetDestination(enemy.player.position);

            // �ִϸ��̼� �ӵ� ������Ʈ
            enemy.animator.SetFloat("MoveSpeed", enemy.agent.velocity.magnitude);
        }
    }

    public void ExitState(EnemyBase enemy)
    {
        Debug.Log("Exiting Chase State");
        if (enemy.agent.isOnNavMesh)
        {
            enemy.agent.isStopped = true; // �̵� ����
            enemy.agent.ResetPath(); // ��� �ʱ�ȭ
        }
    }
}
