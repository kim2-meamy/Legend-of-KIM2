using UnityEngine;

public class EnemyAttackState : AttackState<Enemy>
{
    public override void Enter(Enemy enemy)
    {
        base.Enter(enemy);
        
        enemy.agent.isStopped = true;
        enemy.animator.SetTrigger(enemy.animatorToHash.animAttack);
        enemy.Attack();
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

        if (timer >= enemy.enemyData.attackDelay)
        {
            enemy.ChangeState(new EnemyChaseState());
        }
    }

    public override void Exit(Enemy enemy)
    {
        enemy.agent.isStopped = false;
    }
}
