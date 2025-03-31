using UnityEngine;

public class DieState<T> : IBaseAIState<T> where T : BaseAI<T>
{
    protected float timer = 0f;

    public virtual void Enter(T ai)
    {
        ai.animator.SetTrigger(ai.animatorToHash.animDie);
    }

    public virtual void Update(T ai)
    {
        timer += Time.deltaTime;
    }

    public virtual void Exit(T ai)
    {
    }
}
