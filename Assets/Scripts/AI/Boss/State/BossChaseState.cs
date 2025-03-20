using UnityEngine;

public class BossChaseState : ChaseState<Boss>
{
    public override void Update(Boss boss)
    {
        BossStateUtils.RotateTowardsTarget(boss);

        float distance = Vector3.Distance(boss.transform.position, boss.target.position);

        if (boss.target == null || distance >= boss.stats.detectionRange)
        {
            boss.ChangeState(new BossIdleState());
        }
        else if (distance <= boss.stats.rangedAttackRange)
        {
            Vector3 direction = boss.target.position - boss.transform.position;
            direction.y = 0;
            float angleDifference = Vector3.Angle(boss.transform.forward, direction);

            if (angleDifference > 2f)
                return;
            
            boss.ChangeState(new BossAttackState());
        }
    }
    public override void FixedUpdate(Boss boss)
    {
    }
}
