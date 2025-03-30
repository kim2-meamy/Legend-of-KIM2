using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : BaseAI<Enemy>
{
    public GameObject skin;
    
    protected Collider meleeArea;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public EnemyData enemyData;

    protected override void Awake()
    {
        base.Awake();
        meleeArea = GetComponentsInChildren<SphereCollider>()[1];
        agent = GetComponent<NavMeshAgent>();
        var stats = GetStats<EnemyStats>();
        enemyData = new EnemyData(stats);
        data = enemyData;
    }

    protected override IBaseAIState<Enemy> GetInitialState()
    {
        return new EnemyIdleState();
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(enemyData.hitboxAcitvaionTime);
        meleeArea.enabled = true;
        yield return new WaitForSeconds(enemyData.hitboxDeactivationTime);
        meleeArea.enabled = false;
    }

    public override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    public override void TakeDamage(int damage)
    {
        if (currentState is EnemyDieState)
            return;
        
        base.TakeDamage(damage);

        if (enemyData.health > 0)
        {
            ChangeState(new EnemyHitState());
            return;
        }
        
        ChangeState(new EnemyDieState());
    }

    public override void Die()
    {
        skin.SetActive(false);
        dieEffect.Play();
        Destroy(gameObject, 1f);
    }
}