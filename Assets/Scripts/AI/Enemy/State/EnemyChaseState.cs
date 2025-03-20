using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyChaseState : ChaseState<Enemy>
{
    public override void Update(Enemy enemy)
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.target.position);
        Debug.Log($"distance: {distance}, attackRange: {enemy.stats.attackRange}");
        if (enemy.target == null || distance >= enemy.stats.detectionRange)
        {
            enemy.ChangeState(new EnemyIdleState());
        }
        else if (distance <= enemy.stats.attackRange)
        {
            enemy.ChangeState(new EnemyAttackState());
        }
    }

    public override void FixedUpdate(Enemy enemy)
    {
        if (enemy.target != null)
        {
            if (enemy.agent != null)
            {
                enemy.agent.SetDestination(enemy.target.position);
            }
        }
    }
}
