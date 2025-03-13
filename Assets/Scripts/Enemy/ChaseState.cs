using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IEnemyState
{
    public void Enter(Enemy enemy)
    {
        Debug.Log("Enter Chase");
        enemy.animator.SetBool("isChase", true);
    }

    public void Update(Enemy enemy)
    {
        if (enemy.target != null &&
        Vector3.Distance(enemy.transform.position, enemy.target.position) >= enemy.detectionRange)
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void FixedUpdate(Enemy enemy)
    {
        if (enemy.target != null)
        {
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

            if (agent != null)
            {
                agent.SetDestination(enemy.target.position);
            }
        }
    }

    public void Exit(Enemy enemy)
    {
        Debug.Log("Exit Chase");
    }
}
