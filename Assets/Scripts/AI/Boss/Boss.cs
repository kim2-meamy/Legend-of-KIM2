using System.Collections;
using UnityEngine;

public class Boss : BaseAI<Boss>
{
    [HideInInspector]
    protected Collider meleeArea;
    [HideInInspector]
    public BossStats stats;

    private CharacterController controller;
    private float verticalVelocity = 0f;
    private float gravityMultiplier = 1f;

    protected override void Awake()
    {
        base.Awake();
        meleeArea = GetComponentInChildren<BoxCollider>();
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

    protected void OnTriggerEnter(Collider other)
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
        meleeArea.enabled = true;
        yield return new WaitForSeconds(stats.Attack1hitboxDeactivationTime);
        meleeArea.enabled = false;
    }

    private IEnumerator Attack2Coroutine()
    {
        yield return new WaitForSeconds(stats.Attack2hitboxAcitvaionTime);
        meleeArea.enabled = true;
        yield return new WaitForSeconds(stats.Attack2hitboxDeactivationTime);
        meleeArea.enabled = false;
    }

    private IEnumerator Attack3Coroutine()
    {
        yield return new WaitForSeconds(stats.Attack3hitboxAcitvaionTime);
        meleeArea.enabled = true;
        yield return new WaitForSeconds(stats.Attack3hitboxDeactivationTime);
        meleeArea.enabled = false;
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
}