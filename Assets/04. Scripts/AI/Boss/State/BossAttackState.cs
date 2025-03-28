using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossAttackState : AttackState<Boss>
{
    private enum BossAttackPattern
    {
        meleeAttack = 1,
        chargeAttack = 2,
        rangedAttack = 3
    }
    
    private int attackPattern;
    private float attackDelay;

    public override void Enter(Boss boss)
    {
        base.Enter(boss);

        BossStateUtils.RotateTowardsTarget(boss);
        attackPattern = ChooseAttackPattern(boss);
        boss.animator.SetInteger(boss.animatorToHash.animBossAttack, attackPattern);
        boss.Attack(attackPattern);
        attackDelay = GetAttackDelay(boss, (BossAttackPattern)attackPattern);

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

        if (distance <= boss.bossData.meleeAttackRange)
            return Random.Range(
                (int)BossAttackPattern.meleeAttack,
                (int)BossAttackPattern.chargeAttack + 1);
        else
            return Random.Range(
                (int)BossAttackPattern.chargeAttack,
                (int)BossAttackPattern.rangedAttack + 1);
    }
    
    private float GetAttackDelay(Boss boss, BossAttackPattern pattern)
    {
        switch (pattern)
        {
            case BossAttackPattern.meleeAttack: return boss.bossData.attack1Delay;
            case BossAttackPattern.chargeAttack: return boss.bossData.attack2Delay;
            case BossAttackPattern.rangedAttack: return boss.bossData.attack3Delay;
            default: return 1f;
        }
    }
}
