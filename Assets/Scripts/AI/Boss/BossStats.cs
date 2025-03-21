using UnityEngine;

[CreateAssetMenu(fileName = "BossStats", menuName = "ScriptableObjects/BossStats")]
public class BossStats : BaseAIStats
{
    //detecttion range = 20;
    public int armor = 100;
    public int Attack1damage = 10;
    public int Attack2damage = 10;
    public int Attack3damage = 10;
    public float meleeAttackRange = 3.5f;
    public float rangedAttackRange = 8f;
    public float attack1Delay = 3.5f;
    public float attack2Delay = 5.5f;
    public float attack3Delay = 3f;
    public float Attack1hitboxAcitvaionTime = 0.2f;
    public float Attack1hitboxDeactivationTime = 1.5f;
    public float Attack2hitboxAcitvaionTime = 0.2f;
    public float Attack2hitboxDeactivationTime = 4f;
    public float Attack3hitboxAcitvaionTime = 0.2f;
    public float Attack3hitboxDeactivationTime = 1.5f;
    public float hitTime = 5f;
    public float hitRecoveryTime = 10f;
}
