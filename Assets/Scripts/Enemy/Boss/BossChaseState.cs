using UnityEngine;

public class BossChaseState : ChaseState
{
    public override void Update(Enemy enemy)
    {
        Vector3 dir = enemy.target.position - enemy.transform.position;
        dir.y = 0f;
        if (dir != Vector3.zero)
        {
            enemy.transform.rotation = Quaternion.LookRotation(dir);
        }

        float distance = Vector3.Distance(enemy.transform.position, enemy.target.position);

        if (enemy.target == null || distance >= enemy.stats.detectionRange)
        {
            enemy.ChangeState(new BossIdleState());
        }
        else if (distance <= enemy.stats.attackRange)
        {
            //enemy.ChangeState(new BossAttackState());
        }
    }
    public override void FixedUpdate(Enemy enemy)
    {
    }
}
