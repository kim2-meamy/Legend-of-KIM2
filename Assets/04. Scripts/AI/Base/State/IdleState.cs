public class IdleState<T> : IBaseAIState<T> where T : BaseAI<T>
{
    public void Enter(T ai)
    {
        ai.animator.SetBool(ai.animatorToHash.animIsChase, false);
    }

    public virtual void Update(T ai) { }

    public virtual void Exit(T ai)
    {
    }
}
