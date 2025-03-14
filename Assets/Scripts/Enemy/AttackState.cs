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

        //if (Vector3.Distance(enemy.transform.position, enemy.target.position) > enemy.stats.attackRange)
        //{
        //    enemy.ChangeState(new ChaseState());
        //    return;
        //}

        timer += Time.deltaTime;
        if (timer >= enemy.stats.attackDelay)
        {
            enemy.ChangeState(new ChaseState());
        }
    }

    public void FixedUpdate(Enemy enemy)
    {
        //// 공격 시 방향 전환
        //if (enemy.target != null)
        //{
        //    Vector3 direction = (enemy.target.position - enemy.transform.position).normalized;
        //    if (direction != Vector3.zero)
        //    {
        //        // 즉시 전환
        //        //enemy.transform.rotation = Quaternion.LookRotation(direction);
                
        //        // 부드러운 전환
        //        //Quaternion lookRotation = Quaternion.LookRotation(direction);
        //        //enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 10f);
        //    }
        //}
    }

    public void Exit(Enemy enemy)
    {
        enemy.agent.isStopped = false;
        Debug.Log("Exit Attack");
    }
}
