using UnityEngine;

public class DieState<T> : IBaseAIState<T> where T : BaseAI<T>
{
    public void Enter(T ai)
    {
        Debug.Log("Enter Die");
        ai.agent.isStopped = false;
        ai.animator.SetTrigger("Die");
    }

    public virtual void Update(T ai) { }

    public void Exit(T ai)
    {
        Debug.Log("Exit Die");
    }
}
