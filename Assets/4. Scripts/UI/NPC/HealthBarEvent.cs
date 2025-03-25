using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarEvent : MonoBehaviour
{
    public GameObject obj;
    
    private int hp;
    private Image healthBar;
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI name;
    private Enemy enemy;
    private Boss boss;

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        if (enemy == null)
        {
            boss = GameObject.Find("Boss").GetComponent<Boss>();
        }
        healthBar = GetComponent<Image>();
        healthText = GetComponentInChildren<TextMeshProUGUI>();
        name = GetComponentInParent<TextMeshProUGUI>();
        name.text = obj.name;
    }
    
    void Update()
    {
        if (enemy != null)
        {
            hp = enemy.stats.health;
        }
        else
        {
            hp = boss.stats.health;
        }
        healthBar.fillAmount = hp * 0.01f;
        healthText.text = hp.ToString();
    }
}
