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
        //// ���� �� ���� ��ȯ
        //if (enemy.target != null)
        //{
        //    Vector3 direction = (enemy.target.position - enemy.transform.position).normalized;
        //    if (direction != Vector3.zero)
        //    {
        //        // ��� ��ȯ
        //        //enemy.transform.rotation = Quaternion.LookRotation(direction);
                
        //        // �ε巯�� ��ȯ
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
