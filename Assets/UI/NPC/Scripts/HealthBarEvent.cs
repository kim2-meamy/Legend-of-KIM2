using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEvent : MonoBehaviour
{
    private NpcStats npcStats;
    private Image healthBar;
    private TextMeshProUGUI healthText;

    void Awake()
    {
        npcStats = GameObject.FindGameObjectWithTag("Npc").GetComponent<NpcStats>();
        healthBar = GetComponent<Image>();
        healthText = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    void Update()
    {
        healthBar.fillAmount = npcStats.Hp * 0.01f;
        healthText.text = npcStats.Hp.ToString();
    }
}
