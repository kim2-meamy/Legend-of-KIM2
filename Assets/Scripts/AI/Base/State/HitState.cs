using System.Buffers.Text;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class HitState<T> : IBaseAIState<T> where T : BaseAI<T>
{
    protected float timer;

    public virtual void Enter(T ai)
    {
        Debug.Log("Enter Hit");
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
        Debug.Log("Exit Hit");
    }
}
