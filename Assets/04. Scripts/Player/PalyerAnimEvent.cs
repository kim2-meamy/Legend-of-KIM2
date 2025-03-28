using System;
using UnityEngine;

public class PalyerAnimEvent : MonoBehaviour
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

    public void DodgeStart()
    {
        controller.isDodging=true;
        controller.walkSpeed = 8f;
    }
    
    public void DodgeEnd()
    {
        controller.isDodging=false;
        controller.walkSpeed = 3f;
    }
    
}
