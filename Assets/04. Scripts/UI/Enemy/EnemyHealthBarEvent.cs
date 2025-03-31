using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarEvent : MonoBehaviour
{
    private const float EnemyHp = 100f;
    private const float BossHp = 200f;
    
    [SerializeField]
    private GameObject EnemyObject;
    private TextMeshProUGUI nameValue;
    
    private int hp;
    private float fillPercent;
    
    private Image healthBar;
    private TextMeshProUGUI healthValue;
    
    private Enemy enemy;
    private Boss boss;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        
        if (enemy == null)
        {
            boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        }
        
        healthBar = GetComponent<Image>();
        healthValue = GetComponentInChildren<TextMeshProUGUI>();
        
        nameValue = GetComponentInParent<TextMeshProUGUI>();
        nameValue.text = EnemyObject.name;
    }
    
    private void Update()
    {
        if (enemy != null)
        {
            hp = enemy.stats.health;
            fillPercent = 1f / EnemyHp;
        }
        else
        {
            hp = boss.stats.health;
            fillPercent = 1f / BossHp;
        }
        
        healthBar.fillAmount = hp * fillPercent;
        healthValue.text = hp.ToString();
    }
}
