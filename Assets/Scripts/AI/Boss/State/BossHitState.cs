public class BossHitState : HitState<Boss>
{
    public override void Enter(Boss boss)
    {
        base.Enter(boss);
        boss.animator.SetTrigger("Stun");
    }

    public override void Update(Boss boss)
    {
        base.Update(boss);
        if (timer >= boss.stats.hitRecoveryTime)
        {
            boss.ChangeState(new BossIdleState());
        }
        else if (timer >= boss.stats.hitTime)
        {
            boss.animator.SetTrigger("StunEnd");
        }
    }

    public override void Exit(Boss boss)
    {
        base.Exit(boss);
    }
}
