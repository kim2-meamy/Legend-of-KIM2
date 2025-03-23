using System.Threading;
using UnityEngine;

public class DieState<T> : IBaseAIState<T> where T : BaseAI<T>
{
    public float timer = 0f;

    public virtual void Enter(T ai)
    {
        Debug.Log("Enter Die");
        ai.agent.isStopped = true;
        ai.animator.SetTrigger("Die");
    }

    public virtual void Update(T ai)
    {
        timer += Time.deltaTime;
    }

    public virtual void Exit(T ai)
    {
        Debug.Log("Exit Die");
    }
}
