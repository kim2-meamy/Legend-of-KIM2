using UnityEngine;

public class BossAttackState : AttackState<Boss>
{
    private int attackPattern;
    private float attackDelay;

    public override void Enter(Boss boss)
    {
        base.Enter(boss);

        BossStateUtils.RotateTowardsTarget(boss);
        attackPattern = ChooseAttackPattern(boss);
        boss.animator.SetTrigger($"Attack{attackPattern}");
        boss.Attack(attackPattern);

        switch (attackPattern)
        {
            case 1:
                attackDelay = boss.stats.attack1Delay;
                break;
            case 2:
                attackDelay = boss.stats.attack2Delay;
                break;
            case 3:
                attackDelay = boss.stats.attack3Delay;
                break;
        }

        timer = 0f;
    }

    public override void Update(Boss boss)
    {

        if (boss.target == null)
        {
            boss.ChangeState(new BossIdleState());
            return;
        }

        if (!(boss.currentState is BossAttackState))
            return;

        base.Update(boss);

        if (timer >= attackDelay)
        {
            boss.ChangeState(new BossIdleState());
        }
    }

    private int ChooseAttackPattern(Boss boss)
    {
        float distance = Vector3.Distance(boss.transform.position, boss.target.position);

        if (distance <= boss.stats.meleeAttackRange)
            return Random.Range(1, 3);
        else
            return Random.Range(2, 4);
    }
}
