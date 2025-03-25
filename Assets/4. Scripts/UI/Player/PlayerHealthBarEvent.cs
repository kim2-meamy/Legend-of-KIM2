using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class playerHealthBarEvent : MonoBehaviour
{
    public GameObject obj;
    
    private int hp;
    private Image healthBar;
    private TextMeshProUGUI healthText;
    private playerController player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        healthBar = GetComponent<Image>();
        healthText = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    void Update()
    {
        hp = player.hp;
        healthBar.fillAmount = hp * 0.01f;
        healthText.text = hp.ToString();
    }
}
