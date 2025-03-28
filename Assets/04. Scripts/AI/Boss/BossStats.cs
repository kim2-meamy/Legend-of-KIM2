using UnityEngine;

public class BossData : BaseAIData
{
    public int armor;
    public float meleeAttackRange;
    public float rangedAttackRange;
    public float attack1Delay;
    public float attack2Delay;
    public float attack3Delay;
    public float attack1HitboxAcitvaionTime;
    public float attack1HitboxDeactivationTime;
    public float attack2HitboxAcitvaionTime;
    public float attack2HitboxDeactivationTime;
    public float attack3HitboxAcitvaionTime;
    public float attack3HitboxDeactivationTime;
    
    public BossData(BossStats stats) : base(stats)
    {
        armor = stats.armor;
        meleeAttackRange = stats.meleeAttackRange;
        rangedAttackRange = stats.rangedAttackRange;
        attack1Delay = stats.attack1Delay;
        attack2Delay = stats.attack2Delay;
        attack3Delay = stats.attack3Delay;
        attack1HitboxAcitvaionTime = stats.attack1HitboxAcitvaionTime;
        attack1HitboxDeactivationTime = stats.attack1HitboxDeactivationTime;
        attack2HitboxAcitvaionTime = stats.attack2HitboxAcitvaionTime;
        attack2HitboxDeactivationTime = stats.attack2HitboxDeactivationTime;
        attack3HitboxAcitvaionTime = stats.attack3HitboxAcitvaionTime;
        attack3HitboxDeactivationTime = stats.attack3HitboxDeactivationTime;
    }
}

[CreateAssetMenu(fileName = "BossStats", menuName = "ScriptableObjects/BossStats")]
public class BossStats : BaseAIStats
{
    public int armor = 100;
    public float meleeAttackRange = 3.5f;
    public float rangedAttackRange = 8f;
    public float attack1Delay = 3.5f;
    public float attack2Delay = 5.5f;
    public float attack3Delay = 3f;
    public float attack1HitboxAcitvaionTime = 0.2f;
    public float attack1HitboxDeactivationTime = 1.5f;
    public float attack2HitboxAcitvaionTime = 0.2f;
    public float attack2HitboxDeactivationTime = 4f;
    public float attack3HitboxAcitvaionTime = 0.2f;
    public float attack3HitboxDeactivationTime = 1.5f;
}
