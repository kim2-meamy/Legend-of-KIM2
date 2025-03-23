public interface IBaseAIState<T> where T : BaseAI<T>
{
    void Enter(T ai);
    void Update(T ai);
    void Exit(T ai);
}
