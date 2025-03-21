using UnityEngine;

public class IdleState<T> : IBaseAIState<T> where T : BaseAI<T>
{
    public void Enter(T ai)
    {
        Debug.Log("Enter Idle");
        ai.animator.SetBool("isChase", false);
    }

    public virtual void Update(T ai) { }

    public virtual void Exit(T ai)
    {
        Debug.Log("Exit Idle State");
    }
}
