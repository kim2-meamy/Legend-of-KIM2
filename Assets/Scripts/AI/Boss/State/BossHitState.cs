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
            boss.animator.SetTrigger("StunEnd");
            boss.ChangeState(new BossIdleState());
        }
    }

    public override void Exit(Boss boss)
    {
        base.Exit(boss);
        boss.stats.armor = 100;
    }
}
