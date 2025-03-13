using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IEnemyState
{
    public void Enter(Enemy enemy)
    {
        Debug.Log("Enter Chase");
    }

    public void Update(Enemy enemy)
    {
        Debug.Log("Chase Update »£√‚");
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
        Debug.Log("Exiting Chase State");
    }
}
