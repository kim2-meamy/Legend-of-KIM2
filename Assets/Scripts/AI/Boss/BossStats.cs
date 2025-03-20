using UnityEngine;

[CreateAssetMenu(fileName = "BossStats", menuName = "ScriptableObjects/BossStats")]
public class BossStats : BaseAIStats
{
    public int Attack1damage = 10;
    public int Attack2damage = 10;
    public int Attack3damage = 10;
    public float meleeAttackRange = 3f;
    public float rangedAttackRange = 10f;
    public float attack1Delay = 5f;
    public float attack2Delay = 5f;
    public float attack3Delay = 5f;
    public float Attack1hitboxAcitvaionTime = 0.2f;
    public float Attack1hitboxDeactivationTime = 1f;
    public float Attack2hitboxAcitvaionTime = 0.2f;
    public float Attack2hitboxDeactivationTime = 1f;
    public float Attack3hitboxAcitvaionTime = 0.2f;
    public float Attack3hitboxDeactivationTime = 1f;
}
