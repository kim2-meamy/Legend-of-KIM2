using UnityEngine;

public class HitState<T> : IBaseAIState<T> where T : BaseAI<T>
{
    protected float timer;

    public virtual void Enter(T ai)
    {
        ai.agent.isStopped = true;
        timer = 0f;
    }

    public virtual void Update(T ai)
    {
        timer += Time.deltaTime;
    }

    public virtual void Exit(T ai)
    {
        ai.agent.isStopped = false;
    }
}
