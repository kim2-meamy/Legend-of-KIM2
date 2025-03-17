using UnityEngine;

public class IdleState : IEnemyState
{
    public void Enter(Enemy enemy)
    {
        Debug.Log("Enter Idle");
        enemy.animator.SetBool("isChase", false);
    }

    public virtual void Update(Enemy enemy)
    {
        if (enemy.target != null &&
            Vector3.Distance(enemy.transform.position, enemy.target.position) < enemy.stats.detectionRange)
        {
            enemy.ChangeState(new ChaseState());
        }
    }

    public void FixedUpdate(Enemy enemy) { }
    
    public void Exit(Enemy enemy)
    {
        Debug.Log("Exit Idle State");
    }
}
