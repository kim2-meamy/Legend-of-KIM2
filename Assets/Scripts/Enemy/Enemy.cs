using UnityEngine;
using UnityEngine.XR;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float detectionRange = 10f;

    [HideInInspector]
    public Animator animator;
    
    private IEnemyState currentState;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
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

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
            currentState.Exit(this);

        currentState = newState;

        if (currentState != null)
            currentState.Enter(this);
    }

    // 감지 범위 시각화
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
