using UnityEngine;

public class EnemyData : BaseAIData
{
    public float attackRange;
    public float attackDelay;
    public float hitboxAcitvaionTime;
    public float hitboxDeactivationTime;
    
    public EnemyData(EnemyStats stats) : base(stats)
    {
        attackRange = stats.attackRange;
        attackDelay = stats.attackDelay;
        hitboxAcitvaionTime = stats.hitboxAcitvaionTime;
        hitboxDeactivationTime = stats.hitboxDeactivationTime;
    }
}

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats")]
public class EnemyStats : BaseAIStats
{
    public float attackRange = 3f;
    public float attackDelay = 5f;
    public float hitboxAcitvaionTime = 0.2f;
    public float hitboxDeactivationTime = 1f;
}