public class EnemyHitState : HitState<Enemy>
{
    public override void Enter(Enemy enemy)
    {
        base.Enter(enemy);
        enemy.agent.isStopped = true;

        enemy.animator.SetTrigger(enemy.animatorToHash.animHit1);
        enemy.hitEffect.Play();
    }

    public override void Update(Enemy Enemy)
    {
        base.Update(Enemy);
        
        if (timer >= Enemy.enemyData.hitRecoveryTime)
        {
            Enemy.ChangeState(new EnemyChaseState());
        }
    }

    public override void Exit(Enemy enemy)
    {
        enemy.agent.isStopped = false;
    }
}
