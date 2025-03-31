using UnityEngine;

public class BaseAIData
{
    public int health;
    public int damage;
    public float rotationSpeed;
    public float detectionRange;
    public float deathTime;
    public float hitRecoveryTime;

    public BaseAIData(BaseAIStats stats)
    {
        health = stats.health;
        damage = stats.damage;
        rotationSpeed = stats.rotationSpeed;
        detectionRange = stats.detectionRange;
        deathTime = stats.deathTime;
        hitRecoveryTime = stats.hitRecoveryTime;
    }
}

public class BaseAIStats : ScriptableObject
{
    public int health = 100;
    public int damage = 10;
    public float rotationSpeed = 3.5f;
    public float detectionRange = 10f;
    public float deathTime = 2f;
    public float hitRecoveryTime = 2f;
}
