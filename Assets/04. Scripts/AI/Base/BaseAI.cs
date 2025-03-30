using CartoonFX;
using UnityEngine;

public abstract class BaseAI<T> : MonoBehaviour where T : BaseAI<T>
{
    public Transform target;
    public ParticleSystem hitEffect;
    public ParticleSystem dieEffect;
    public ParticleSystem damageEffect;
    public AnimatorToHash animatorToHash;

    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public IBaseAIState<T> currentState;
    [HideInInspector]
    public BaseAIData data;

    [SerializeField]
    private BaseAIStats stats;

    public U GetStats<U>() where U : BaseAIStats
    {
        return stats as U;
    }


    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        data = new BaseAIData(stats);
        animatorToHash = new AnimatorToHash();
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

    public virtual void TakeDamage(int damage)
    {
        data.health -= damage;
        damageEffect.GetComponent<CFXR_ParticleText>().UpdateText("-" + damage.ToString());
        damageEffect.Play();
    }

    public virtual void Die()
    {
        currentState = null;
    }
}
