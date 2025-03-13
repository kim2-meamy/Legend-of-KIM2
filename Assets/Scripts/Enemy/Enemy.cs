using UnityEngine;
using UnityEngine.XR;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float detectionRange = 10f;
    
    private IEnemyState currentState;

    void Start()
    {
        ChangeState(new IdleState());
    }

    void Update()
    {
        if (currentState != null)
            currentState.Update(this);
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
