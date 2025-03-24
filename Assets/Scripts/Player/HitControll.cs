using Unity.VisualScripting;
using UnityEngine;

public class HitControll : MonoBehaviour
{
    private Enemy enemy;
    private playerController player;
    private Boss boss;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        player = GetComponentInParent<playerController>();
        boss = GetComponentInParent<Boss>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && player != null)
        {
            Enemy hitEnemy = other.GetComponentInParent<Enemy>();
            if (hitEnemy != null)
            {
                hitEnemy.TakeDamage(player.damage);
            }
        }
        else if (other.CompareTag("Player") && enemy != null)
        {
            playerController hitPlayer = other.GetComponentInParent<playerController>();
            if (hitPlayer != null)
            {
                hitPlayer.Hit(enemy.stats.damage);
            }
        }
        else if (other.CompareTag("Boss") && player != null)
        {
            Boss hitBoss = other.GetComponentInParent<Boss>();
            if (hitBoss != null)
            {
                hitBoss.TakeDamage(player.damage);
            }
        }
    }
}