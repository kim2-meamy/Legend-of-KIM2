using System.Collections;
using UnityEngine;

public class Enemy : BaseAI<Enemy>
{
    public ParticleSystem dieEffect;
    public GameObject skin;

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
        if (stats.health <= 0)
        {
            ChangeState(new EnemyDieState());
        }
        else
        {
            ChangeState(new EnemyHitState());
        }
    }

    public void Die()
    {
        skin.SetActive(false);
        dieEffect.Play();
        Destroy(gameObject, 1f);
    }
}