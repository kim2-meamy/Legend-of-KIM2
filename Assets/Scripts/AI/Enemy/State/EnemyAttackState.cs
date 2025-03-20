using System;
using UnityEngine;

public class EnemyAttackState : AttackState<Enemy>
{
    public override void Enter(Enemy enemy)
    {
        Debug.Log("Enter Attack");
        enemy.agent.isStopped = true;
        enemy.animator.SetTrigger("Attack");
        enemy.Attack();
        timer = 0f;
    }

    public override void Update(Enemy enemy)
    {
        if (enemy.target == null)
        {
            enemy.ChangeState(new EnemyIdleState());
            return;
        }

        if (!(enemy.currentState is EnemyAttackState))
            return;

        base.Update(enemy);

        if (timer >= enemy.stats.attackDelay)
        {
            enemy.ChangeState(new EnemyChaseState());
        }
    }

    public virtual void Exit(Enemy enemy)
    {
        enemy.agent.isStopped = false;
        enemy.animator.SetTrigger("AttackEnd");
        Debug.Log("Exit Attack");
    }
}
