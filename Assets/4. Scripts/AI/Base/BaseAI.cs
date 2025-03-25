using System.IO;
using CartoonFX;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseAI<T> : MonoBehaviour where T : BaseAI<T>
{
    public Transform target;
    public ParticleSystem hitEffect;
    public ParticleSystem dieEffect;
    public ParticleSystem damageEffect;

    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public int playerAttackPattern;
    [HideInInspector]
    public IBaseAIState<T> currentState;

    [SerializeField]
    private BaseAIStats stat;

    public U GetStats<U>() where U : BaseAIStats
    {
        return stat as U;
    }


    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Start()
    {
        ChangeState(GetInitialState());
    }

    protected virtual void Update()
    {
        if (currentState == null)
                return;

        currentState?.Update((T)this);
    }

    public void ChangeState(IBaseAIState<T> newState)
    {
        currentState?.Exit((T)this);
        currentState = newState;
        
        if (currentState != null)
        {
            currentState?.Enter((T)this);
        }
    }

    protected abstract IBaseAIState<T> GetInitialState();

    public abstract void Attack();

    public virtual void Attack(int attackPattern)
    {
        Attack();
    }

    public virtual void TakeDamage(int damage)
    {
        stat.health -= damage;
        damageEffect.GetComponent<CFXR_ParticleText>().UpdateText("-" + damage.ToString());
        damageEffect.Play();
    }

    public virtual void Die()
    {
        currentState = null;
        //Destroy(gameObject);
    }
}
