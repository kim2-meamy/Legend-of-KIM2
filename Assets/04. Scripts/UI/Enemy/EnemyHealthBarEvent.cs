using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarEvent : MonoBehaviour
{
    private const float EnemyHp = 100f;
    private const float BossHp = 200f;
    
    [SerializeField]
    private GameObject EnemyObject;
    
    private int hp;
    private float fillPercent;
    
    private Image healthBar;
    private TextMeshProUGUI healthValue;
    
    private Enemy enemy;
    private Boss boss;

    private void Awake()
    {
        enemy = EnemyObject.GetComponent<Enemy>();
        boss = EnemyObject.GetComponent<Boss>();
        
        healthBar = GetComponent<Image>();
        healthValue = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    private void Update()
    {
        if (enemy != null)
        {
            hp = enemy.enemyData.health;
            fillPercent = 1f / EnemyHp;
        }
        else if (boss != null)
        {
            hp = boss.bossData.health;
            fillPercent = 1f / BossHp;
        }
        
        healthBar.fillAmount = hp * fillPercent;
        healthValue.text = hp.ToString();
    }
}
