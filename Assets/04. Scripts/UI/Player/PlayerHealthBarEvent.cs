using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarEvent : MonoBehaviour
{
    private const float PlayerHp = 100f;
    
    private int hp;
    private float fillPercent;
    
    private Image healthBar;
    private TextMeshProUGUI healthValue;
    
    private PlayerController player;

    private void Awake()
    {
        var playerUIManager = GetComponentInParent<PlayerUIManager>();
        player = playerUIManager.Player.GetComponent<PlayerController>();
        
        healthBar = GetComponent<Image>();
        healthValue = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    private void Update()
    {
        hp = player.hp;
        fillPercent = 1f / PlayerHp;
        
        healthBar.fillAmount = hp * fillPercent;
        healthValue.text = hp.ToString();
    }
}
