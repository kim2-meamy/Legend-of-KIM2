using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
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

        SFXType type = SFXType.BossMeleeAttack + attackPattern - 1;
        float startTime = GetAudioStartTime(attackPattern);
        float durationTime = GetAudioDurationTime(attackPattern);
        if (type == SFXType.BossRangeAttack)
            boss.StartSFXCoroutine(type, startTime, durationTime, 0.4f);
        else
        {
            AudioManager.Instance.PlaySFX(type, 0.3f, startTime, durationTime);
        }
        
        attackDelay = GetAttackDelay(boss, (BossAttackPattern)attackPattern);
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

    private float GetAudioStartTime(int attackPattern)
    {
        switch (attackPattern)
        {
            case 1:
                return 0f;
            case 2:
                return 1.2f;
            case 3:
                return 0f;
            default:
                return 0f;
        }
    }
    
    private float GetAudioDurationTime(int attackPattern)
    {
        switch (attackPattern)
        {
            case 1:
                return 1f;
            case 2:
                return 2.5f;
            case 3:
                return 0.6f;
            default:
                return 0f;
        }
    }
}
