public class BossDieState : DieState<Boss>
{
    public override void Enter(Boss boss)
    {
        base.Enter(boss);
        boss.armorBreakHit.Play();
    }

    public override void Update(Boss boss)
    {
        base.Update(boss);
        if (timer >= boss.stats.deathTime)
        {
            boss.ChangeState(null);
        }
    }

    public override void Exit(Boss boss)
    {
        base.Exit(boss);
        boss.Die();
    }
}
