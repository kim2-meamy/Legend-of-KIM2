using UnityEngine;

public class BossData : BaseAIData
{
    public int armor;
    public float meleeAttackRange;
    public float rangedAttackRange;
    public float attack1Delay;
    public float attack2Delay;
    public float attack3Delay;
    
    public BossData(BossStats stats) : base(stats)
    {
        armor = stats.armor;
        meleeAttackRange = stats.meleeAttackRange;
        rangedAttackRange = stats.rangedAttackRange;
        attack1Delay = stats.attack1Delay;
        attack2Delay = stats.attack2Delay;
        attack3Delay = stats.attack3Delay;
    }
}

[CreateAssetMenu(fileName = "BossStats", menuName = "ScriptableObjects/BossStats")]
public class BossStats : BaseAIStats
{
    public int armor = 100;
    public float meleeAttackRange = 3.5f;
    public float rangedAttackRange = 8f;
    public float attack1Delay = 2.5f;
    public float attack2Delay = 5f;
    public float attack3Delay = 2f;
}
