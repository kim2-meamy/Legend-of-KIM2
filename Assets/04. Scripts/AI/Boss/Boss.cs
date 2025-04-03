using UnityEngine;
using System.Collections;

public class Boss : BaseAI<Boss>
{
    public ParticleSystem armorBreakHit;
    public PlayerUIManager playerUIManager;
    
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
        if (!playerUIManager.InstQuestManager.InstQuestData.QuestDataList[QuestId.QuestIdList["BossFight"]].DidAccept)
            return;
        
        if (currentState is BossDieState)
            return;
        
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
            AudioManager.Instance.PlaySFX(SFXType.BossHit);
        }
        else
        {
            hitEffect.Play();
            AudioManager.Instance.PlaySFX(SFXType.BossArmorHit);
        }
    }

    public void OnStun()
    {
        ChangeState(new BossHitState());
    }

    public override void Die()
    {
        dieEffect.Play();
        AudioManager.Instance.PlaySFX(SFXType.BossDie);
        playerUIManager.IsGameClear = true;
    }

    public void ChaseChangeState()
    {
        ChangeState(new BossChaseState());
    }

    public void StartSFXCoroutine(SFXType type, float startTime, float durationTime, float delay = 0f)
    {
        StartCoroutine(SFXCoroutine(type, startTime, durationTime, delay));
    }
     
    private IEnumerator SFXCoroutine(SFXType type, float startTime, float durationTime, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySFX(type, 0.3f, startTime, durationTime);
    }
}