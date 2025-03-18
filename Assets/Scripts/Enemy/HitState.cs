using UnityEngine;

public class HitState : IEnemyState
{
    private float timer;

    public void Enter(Enemy enemy)
    {
        Debug.Log("Enter Hit");
        enemy.agent.isStopped = true;
        switch (enemy.playerAttackPattern)
        {
            case 1:
                enemy.animator.SetTrigger("Hit1");
                break;
            case 2:
                enemy.animator.SetTrigger("Hit2");
                break;
            case 3:
                enemy.animator.SetTrigger("Hit3");
                break;
        }

        timer = 0f;
    }

    public void Update(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (timer >= enemy.stats.hitRecoveryTime)
        {
            enemy.ChangeState(new ChaseState());
        }
    }

    public void FixedUpdate(Enemy enemy)
    {

    }

    public void Exit(Enemy enemy)
    {
        enemy.agent.isStopped = false;
        Debug.Log("Exit Hit");
    }
}
