public class EnemyDieState : DieState<Enemy>
{
    public override void Enter(Enemy enemy)
    {
        base.Enter(enemy);
        enemy.hitEffect.Play();
    }

    public override void Update(Enemy enemy)
    {
        base.Update(enemy);
        if (timer >= enemy.stats.deathTime)
        {
            enemy.ChangeState(null);
        }
    }

    public override void Exit(Enemy enemy)
    {
        base.Exit(enemy);
    }
}
