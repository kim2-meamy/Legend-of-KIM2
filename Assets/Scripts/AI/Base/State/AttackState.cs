using UnityEngine;

public class AttackState<T> : IBaseAIState<T> where T : BaseAI<T>
{
    protected float timer;

    public virtual void Enter(T ai)
    {
        ai.agent.isStopped = true;
    }
    public virtual void Update(T ai)
    {
        timer += Time.deltaTime;
    }

    public void Exit(T ai)
    {
        ai.agent.isStopped = false;
        ai.animator.SetTrigger("AttackEnd");
    }
}
