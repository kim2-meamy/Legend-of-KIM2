using System.Collections;
using UnityEngine;

public class Enemy : BaseAI<Enemy>
{
    [HideInInspector]
    protected Collider meleeArea;
    [HideInInspector]
    public EnemyStats stats;

    protected override void Awake()
    {
        base.Awake();
        meleeArea = GetComponentsInChildren<SphereCollider>()[1];
        stats = GetStats<EnemyStats>();
    }


    protected override IBaseAIState<Enemy> GetInitialState()
    {
        return new EnemyIdleState();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player.Damaged(stats.damage);
            Debug.Log("Player Damaged");
        }
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(stats.hitboxAcitvaionTime);
        meleeArea.enabled = true;
        yield return new WaitForSeconds(stats.hitboxDeactivationTime);
        meleeArea.enabled = false;
    }

    public override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    public void TakeDamage(int damage)
    {
        stats.health -= damage;
        playerAttackPattern = damage;
        ChangeState(new EnemyHitState());
    }
}