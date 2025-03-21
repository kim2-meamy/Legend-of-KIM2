using UnityEngine;

public class EnemyHitState : HitState<Enemy>
{
    public override void Enter(Enemy enemy)
    {
        base.Enter(enemy);

        switch (enemy.playerAttackPattern)
        {
            case 1:
                enemy.animator.SetTrigger("Hit1");
                break;
            case 2:
                enemy.animator.SetTrigger("Hit2");
                break;
            case 3:
                enemy.animator.SetTrigger("Hit3");
                break;
        }
    }

public override void Update(Enemy Enemy)
    {
        base.Update(Enemy);
        
        if (timer >= Enemy.stats.hitRecoveryTime)
        {
            Enemy.ChangeState(new EnemyChaseState());
        }
    }
}
