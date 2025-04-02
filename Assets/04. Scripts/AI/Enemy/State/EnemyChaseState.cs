using UnityEngine;

public class EnemyChaseState : ChaseState<Enemy>
{
    public override void Enter(Enemy enemy)
    {
        base.Enter(enemy);
        enemy.agent.isStopped = false;
    }
    
    public override void Update(Enemy enemy)
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.target.position);
        
        if (enemy.target != null)
        {
            if (enemy.agent != null)
            {
                enemy.agent.SetDestination(enemy.target.position);
            }
        }

        if (enemy.target == null || distance >= enemy.enemyData.detectionRange)
        {
            enemy.ChangeState(new EnemyIdleState());
        }
        else if (distance <= enemy.enemyData.attackRange)
        {
            Vector3 direction = enemy.target.position - enemy.transform.position;
            direction.y = 0;
            float angleDifference = Vector3.Angle(enemy.transform.forward, direction);

            if (angleDifference > enemy.enemyData.angleOffset)
                return;
            
            enemy.ChangeState(new EnemyAttackState());
        }
    }
}
