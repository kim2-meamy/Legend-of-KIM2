using System.Threading;
using UnityEngine;

public class DieState<T> : IBaseAIState<T> where T : BaseAI<T>
{
    public float timer = 0f;

    public virtual void Enter(T ai)
    {
        ai.agent.isStopped = true;
        ai.animator.SetTrigger("Die");
    }

    public virtual void Update(T ai)
    {
        timer += Time.deltaTime;
    }

    public virtual void Exit(T ai)
    {
    }
}
