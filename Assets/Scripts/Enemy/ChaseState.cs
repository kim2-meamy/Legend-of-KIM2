using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ChaseState : IEnemyState
{
    public void Enter(Enemy enemy)
    {
        Debug.Log("Enter Chase");
        enemy.agent.isStopped = false;
        enemy.animator.SetBool("isChase", true);
    }

    public void Update(Enemy enemy)
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.target.position);
        Debug.Log($"distance: {distance}, attackRange: {enemy.stats.attackRange}");
        if (enemy.target == null || distance >= enemy.stats.detectionRange)
        {
            enemy.ChangeState(new IdleState());
        }
        else if (distance <= enemy.stats.attackRange)
        {
            enemy.ChangeState(new AttackState());
        }
    }

    public void FixedUpdate(Enemy enemy)
    {
        if (enemy.target != null)
        {
            if (enemy.agent != null)
            {
                enemy.agent.SetDestination(enemy.target.position);
            }
        }
    }

    public void Exit(Enemy enemy)
    {
        Debug.Log("Exit Chase");
        if (enemy.agent != null)
        {
            enemy.agent.isStopped = true;
        }
    }
}
