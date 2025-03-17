using UnityEngine;

public class BossIdleState : IdleState
{   
    public override void Update(Enemy enemy)
    {
        if (enemy.target != null &&
            Vector3.Distance(enemy.transform.position, enemy.target.position) < enemy.stats.detectionRange)
        {
            enemy.ChangeState(new BossChaseState());
        }
    }
}
