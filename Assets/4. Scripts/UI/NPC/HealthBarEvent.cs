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

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        healthBar = GetComponent<Image>();
        healthText = GetComponentInChildren<TextMeshProUGUI>();
        name = GetComponentInParent<TextMeshProUGUI>();
        name.text = obj.name;
    }
    
    void Update()
    {
        hp = enemy.stats.health;
        healthBar.fillAmount = hp * 0.01f;
        healthText.text = hp.ToString();
    }
}
