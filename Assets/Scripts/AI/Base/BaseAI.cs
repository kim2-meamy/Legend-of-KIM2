using UnityEngine;
using UnityEngine.AI;

public abstract class BaseAI<T> : MonoBehaviour where T : BaseAI<T>
{
    public Transform target;

    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public int playerAttackPattern;
    [HideInInspector]
    public IBaseAIState<T> currentState;

    public T GetStats<T>() where T : BaseAIStats
    {
        return stat as T;
    }

    [SerializeField]
    private BaseAIStats stat;

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
        currentState?.Update((T)this);
    }

    protected virtual void FixedUpdate()
    {
        currentState?.FixedUpdate((T)this);
    }

    public void ChangeState(IBaseAIState<T> newState)
    {
        currentState?.Exit((T)this);
        currentState = newState;
        currentState?.Enter((T)this);
    }

    protected abstract IBaseAIState<T> GetInitialState();

    public abstract void Attack();

    public virtual void Attack(int attackPattern)
    {
        Attack();
    }
}
