using UnityEngine;

public class AttackState<T> : IBaseAIState<T> where T : BaseAI<T>
{
    protected float timer;

    public virtual void Enter(T ai)
    {
        Debug.Log("Enter Attack");
        ai.agent.isStopped = true;
    }
    public virtual void Update(T ai)
    {
        timer += Time.deltaTime;
    }

    public void FixedUpdate(T ai) { }

    public void Exit(T ai)
    {
        Debug.Log("Exit Attack");
        ai.agent.isStopped = false;
        ai.animator.SetTrigger("AttackEnd");
    }
}
