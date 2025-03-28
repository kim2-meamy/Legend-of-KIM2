using System;
using UnityEngine;

public class HitControll : MonoBehaviour
{
    private Enemy enemy;
    private PlayerController player;
    private Boss boss;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        player = GetComponentInParent<PlayerController>();
        boss = GetComponentInParent<Boss>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (player == null)
            {
                return;
            }

            var hitEnemy = other.GetComponentInParent<Enemy>();
            if (hitEnemy != null)
            {
                hitEnemy.TakeDamage(player.damage);
            }

            return;
        }
        
        if (other.CompareTag("Player"))
        {
            if (enemy == null && boss == null)
            {
                return;
            }
            var hitPlayer = other.GetComponentInParent<PlayerController>();
            if (hitPlayer == null)
            {
                return;
            }

            if (enemy != null)
                hitPlayer.Hit(enemy.enemyData.damage);
            else if (boss != null)
                hitPlayer.Hit(boss.bossData.damage);
            return;
        }
        
        if (other.CompareTag("Boss"))
        {
            var hitBoss = other.GetComponentInParent<Boss>();
            hitBoss?.TakeDamage(player.damage);
            return;
        }
    }
}