using UnityEngine;

public class Boss : BaseAI<Boss>
{
    public ParticleSystem armorBreakHit;
    
    [HideInInspector]
    public BossData bossData;

    private CharacterController controller;
    private float verticalVelocity;
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
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y += verticalVelocity * Time.deltaTime;
        controller.Move(deltaPosition);
    }

    public override void Attack() { }

    public override void TakeDamage(int damage)
    {
        float animationLength = 0.533f;
        
        if (Time.time < lastHitTime + animationLength)
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
        dieEffect.Play();
    }
}