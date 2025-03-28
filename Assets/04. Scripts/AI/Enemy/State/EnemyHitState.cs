public class EnemyHitState : HitState<Enemy>
{
    public override void Enter(Enemy enemy)
    {
        base.Enter(enemy);

        enemy.animator.SetTrigger("Hit1");
        enemy.hitEffect.Play();
        //switch (enemy.playerAttackPattern)
        //{
        //    case 1:
        //        enemy.animator.SetTrigger("Hit1");
        //        break;
        //    case 2:
        //        enemy.animator.SetTrigger("Hit2");
        //        break;
        //}
    }

    public override void Update(Enemy Enemy)
    {
        base.Update(Enemy);
        
        if (timer >= Enemy.enemyData.hitRecoveryTime)
        {
            Enemy.ChangeState(new EnemyChaseState());
        }
    }
}
