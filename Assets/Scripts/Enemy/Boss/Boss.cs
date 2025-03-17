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
        // �ִϸ������� ��Ʈ ������� ��ġ�� ȸ���� ������Ʈ
        if (agent != null)
        {
            transform.position = animator.rootPosition;
            transform.rotation = animator.rootRotation;
            // NavMeshAgent�� ���� ��ġ�� ����ȭ
            agent.nextPosition = transform.position;
        }
    }
}