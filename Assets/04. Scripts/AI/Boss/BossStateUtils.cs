using UnityEngine;

public static class BossStateUtils
{
    public static void RotateTowardsTarget(Boss boss)
    {
        if (boss.target == null)
            return;

        Vector3 direction = boss.target.position - boss.transform.position;
        direction.y = boss.target.position.y;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, targetRotation, boss.stats.rotationSpeed * Time.deltaTime);
        }
    }
}