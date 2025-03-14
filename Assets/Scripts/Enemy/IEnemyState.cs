public interface IEnemyState
{
    void Enter(Enemy enemy);
    void Update(Enemy enemy);
    void FixedUpdate(Enemy enemy);
    void Exit(Enemy enemy);
}
