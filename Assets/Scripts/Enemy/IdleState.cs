using UnityEngine;

public class IdleState : IEnemyState
{
    public void Enter(Enemy enemy)
    {
        Debug.Log("Enter Idle");
    }

    public void Update(Enemy enemy)
    {
        if (enemy.target != null &&
            Vector3.Distance(enemy.transform.position, enemy.target.position) < enemy.detectionRange)
        {
            enemy.ChangeState(new ChaseState());
        }
    }
    
    public void Exit(Enemy enemy)
    {
        Debug.Log("Exiting Idle State");
    }
}
