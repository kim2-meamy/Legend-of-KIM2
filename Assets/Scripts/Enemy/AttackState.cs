using UnityEngine;

public class AttackState : IEnemyState
{
    private float timer;

    public void Enter(Enemy enemy)
    {
        Debug.Log("Enter Attack");
        enemy.agent.isStopped = true;
        enemy.animator.SetTrigger("Attack");
        enemy.Attack();
        timer = 0f;
    }

    public void Update(Enemy enemy)
    {
        if (enemy.target == null)
        {
            enemy.ChangeState(new IdleState());
            return;
        }

        if (!(enemy.currentState is AttackState))
            return;

        timer += Time.deltaTime;
        if (timer >= enemy.stats.attackDelay)
        {
            enemy.ChangeState(new ChaseState());
        }
    }

    public void FixedUpdate(Enemy enemy) {}

    public void Exit(Enemy enemy)
    {
        enemy.agent.isStopped = false;
        enemy.animator.SetTrigger("AttackEnd");
        Debug.Log("Exit Attack");
    }
}
