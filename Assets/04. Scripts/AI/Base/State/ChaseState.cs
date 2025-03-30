using UnityEngine;

public class ChaseState<T> : IBaseAIState<T> where T : BaseAI<T>
{
    public virtual void Enter(T ai)
    {
        ai.animator.SetBool(ai.animatorToHash.animIsChase, true);
    }

    public virtual void Update(T ai) { }

    public void Exit(T ai)
    {
    }
}
