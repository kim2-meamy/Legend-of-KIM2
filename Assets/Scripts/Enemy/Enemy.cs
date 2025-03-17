using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public EnemyStats stats;

    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public int playerAttackPattern;
    [HideInInspector]
    public IEnemyState currentState;

    private SphereCollider meleeArea;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        Transform meleeTransform = transform.Find("MeleeArea");
        if (meleeTransform != null)
            meleeArea = meleeTransform.GetComponent<SphereCollider>();
    }

    protected virtual void Start()
    {
        ChangeState(new IdleState());
    }

    void Update()
    {
        if (currentState != null)
            currentState.Update(this);
    }

    void FixedUpdate()
    {
        if (currentState != null)
            currentState.FixedUpdate(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player.Damaged(stats.damage);
            Debug.Log("Player Damaged");
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
            currentState.Exit(this);

        currentState = newState;

        if (currentState != null)
            currentState.Enter(this);
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(stats.hitboxAcitvaionTime);
        meleeArea.enabled = true;
        yield return new WaitForSeconds(stats.hitboxDeactivationTime);
        meleeArea.enabled = false;
    }

    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    public void TakeDamage(int damage)
    {
        stats.health -= damage;
        playerAttackPattern = damage;
        ChangeState(new HitState());
    }
}