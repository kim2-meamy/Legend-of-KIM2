using UnityEngine;

public class BossIdleState : IdleState<Boss>
{   
    public override void Update(Boss boss)
    {
        if (boss.target != null &&
            Vector3.Distance(boss.transform.position, boss.target.position) < boss.stats.detectionRange)
        {
            boss.ChangeState(new BossChaseState());
        }
    }
}
