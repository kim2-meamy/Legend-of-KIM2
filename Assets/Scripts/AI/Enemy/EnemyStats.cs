using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats")]
public class EnemyStats : BaseAIStats
{
    public float attackRange = 3f;
    public float attackDelay = 5f;
    public float hitboxAcitvaionTime = 0.2f;
    public float hitboxDeactivationTime = 1f;
    public float hitRecoveryTime = 2f;
}