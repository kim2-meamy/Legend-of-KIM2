using UnityEngine;

public class ChaseState<T> : IBaseAIState<T> where T : BaseAI<T>
{
    public void Enter(T ai)
    {
        Debug.Log("Enter Chase");
        ai.agent.isStopped = false;
        ai.animator.SetBool("isChase", true);
    }

    public virtual void Update(T ai) { }

    public void Exit(T ai)
    {
        Debug.Log("Exit Chase");
    }
}
