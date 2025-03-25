using System.Collections;
using UnityEngine;

public class Boss : BaseAI<Boss>
{
    public ParticleSystem armorBreakHit;
    public Collider axeArea;
    public Collider headButtArea;

    [HideInInspector]
    public BossStats stats;

    //private Collider axeArea;
    //private Collider headButtArea;
    private CharacterController controller;
    private float verticalVelocity = 0f;
    private float gravityMultiplier = 1f;

    protected override void Awake()
    {
        base.Awake();
        //axeArea = GetComponentInChildren<BoxCollider>();
        //headButtArea = GetComponentInChildren<CapsuleCollider>();
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
        headButtArea.enabled = true;
        yield return new WaitForSeconds(stats.Attack2hitboxDeactivationTime);
        headButtArea.enabled = false;
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

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        stats.armor -= damage * 2;

        if (stats.health <= 0)
        {
            ChangeState(new BossDieState());
            return;
        }
        
        if (stats.armor <= 0 && !(currentState is BossHitState))
        {
            if (!(currentState is BossHitState))
                OnStun();
        }
        else if (currentState is BossHitState)
        {
            armorBreakHit.Play();
        }
        else
        {
            hitEffect.Play();
        }
    }

    public void OnStun()
    {
        ChangeState(new BossHitState());
    }

    public override void Die()
    {
        //skin.SetActive(false);
        dieEffect.Play();
        //Destroy(gameObject, 1f);
    }
}