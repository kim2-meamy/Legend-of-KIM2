using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Enemy : BaseAI<Enemy>
{
    public GameObject skin;

    [HideInInspector]
    protected Collider meleeArea;
    [HideInInspector]
    public EnemyData enemyData;

    protected override void Awake()
    {
        base.Awake();
        meleeArea = GetComponentsInChildren<SphereCollider>()[1];
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
        base.TakeDamage(damage);
        if (enemyData.health <= 0)
        {
            ChangeState(new EnemyDieState());
        }
        else
        {
            ChangeState(new EnemyHitState());
        }
    }

    public override void Die()
    {
        skin.SetActive(false);
        dieEffect.Play();
        Destroy(gameObject, 1f);
    }
}