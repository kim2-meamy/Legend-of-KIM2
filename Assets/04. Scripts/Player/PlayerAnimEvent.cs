using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    public Collider meleeArea;
    public bool alreadyAttack = false;
    
    private PlayerController controller; 

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }
    
    public void AttackStart()
    {
        meleeArea.enabled = true;
        alreadyAttack = true;
    }

    public void AttackEnd()
    {
        meleeArea.enabled = false;
        alreadyAttack = false;
    }

    public void AttackSound()
    {
        AudioManager.Instance.PlaySFX(SFXType.PlayerAttack, 0.6f, 0.1f);
    }

    public void DodgeStart()
    {
        controller.isDodging = true;
        controller.walkSpeed = 8f;
        AudioManager.Instance.PlaySFX(SFXType.PlayerDodge, 0.5f, 0.2f, 0.15f);
    }
    
    public void DodgeEnd()
    {
        controller.isDodging = false;
        controller.walkSpeed = 3f;
    }

    public void LeftStep()
    {
        AudioManager.Instance.PlaySFX(SFXType.PlayerLStep, 2f, 0.2f);
    }

    public void RightStep()
    {
        AudioManager.Instance.PlaySFX(SFXType.PlayerRStep, 2f, 0.2f);
    }
}
