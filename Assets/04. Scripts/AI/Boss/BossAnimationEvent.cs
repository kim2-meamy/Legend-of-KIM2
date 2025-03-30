using UnityEngine;

public class BossAnimationEvent : MonoBehaviour
{
    public Collider axeArea;
    public Collider headButtArea;
    
    public void axeAttackStart()
    {
        axeArea.enabled = true;
    }

    public void axeAttackEnd()
    {
        axeArea.enabled = false;
    }

    public void ChargeAttackStart()
    {
        headButtArea.enabled = true;
    }

    public void ChargeAttackEnd()
    {
        headButtArea.enabled = false;
    }
}
