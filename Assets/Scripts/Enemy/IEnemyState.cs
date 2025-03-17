public interface IEnemyState
{
    public void Enter(Enemy enemy);
    public void Update(Enemy enemy);
    public void FixedUpdate(Enemy enemy);
    public void Exit(Enemy enemy);
}
