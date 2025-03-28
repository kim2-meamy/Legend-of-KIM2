public class EnemyHitState : HitState<Enemy>
{
    public override void Enter(Enemy enemy)
    {
        base.Enter(enemy);

        enemy.animator.SetTrigger("Hit1");
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
}
