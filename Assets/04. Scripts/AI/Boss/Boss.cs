using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Boss : BaseAI<Boss>
{
    public ParticleSystem armorBreakHit;
    public Collider axeArea;
    public Collider headButtArea;
    
    [HideInInspector]
    public BossData bossData;

    private CharacterController controller;
    private float verticalVelocity;
    private readonly float gravityMultiplier = 1f;
    private float lastHitTime = float.MinValue;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<CharacterController>();
        var stats = GetStats<BossStats>();
        bossData = new BossData(stats);
        data = bossData;
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
        yield return new WaitForSeconds(bossData.attack1HitboxAcitvaionTime);
        axeArea.enabled = true;
        yield return new WaitForSeconds(bossData.attack1HitboxDeactivationTime);
        axeArea.enabled = false;
    }

    private IEnumerator Attack2Coroutine()
    {
        yield return new WaitForSeconds(bossData.attack2HitboxAcitvaionTime);
        headButtArea.enabled = true;
        yield return new WaitForSeconds(bossData.attack2HitboxDeactivationTime);
        headButtArea.enabled = false;
    }

    private IEnumerator Attack3Coroutine()
    {
        yield return new WaitForSeconds(bossData.attack3HitboxAcitvaionTime);
        axeArea.enabled = true;
        yield return new WaitForSeconds(bossData.attack3HitboxDeactivationTime);
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
        if (Time.time < lastHitTime + 0.533f)
            return;

        lastHitTime = Time.time;

        base.TakeDamage(damage);
        bossData.armor -= damage;

        if (bossData.health <= 0)
        {
            ChangeState(new BossDieState());
            return;
        }
        
        if (bossData.armor <= 0 && !(currentState is BossHitState))
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