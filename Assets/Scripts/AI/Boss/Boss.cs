using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : BaseAI<Boss>
{
    [HideInInspector]
    public BossStats stats;

    private Collider axeArea;
    //private Collider bodyArea;
    private CharacterController controller;
    private float verticalVelocity = 0f;
    private float gravityMultiplier = 1f;

    protected override void Awake()
    {
        base.Awake();
        axeArea = GetComponentInChildren<BoxCollider>();
        Debug.Log(axeArea.gameObject.name);
        //bodyArea = GetComponentInChildren<CapsuleCollider>();
        controller = GetComponent<CharacterController>();
        stats = GetStats<BossStats>();
    }

    protected override void Start()
    {
        agent.updatePosition = false;
        agent.updateRotation = false;
        ChangeState(new BossIdleState());
    }

    protected override IBaseAIState<Boss> GetInitialState()
    {
        return new BossIdleState();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player.Damaged(stats.damage);
            Debug.Log("Player Damaged");
        }
    }

    void OnAnimatorMove()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = 0f;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }

        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y += verticalVelocity * Time.deltaTime;
        controller.Move(deltaPosition);
    }

    private IEnumerator Attack1Coroutine()
    {
        yield return new WaitForSeconds(stats.Attack1hitboxAcitvaionTime);
        axeArea.enabled = true;
        yield return new WaitForSeconds(stats.Attack1hitboxDeactivationTime);
        axeArea.enabled = false;
    }

    private IEnumerator Attack2Coroutine()
    {
        yield return new WaitForSeconds(stats.Attack2hitboxAcitvaionTime);
        //bodyArea.enabled = true;
        yield return new WaitForSeconds(stats.Attack2hitboxDeactivationTime);
        //bodyArea.enabled = false;
    }

    private IEnumerator Attack3Coroutine()
    {
        yield return new WaitForSeconds(stats.Attack3hitboxAcitvaionTime);
        axeArea.enabled = true;
        yield return new WaitForSeconds(stats.Attack3hitboxDeactivationTime);
        axeArea.enabled = false;
    }

    public override void Attack() { }

    public override void Attack(int attackPattern)
    {
        switch(attackPattern)
        {
            case 1:
                StartCoroutine(Attack1Coroutine());
                break;
            case 2:
                StartCoroutine(Attack2Coroutine());
                break;
            case 3:
                StartCoroutine(Attack3Coroutine());
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        stats.health -= damage;
        stats.armor -= damage * 2;

        if (stats.armor <= 0)
        {
            OnStun();
        }
    }

    public void OnStun()
    {
        ChangeState(new BossHitState());
        stats.armor = 100;
    }
}