using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Enemy
{
    protected override void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    protected override void Start()
    {
        agent.updatePosition = false;
        agent.updateRotation = false;
        ChangeState(new BossIdleState());
    }

    void OnAnimatorMove()
    {
        // 애니메이터의 루트 모션으로 위치와 회전을 업데이트
        if (agent != null)
        {
            transform.position = animator.rootPosition;
            transform.rotation = animator.rootRotation;
            // NavMeshAgent의 다음 위치도 동기화
            agent.nextPosition = transform.position;
        }
    }
}