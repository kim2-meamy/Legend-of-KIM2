using UnityEngine;

public class AttackState<T> : IBaseAIState<T> where T : BaseAI<T>
{
    protected float timer;

    public virtual void Enter(T ai)
    {
        timer = 0f;
    }
    
    public virtual void Update(T ai)
    {
        timer += Time.deltaTime;
    }

    public virtual void Exit(T ai)
    {
        ai.animator.SetInteger(ai.animatorToHash.animBossAttack, 0);
        ai.animator.SetTrigger(ai.animatorToHash.animAttackEnd);
    }
}
